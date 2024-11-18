using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using MovieWebsite.Application.AutoMapper;
using MovieWebsite.Domain.Interfaces;
using MovieWebsite.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieWebsite.Application.Ioc
{
    public class DependencyResolver:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepo>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FilmRepo>().As<IFilmRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepo>().As<IGenreRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserFilmLikeRepo>().As<IUserFilmLikeRepo>().InstancePerLifetimeScope();    


            builder.Register(context => new MapperConfiguration(config =>
            {
                config.AddProfile<Mapping>();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();

                return config.CreateMapper(context.Resolve);
            })
                .As<IMapper>()
                .InstancePerLifetimeScope();






            // Silmiyoruz.
            base.Load(builder);
        }
    }
}
