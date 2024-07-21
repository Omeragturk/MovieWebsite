using ETicaretPlatformu.Application.Models.DTOs.CatagoryDto;
using MovieWebsite.Application.Models.DTOs.GenreDto;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Models.VMs.GenreVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.Services.GenreServices
{
    public interface IGenreService
    {
        Task Create(CreateGenreDto model);
        Task Update(UpdateGenreDto model);
        Task Delete(int id);
        Task<List<GenreVM>> GetGenres();

        Task<UpdateGenreDto> GetById(int id);
        Task<bool> IsGenreExist(string name);

    }
}
