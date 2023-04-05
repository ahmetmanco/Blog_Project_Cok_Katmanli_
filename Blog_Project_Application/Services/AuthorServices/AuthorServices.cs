using _3_Blog_Project_Application.Models.DTOs.AuthorDTO;
using _3_Blog_Project_Application.Models.VM.AuthorVM;
using AutoMapper;
using Blog_Project_Application.Models.DTOs.PostDTO;
using Blog_Project_Application.Models.VM.AuthorVM;
using Blog_Project_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using Blog_Project_Domain.Repositories;
using Blog_Project_Domain.Enums;
using Blog_Project_Infrustructure.Repositories;
using Blog_Project_Application.Models.VM.GenreVM;
using _3_Blog_Project_Application.Services.GenreServices;
using Models.VM.PostVM;
using Microsoft.EntityFrameworkCore;

namespace _3_Blog_Project_Application.Services.AuthorServices
{
    public class AuthorServices : IAuthorServices
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public AuthorServices(IAuthorRepository authorRepository, IMapper mapper, IGenreRepository genreRepository, IPostRepository postRepository)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _postRepository = postRepository;
        }

        public async Task Create(CreateAuthorDTO model)
        {
            Author author = _mapper.Map<Author>(model);

            if (author.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 480));

                Guid guid = Guid.NewGuid();
                image.Save($"/wwwroot/imagesauthor/{guid}.jpg");
                author.ImagePath = $"/images/{guid}.jpg";
            }
            else
                author.ImagePath = $"/images/defaultAuthor.jpg";

            await _authorRepository.Create(author);
        }

        public async Task<CreateAuthorDTO> CreateAuthor()
        {
            CreateAuthorDTO model = new CreateAuthorDTO()
            {
                Posts = await _postRepository.GetFilteredList(
                    select: x => new PostVM()
                    {
                        Id = x.Id,
                        AuthorFirstName = x.Author.FirstName,
                        AuthorLastName = x.Author.LastName,
                        Title = x.Title

                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x=>x.OrderBy(x=>x.Author.FirstName)
                    )
            };
            return model;
        }

        public async Task Delete(int Id)
        {
            Author author = await _authorRepository.GetDefault(x=>x.Id == Id);
            author.DeleteDate = DateTime.Now;
            author.Status = Status.Passive;
        }

        public async Task<AuthorDetailsVM> GetAuthorDetails(int id)
        {
            var author = await _authorRepository.GetFilteredFirstOrDefault(
                select: x => new AuthorDetailsVM()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath
                },
                where: x => x.Id == id
            ) ;
            return author;
        }

        public async Task<List<AuthorVM>> GetAuthors()
        {
            var authors = await _authorRepository.GetFilteredList(
               select: x => new AuthorVM
               {
                 Id = x.Id,
                 FirstName = x.FirstName,
                 LastName = x.LastName
               },
               where: x => x.Status != Status.Passive,
               orderBy: x => x.OrderBy(x => x.FirstName),
               include: x => x.Include(x => x.Posts)
               );
            return authors;
        }

        public async Task<UpdateAuthorDTO> GetById(int Id)
        {
            Author author = await _authorRepository.GetDefault(x => x.Id == Id);

            var model = _mapper.Map<UpdateAuthorDTO>(author);

            model.Authors = await _authorRepository.GetFilteredList(
                select: x => new AuthorVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName)
                );

            model.Genres = await _genreRepository.GetFilteredList(
                select: x => new GenreVM()
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name)
                );

            return model;
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);
            
                if (author.UploadPath != null)
                {
                    using var image = Image.Load(model.UploadPath.OpenReadStream());

                    image.Mutate(x => x.Resize(600, 560));
                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/imagesAuthor/{guid}.jpg");

                    author.ImagePath = $"images/{guid}.jpg";
                }
                else
                    author.ImagePath = model.ImagePath;
            
            await _authorRepository.Update(author);
        }
    }
}
