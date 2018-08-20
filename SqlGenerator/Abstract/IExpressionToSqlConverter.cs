using System;
using System.Linq.Expressions;

namespace SqlGenerator.Abstract
{
    public interface IExpressionToSqlConverter
    {
        string GetWhere<T>(Expression<Func<T, bool>> predicate) where T: class;
    }
}
