using AutoMapper;
using Blog_Project_Application.Models.DTOs.PostDTO;
using Blog_Project_Application.Models.VM.GenreVM;
using Blog_Project_Domain.Entities;
using Blog_Project_Domain.Enums;
using Blog_Project_Domain.Repositories;
using Models.VM.PostVM;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog_Project_Application.Models.VM.AuthorVM;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;

namespace Blog_Project_Application.Services.PostServices
{
    public class PostServices : IPostServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        public PostServices(IPostRepository postRepository, IMapper mapper, IGenreRepository genreRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
        }


        public async Task Create(CreatePostDTO model)
        {
            Post post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");

                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
                post.ImagePath = $"/images/defaultpost.jpg";

            await _postRepository.Create(post);
        }

        public async Task<CreatePostDTO> CreatePost()
        {
            CreatePostDTO model = new CreatePostDTO()
            {

                Authors = await _authorRepository.GetFilteredList(
                    select: x => new AuthorVM()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.FirstName)
                    ),

                Genres = await _genreRepository.GetFilteredList(
                    select: x => new GenreVM
                    {
                        Id = x.Id,
                        Name = x.Name
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Name)
                    )
            };
            return model;
        }
        
        public async Task Delete(int Id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == Id);
            post.DeleteDate = DateTime.Now;
            post.Status = Status.Passive;
        }

        public async Task<UpdatePostDTO> GetById(int id)
        {
            Post post = await _postRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdatePostDTO>(post);

            model.Authors = await _authorRepository.GetFilteredList(
                select: x => new AuthorVM()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                },
                where: x=>x.Status != Status.Passive,
                orderBy: x=>x.OrderBy(x=>x.FirstName)
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

        public async Task<PostDetailsVM> GetPostDetails(int id)
        {
            var post = await _postRepository.GetFilteredFirstOrDefault(
                select: x => new PostDetailsVM()
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,
                    Content = x.Content,
                    CreateDate = x.CreateDate,
                    ImagePath = x.ImagePath,
                    Title = x.Title
                },
                where: x => x.Id == id,
                orderBy: null,
                include: x => x.Include(x => x.Author)
                );
            return post;
        }

        public async Task<List<GetPostVM>> GetPostForMember()
        {
            var posts = await _postRepository.GetFilteredList(
                select: x => new GetPostVM()
                {
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    AuthorImagePath = x.Author.ImagePath,
                    Content = x.Content,
                    CreateDate = x.CreateDate,
                    ImagePath = x.ImagePath,
                    Title = x.Title,
                    Id = x.Id
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate)
                );
            return posts;
        }

        public async Task<List<PostVM>> GetPosts()
        {
            var posts = await _postRepository.GetFilteredList(
                select:x=> new PostVM
                { 
                    Id = x.Id,
                    Title = x.Title,
                    GenreName = x.Genre.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,

                },
                where : x => x.Status != Status.Passive,
                orderBy: x=>x.OrderBy(x=>x.Title),
                include: x=>x.Include(x=>x.Author).Include(x=>x.Genre)
                );
            return posts;
        }

        public async Task Update(UpdatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);

            if(post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                post.ImagePath = model.ImagePath;
            }
            await _postRepository.Update(post);
        }
    }
}
