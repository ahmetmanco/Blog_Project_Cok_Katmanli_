using Blog_Project_Application.Extensions;
using Blog_Project_Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.AppUserDTO
{
    public class UpdateProfileDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status { get; set; }
        [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }
        public string? ImagePath { get; set; }
    }
}
