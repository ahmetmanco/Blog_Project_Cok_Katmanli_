﻿using Blog_Project_Domain.Entities;
using Blog_Project_Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project_Infrustructure.Repositories
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        public LikeRepository(AppDbContext appDbContex) : base(appDbContex)
        {
        }
    }
}
