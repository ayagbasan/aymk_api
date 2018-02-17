using aymk_library.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace aymk_database.Repository.Base
{
    public interface IRepositoryBase<T> where T:class, new()
    {
        aymkResponse Get(Expression<Func<T, bool>> filter = null, List<string> includes = null);
        T GetSingle(Expression<Func<T, bool>> filter = null, List<string> includes = null);
        aymkResponse GetList(Expression<Func<T, bool>> filter = null, List<string> includes = null);
        aymkResponse Add(T entity);
        aymkResponse Update(T entity);
        aymkResponse Delete(T entity);

    }
}
