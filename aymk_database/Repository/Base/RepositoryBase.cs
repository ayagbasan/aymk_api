using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using aymk_database.Database;
using System.Data.Entity;
using aymk_library.Library.Models;
using aymk_library.Library.Util;

namespace aymk_database.Repository.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        public aymkResponse Add(TEntity entity)
        {
            try
            {
                using (var context = new AYMKEntities())
                {
                    var entityDB = context.Entry(entity);
                    entityDB.State = EntityState.Added;
                    context.SaveChanges();
                    return new aymkResponse(entity);
                }
            }
            catch (Exception ex)
            {
                return new aymkResponse(aymkError.AddError, entity.ToString(), ex);
            }
            
        }
       
        public aymkResponse Get(Expression<Func<TEntity, bool>> filter = null, List<string> includes=null)
        {
            try
            {
                using (var context = new AYMKEntities())
                {
                   
                    IQueryable<TEntity> query = context.Set<TEntity>();

                    if (includes != null)
                        foreach (var include in includes)
                            query = query.Include(include);
                   

                    return new aymkResponse(query.SingleOrDefault(filter));
                }
            }
            catch (Exception ex)
            {
                return new aymkResponse(aymkError.GetError, filter.ToString(), ex);

            }

        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter = null, List<string> includes = null)
        {
          
            using (var context = new AYMKEntities())
            {

                IQueryable<TEntity> query = context.Set<TEntity>();

                if (includes != null)
                    foreach (var include in includes)
                        query = query.Include(include);

                return  (query.SingleOrDefault(filter));
            }          

        }

        public aymkResponse GetList(Expression<Func<TEntity, bool>> filter = null, List<string> includes = null)
        {
            try
            {
                using (var context = new AYMKEntities())
                {
                    IQueryable<TEntity> query = context.Set<TEntity>();
                    if (includes != null)
                        foreach (var include in includes)
                            query = query.Include(include);

                    var data = filter == null
                        ? query.ToList()
                        : query.Where(filter).ToList();
                    return new aymkResponse(data);
                }
            }
            catch (Exception ex)
            {
                return new aymkResponse(aymkError.GetListError,filter.ToString(), ex);
            }
           
        }

        public aymkResponse Update(TEntity entity)
        {
            try
            {
                using (var context = new AYMKEntities())
                {
                    var entityDB = context.Entry(entity);
                    entityDB.State = EntityState.Modified;
                    context.SaveChanges();

                    return new aymkResponse(entity);
                }
            }
            catch (Exception ex)
            {
                return new aymkResponse(aymkError.UpdateError, entity.ToString(),ex);
            }
            
        }

        public aymkResponse Delete(TEntity entity)
        {
            try
            {
                using (var context = new AYMKEntities())
                {
                    var entityDB = context.Entry(entity);
                    entityDB.State = EntityState.Deleted;
                    context.SaveChanges();
                    return new aymkResponse(true);
                }
            }
            catch (Exception ex)
            {
                return new aymkResponse(aymkError.DeleteError, entity.ToString(), ex);
            }
           
        }
    }
}
