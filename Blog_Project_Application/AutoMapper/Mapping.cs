using _3_Blog_Project_Application.Models.DTOs.GenreDTO;
using AutoMapper;
using Blog_Project_Application.Models.DTOs.PostDTO;
using Blog_Project_Application.Models.VM.GenreVM;
using Blog_Project_Domain.Entities;
using Models.VM.PostVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Blog_Project_Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, GetPostVM>().ReverseMap();
            CreateMap<Post, PostDetailsVM>().ReverseMap();

            CreateMap<PostVM, UpdatePostDTO>().ReverseMap();

            CreateMap<Genre, CreateGenreDTO>().ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();

            CreateMap<GenreVM, UpdateGenreDTO>().ReverseMap();
        }
    }
}
