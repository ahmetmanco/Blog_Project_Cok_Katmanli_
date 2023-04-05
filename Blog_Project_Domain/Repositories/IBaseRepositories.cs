using Blog_Project_Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project_Domain.Repositories
{
    public interface IBaseRepositories<T> where T:IBaseEntity
    {
        //Create,Update,Delete,Any,GetDefault,GetDefaults,(Expression)

        //Task : Asenkron olarak çalışacak

        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> Any(Expression<Func<T, bool>> expression) ; //kayıt varsa true, yoksa false döner
        Task<T> GetDefault(Expression<Func<T, bool>> expression);
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression);

        //select , where ,sıralama, join
        //Hem select hem order by yapabileceğimiz Post ,Author,Comment,Like'ları birlikte çekmek için include etmek gerekir. Bir sorguya birden fazla tablo egirecek yani eagerLoading kullanacağız. 
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T,bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,//sıralama
            Func<IQueryable<T>,IIncludableQueryable<T,object>> include = null
            );

        Task<List<TResult>> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,//sıralama
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );
        
    }
}
