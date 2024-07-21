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
        if (film.UpLoadPath != null)
        {
            using var image = Image.Load(film.UpLoadPath.OpenReadStream());
            image.Mutate(x => x.Resize(600, 560));
            Guid id = Guid.NewGuid();
            image.Save($"wwwroot/images/{id}.jpg");
            film.ImagePath = $"{id}.jpg";
        }
        else
        {
            film.ImagePath = "default.jpg";
        }

        await _filmRepository.Create(film);
    }

    public async Task UpdateFilm(UpdateFilmDto filmDto, int id)
    {
        var film = await _filmRepository.GetDefault(f => f.Id == id && f.Status != Status.Passive);
        if (film == null)
        {
            // Film not found, handle the case accordingly
            return;
        }

        film.Title = filmDto.Title;
        film.Description = filmDto.Description;

        // Update the image path if a new image is uploaded
        if (filmDto.UpLoadPath != null)
        {
            using var image = Image.Load(filmDto.UpLoadPath.OpenReadStream());
            image.Mutate(x => x.Resize(600, 560));
            Guid newId = Guid.NewGuid();
            image.Save($"wwwroot/images/{newId}.jpg");
            film.ImagePath = $"{newId}.jpg";
        }

        // Update genre ID
        film.GenreId = filmDto.GenreId;

        await _filmRepository.Update(film);
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

}