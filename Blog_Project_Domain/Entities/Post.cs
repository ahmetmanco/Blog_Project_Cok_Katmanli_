using Blog_Project_Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project_Domain.Entities
{
    public class Post : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile UploadPath { get; set; } // Resmi yüklemek için kullanacağız, 

        //************************************************************
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get ; set ; }
        public DateTime? DeleteDate { get; set ; }
        public Status Status { get; set ; }

        //************************************************************
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public Post()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }

    }
}
