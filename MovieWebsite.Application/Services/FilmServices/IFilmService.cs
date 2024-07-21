using MovieWebsite.Application.Models.DTOs.FilmDTos;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Services.FilmServices
{
    public interface IFilmService
    {

        Task<List<FilmVM>> GetAllFilms();
        Task<FilmVM> GetFilmByIdAsync(int id);
        Task Create(CreateFilmDTO filmDTO);
        Task UpdateFilm(UpdateFilmDto filmDto, int id);
        Task DeleteFilmAsync(int id); 
        
        Task<List<FilmVM>> GetFilmsByGenreId(int genreId);
    }
}
