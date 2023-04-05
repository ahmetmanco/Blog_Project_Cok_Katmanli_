using _3_Blog_Project_Application.Models.DTOs.AuthorDTO;
using _3_Blog_Project_Application.Models.VM.AuthorVM;
using Blog_Project_Application.Models.DTOs.PostDTO;
using Blog_Project_Application.Models.VM.AuthorVM;
using Models.VM.PostVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Blog_Project_Application.Services.AuthorServices
{
    public interface IAuthorServices
    {
        Task Create(CreateAuthorDTO model);
        Task Update(UpdateAuthorDTO model);
        Task Delete(int Id);
        Task<UpdateAuthorDTO> GetById(int Id);
        Task<List<AuthorVM>> GetAuthors();
        Task<AuthorDetailsVM> GetAuthorDetails(int Id);
        Task<CreateAuthorDTO> CreateAuthor();
    }
}
