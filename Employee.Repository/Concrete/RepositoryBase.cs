using AutoMapper;
using Employee.Infrastructure.Persistence;
using Employee.Shared;
using Employee.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Repository.Concrete;

public  class RepositoryBase<TEntity , IModel , T > : IRepository<TEntity,IModel,T> 
    where TEntity : class,IEntity<T> , new()
    where IModel : class,IVM<T> , new()
    where T:IEquatable<T>
{
    private readonly EmployeeDbContext _dbContext;
    private readonly IMapper _mapper;


    public RepositoryBase (EmployeeDbContext dbContext,IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        DbSet = _dbContext.Set<TEntity>();

       
    }
   public DbSet<TEntity> DbSet { get; }
    public async Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public  async Task<IModel> DeleteAsync(T id)
    {
        var entity = await DbSet.FindAsync(id); 
        
        if(entity != null)
        {
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();

        }

        return _mapper.Map<IModel>(entity);

    }

    public async  Task<IEnumerable<IModel>> GetAllAsync()
    {
        var entities = await DbSet.ToListAsync();
        return _mapper.Map<IEnumerable<IModel>>(entities);

    }

    public async Task<List<IModel>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var entities = await includes.Aggregate(
            DbSet.AsQueryable(),
            (current, include) => current.Include(include)).
            ToListAsync().
            ConfigureAwait(false);
        return _mapper.Map<List<IModel>>(entities);
    }

    public async Task<IModel> GetByIdAsync(T id)
    {
        var data = await DbSet.FindAsync(id);
        return _mapper.Map<IModel>(data);

       
    }

    public async Task<IModel> InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<IModel>(entity);
    }


    public async Task<IModel> UpdateAsync(T id, TEntity entity)
    {


        if(entity == null)
        {
            throw new ArgumentNullException("entity");

        }

        var exist = await DbSet.FindAsync(id);

        if( exist != null) {

            DbSet.Entry(exist).CurrentValues.SetValues(entity);

            await _dbContext.SaveChangesAsync();

        
        
        }

        return _mapper.Map<IModel>(entity);
        
    }
}
