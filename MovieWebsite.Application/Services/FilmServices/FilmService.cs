using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieWebsite.Application.Models.DTOs.FilmDTos;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Domain.Entities;

using AutoMapper;
using MovieWebsite.Application.Services.FilmServices;
using MovieWebsite.Domain.Interfaces;
using MovieWebsite.Infrastructure.Context;
using MovieWebsite.Domain.Enums;
using Microsoft.AspNetCore.Http;

public class FilmService : IFilmService
{
    private readonly IMapper _mapper;
    private readonly IGenreRepository _genreRepository;
    private readonly IFilmRepository _filmRepository;

    public FilmService(IMapper mapper, IGenreRepository genreRepository, IFilmRepository filmRepository)
    {
        _mapper = mapper;
        _genreRepository = genreRepository;
        _filmRepository = filmRepository;
    }

    public async Task<List<FilmVM>> GetAllFilms()
    {
        var films = await _filmRepository.GetFilteredList(
            select: f => new FilmVM
            {
                Id = f.Id,
                Title = f.Title,
                Description = f.Description,
                ImagePath = f.ImagePath,
                GenreName = f.Genre.Name,
                GenreId = f.GenreId
            },
            where: f => f.Status != Status.Passive,
            orderBy: f => f.OrderBy(f => f.Title)
        );
        return films;
    }

    public async Task<FilmVM> GetFilmByIdAsync(int id)
    {
        var film = await _filmRepository.GetFilteredFirstOrDefault(
            select: f => new FilmVM
            {
                Id = f.Id,
                GenreId = f.GenreId,               
                Title = f.Title,
                Description = f.Description,
                ImagePath = f.ImagePath,
                GenreName = f.Genre.Name,
            },
            where: f => f.Id == id && f.Status != Status.Passive
        );
        return film;
    }

    public async Task Create(CreateFilmDTO filmDTO)
    {
       var film = _mapper.Map<Film>(filmDTO);
        film.Title = filmDTO.Title;
        film.Description = filmDTO.Description;
        film.GenreId = filmDTO.GenreId;
        film.ImagePath = filmDTO.ImagePath;
        film.Status = Status.Active;
        film.CreateDate = DateTime.Now;
        await _filmRepository.Create(film);
    }

   

    public async Task DeleteFilmAsync(int id)
    {
        var film = await _filmRepository.GetDefault(f => f.Id == id && f.Status != Status.Passive);
        if (film == null)
        {
            // Film not found, handle the case accordingly
            return;
        }

        film.Status = Status.Passive;
        await _filmRepository.Update(film);
    }

    public async Task<List<FilmVM>> GetFilmsByGenreId(int genreId)
    {
        var films = await _filmRepository.GetFilteredList(
            select: x => new FilmVM
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImagePath = x.ImagePath,
                GenreName = x.Genre.Name,
                GenreId = x.GenreId
            },
            where: x => x.GenreId == genreId && x.Status != Status.Passive,
            orderBy: x => x.OrderBy(f => f.Title)
        );

        return films;
    }

    public async Task UpdateFilm(UpdateFilmDto filmDto)
    {
        var film = await _filmRepository.GetDefault(f => f.Id == filmDto.Id && f.Status != Status.Passive);
        if(film is not null)
        {
            if(film.GenreId != filmDto.GenreId)
            {
                film.GenreId = filmDto.GenreId;
                film.Genre = await _genreRepository.GetDefault(g => g.Id == filmDto.GenreId);
            }
            if(filmDto.UpLoadPath != null && filmDto.UpLoadPath.Length>0)
            {
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(filmDto.UpLoadPath.FileName)}";
                string uploadDirectory = Path.Combine("wwwroot", "images");
                Directory.CreateDirectory(uploadDirectory);
                string filePath = Path.Combine(uploadDirectory, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await filmDto.UpLoadPath.CopyToAsync(stream);
                }
                film.ImagePath= $"/{Path.Combine("images", fileName)}";
            }
            else
            {
                film.ImagePath = filmDto.ImagePath ?? film.ImagePath;
            }
            film.Title = filmDto.Title;
            film.Description = filmDto.Description;
            film.UpdateDate = DateTime.Now;
            film.Status = Status.Modified;
            film.GenreId = filmDto.GenreId;
            film.ImagePath = filmDto.ImagePath;
            await _filmRepository.Update(film);
        }
    }


    public async Task<string> SaveFile(IFormFile file)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var filePath = Path.Combine(directory, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return "/images/" + fileName;

    }
}