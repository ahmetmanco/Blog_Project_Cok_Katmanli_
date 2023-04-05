using Blog_Project_Application.Extensions;
using Blog_Project_Domain.Enums;
using Microsoft.AspNetCore.Http;
using Models.VM.PostVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Blog_Project_Application.Models.DTOs.AuthorDTO
{
    public class CreateAuthorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;

        public List<PostVM> Posts { get; set; }

    }
}
