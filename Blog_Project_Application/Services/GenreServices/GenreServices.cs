using _3_Blog_Project_Application.Models.DTOs.GenreDTO;
using AutoMapper;
using Blog_Project_Application.Models.VM.AuthorVM;
using Blog_Project_Application.Models.VM.GenreVM;
using Blog_Project_Domain.Entities;
using Blog_Project_Domain.Enums;
using Blog_Project_Domain.Repositories;
using Blog_Project_Infrustructure.Repositories;


namespace _3_Blog_Project_Application.Services.GenreServices
{
    public class GenreServices : IGenreServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        public GenreServices(IPostRepository postRepository, IMapper mapper, IGenreRepository genreRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
        }

        public async Task Create(CreateGenreDTO model)
        {
            Genre genre = _mapper.Map<Genre>(model);
            genre.Status = Status.Active;
            genre.CreateDate = DateTime.Now;

            await _genreRepository.Create(genre);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);
            genre.DeleteDate = DateTime.Now;
            genre.Status = Status.Passive;
        }

        public async Task<UpdateGenreDTO> GetById(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateGenreDTO>(genre);

            return model;
        }

        public  async Task<List<GenreVM>> GetGenres()
        {
            var genres = await _genreRepository.GetFilteredList(
                select: x => new GenreVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name)

                );
            return genres;
        }

        public async Task Update(UpdateGenreDTO model)
        {
            var genre = _mapper.Map<Genre>(model);
            genre.UpdateDate = DateTime.Now;
            genre.Status = Status.Modified;
            await _genreRepository.Update(genre);
        }
    }
}
