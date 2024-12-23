using AutoMapper;
using ETicaretPlatformu.Application.Models.DTOs.CatagoryDto;
using MovieWebsite.Application.Models.DTOs.FilmDTos;
using MovieWebsite.Application.Models.DTOs.GenreDto;
using MovieWebsite.Application.Models.DTOs.UserDtos;
using MovieWebsite.Application.Models.DTOs.UserFilmLikeDTos;
using MovieWebsite.Application.Models.VMs.FilmVMs;
using MovieWebsite.Application.Models.VMs.GenreVM;
using MovieWebsite.Application.Models.VMs.UserFilmLikeViewModels;
using MovieWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Application.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, UpdateProfileDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();
            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<GenreVM, UpdateGenreDto>().ReverseMap();
            CreateMap<UpdateGenreDto, Genre>().ReverseMap();
            CreateMap<Film, CreateFilmDTO>().ReverseMap();
            CreateMap<Film,UpdateFilmDto>().ReverseMap();
            CreateMap<Film, FilmDetailVM>().ReverseMap();
            CreateMap<Film,FilmVM>().ReverseMap();
            CreateMap<UserFilmLike,UserFilmLikeDto>().ReverseMap();
            CreateMap<UserFilmLike,UserFilmLikeVM>().ReverseMap();
            CreateMap<Film, UserFilmLikeVM>().ReverseMap();
            CreateMap<FilmVM, UpdateFilmDto>().ReverseMap();



        }
    }
}
