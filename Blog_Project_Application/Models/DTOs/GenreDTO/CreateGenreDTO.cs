using Blog_Project_Domain.Enums;
using Models.VM.PostVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Blog_Project_Application.Models.DTOs.GenreDTO
{
    public class CreateGenreDTO
    {

        public string Name { get; set; }
       // public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
    }
}
