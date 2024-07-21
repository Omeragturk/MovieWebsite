using AutoMapper;
using ETicaretPlatformu.Application.Models.DTOs.CatagoryDto;
using MovieWebsite.Application.Models.DTOs.GenreDto;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Models.VMs.GenreVM;
using MovieWebsite.Domain.Entities;
using MovieWebsite.Domain.Enums;
using MovieWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Services.GenreServices
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateGenreDto model)
        {
            var category = _mapper.Map<Genre>(model);
            await _genreRepository.Create(category);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);
            await _genreRepository.Delete(genre);


        }

        public async Task<UpdateGenreDto> GetById(int id)
        {
            var category = await _genreRepository.GetFilteredFirstOrDefault(
             select: x => new GenreVM
             {
                 Id = x.Id,
                 Name = x.Name,
                 Description = x.Description
             },
             where: x => x.Id == id
              );
            return _mapper.Map<UpdateGenreDto>(category);

        }

        public Task<List<GenreVM>> GetGenres()
        {

            var categories = _genreRepository.GetFilteredList(
                               select: x => new GenreVM
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   Description = x.Description
                               },
                 where: x => x.Status != Status.Passive,
                 orderBy: x => x.OrderBy(x => x.Name)
                                                             );
            return categories;
        }

        public Task<bool> IsGenreExist(string name)
        {
            return _genreRepository.Any(x => x.Name == name);
            
        }

        public async Task Update(UpdateGenreDto model)
        {
            var category = _mapper.Map<Genre>(model);
            await _genreRepository.Update(category);
        }

    }
}
