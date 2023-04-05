using Blog_Project_Application.Extensions;
using Blog_Project_Application.Models.VM.AuthorVM;
using Blog_Project_Application.Models.VM.GenreVM;
using Blog_Project_Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Blog_Project_Application.Models.DTOs.AuthorDTO
{
    public class UpdateAuthorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;

        public List<AuthorVM> Authors { get; internal set; }
        public List<GenreVM> Genres { get; internal set; }
    }
}
