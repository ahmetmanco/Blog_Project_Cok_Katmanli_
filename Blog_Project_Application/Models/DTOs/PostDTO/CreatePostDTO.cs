using Blog_Project_Application.Extensions;
using Blog_Project_Application.Models.VM.AuthorVM;
using Blog_Project_Application.Models.VM.GenreVM;
using Blog_Project_Domain.Entities;
using Blog_Project_Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project_Application.Models.DTOs.PostDTO
{
    public class CreatePostDTO
    {
        [Required(ErrorMessage = "Must to type Title")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must to type Content")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Content { get; set; }

        public string? ImagePath { get; set; }

        //custom extension yazacağız.

       [PictureFileExtension]
        public IFormFile? UploadPath { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        [Required(ErrorMessage = "Must to type Author")]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Must to type Genre")]
        public int GenreId { get; set; }

        // Genre ve Author CM listerleri doldurulacak
        public List<GenreVM>? Genres { get; set; }
        public List<AuthorVM>? Authors { get; set; }
    }
}
