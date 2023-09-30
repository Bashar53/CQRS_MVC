using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Shared.Contracts;

public interface IRepository<TEntity,IModel,T>
where TEntity : class, IEntity <T> , new()
where IModel : class , IVM<T> , new()
where T  : IEquatable<T>

{
    /// <summary>
    /// Get By Identifier asynchronus
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    public Task<IModel> GetByIdAsync(T id);
    /// <summary>
    /// Get All Async
    /// </summary>
    /// <returns></returns>

    public Task<IEnumerable<IModel>> GetAllAsync();

   /// <summary>
   /// GetAllAsync
   /// </summary>
   /// <param name="include"></param>
   /// <returns></returns>
   

    

    public Task<List<IModel>> GetAllAsync(params Expression <Func<TEntity, object>>[] include);


    /// <summary>
    /// Delete The Asynchronus
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task DeleteAsync(TEntity entity);
    /// <summary>
    /// Delete The Asynchronus
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<IModel> DeleteAsync(T id);
    /// <summary>
    /// Update the asynchronus
    /// </summary>
    /// <param name="id"></param>
    /// <param name="entity"></param>
    /// <returns></returns>

    public Task<IModel> UpdateAsync(T id, TEntity entity);

    public Task<IModel> InsertAsync(TEntity entity);


    





}
