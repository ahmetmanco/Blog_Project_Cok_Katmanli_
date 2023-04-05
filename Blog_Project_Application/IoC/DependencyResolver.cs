using _3_Blog_Project_Application.AutoMapper;
using _3_Blog_Project_Application.Services.AuthorServices;
using _3_Blog_Project_Application.Services.GenreServices;
using Autofac;
using AutoMapper;
using Blog_Project_Application.Services.AppUserServices;
using Blog_Project_Application.Services.PostServices;
using Blog_Project_Domain.Repositories;
using Blog_Project_Infrustructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Blog_Project_Application.IoC
{
    public class DependencyResolver : Module
    {
        //System.Reflection'dan almayacağız
        //Nuget:Autofac.Extensions.DependencyInJection ekle

        protected override void Load(ContainerBuilder builder)
        {
           
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AppUserServices>().As<IAppUserServices>().InstancePerLifetimeScope();
            builder.RegisterType<GenreServices>().As<IGenreServices>().InstancePerLifetimeScope();
            builder.RegisterType<PostServices>().As<IPostServices>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorServices>().As<IAuthorServices>().InstancePerLifetimeScope();
            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>(); /// AutoMapper klasörünün altına eklediğimiz Mapping classını bağlıyoruz.
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }
    }
}
