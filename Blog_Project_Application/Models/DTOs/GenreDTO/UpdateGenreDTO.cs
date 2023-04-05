using Blog_Project_Application.Models.VM.AuthorVM;
using Blog_Project_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Blog_Project_Application.Models.DTOs.GenreDTO
{
    public class UpdateGenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public List<AuthorVM> Authors { get; internal set; }
    }
}
