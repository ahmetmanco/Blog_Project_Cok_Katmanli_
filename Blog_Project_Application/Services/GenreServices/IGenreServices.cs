using _3_Blog_Project_Application.Models.DTOs.GenreDTO;
using Blog_Project_Application.Models.VM.GenreVM;
using Blog_Project_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Blog_Project_Application.Services.GenreServices
{
    public interface IGenreServices
    {
        Task Create(CreateGenreDTO model);
        Task Update(UpdateGenreDTO model);
        Task Delete(int id);
        Task<UpdateGenreDTO> GetById(int id);
        Task<List<GenreVM>> GetGenres();
    }
}
