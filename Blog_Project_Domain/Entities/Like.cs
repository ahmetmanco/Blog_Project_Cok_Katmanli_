using Blog_Project_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project_Domain.Entities
{
    public class Like:IBaseEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string AppUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        //*************************************************************
        public Post Post { get; set; }
        public AppUser AppUser { get; set; }
    }
}
