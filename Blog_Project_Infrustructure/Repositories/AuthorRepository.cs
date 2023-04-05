using Blog_Project_Domain.Entities;
using Blog_Project_Domain.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project_Infrustructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author> , IAuthorRepository
    {
        public AuthorRepository(AppDbContext appDbContex) : base(appDbContex)
        {
        }
    }
}
