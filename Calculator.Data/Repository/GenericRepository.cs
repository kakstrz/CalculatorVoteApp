using Calculator.Domain.Interfaces;
using Calculator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculator.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : DomainObject
    {
        protected CalculatorDbContextFactory _contextFactory;

        public GenericRepository(
            CalculatorDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException($"{nameof(Create)} entity must not be null");
            }

            using(CalculatorDbContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                
                return createdResult.Entity;
            }
        }

        public async Task<T> Get(Guid id)
        {
            using (CalculatorDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (CalculatorDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(Guid id, T entity)
        {
            using (CalculatorDbContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
