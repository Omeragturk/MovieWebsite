using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MovieWebsite.Domain.Interfaces;
using MovieWebsite.Domain.Entities;

namespace MovieWebsite.Infrastructure.Repositories
{
    public class BaseRepo<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _table;
        public BaseRepo(DbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            
            return await _table.AnyAsync(expression);
        }

        public async virtual Task Create(T entity)
        {
            
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            

        }

        public Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            
            return _table.FirstOrDefaultAsync(expression);
        }

        public Task<T> GetDefaultIncluding(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            
            IQueryable<T> query = _table;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefaultAsync(expression);
        }

        public Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).ToListAsync();
            
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {

            IQueryable<T> query = _table;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();  
            }
            else
            {
                return await query.Select(select).FirstOrDefaultAsync();
            }

        }

        public Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            
            IQueryable<T> query = _table;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(select).ToListAsync();
            }
            else
            {
                return query.Select(select).ToListAsync();
            }
        }

        public Task Update(T entity)
        {
            
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
    }
