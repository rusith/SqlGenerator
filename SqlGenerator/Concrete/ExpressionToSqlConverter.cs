using System;
using System.Linq.Expressions;
using System.Reflection;
using SqlGenerator.Abstract;

namespace SqlGenerator.Concrete
{
    public class ExpressionToSqlConverter: IExpressionToSqlConverter
    {
        public string GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (!(predicate.Body is BinaryExpression body))
                throw new Exception("Only binary expressions are supported");

            var left = body.Left;
            var right = body.Right;


            if (left.NodeType == ExpressionType.MemberAccess && left is MemberExpression leftMemberExpression)
            {
                var member = leftMemberExpression.Member;
                if (member.DeclaringType == typeof(T))
                {
                    if (member.MemberType == MemberTypes.Property)
                    {
                        if (right.NodeType == ExpressionType.Constant && right is ConstantExpression rightConstantExpression)
                        {
                            if (rightConstantExpression.Value == null)
                            {
                                return $"[{member.Name}] IS NULL";
                            }
                            if (rightConstantExpression.Type == typeof(string))
                            {
                                return $"[{member.Name}] = '{rightConstantExpression.Value}'";
                            }
                            if (rightConstantExpression.Type == typeof(int))
                            {
                                return $"[{member.Name}] = {rightConstantExpression.Value}";
                            }
                        }
                      

                        


                    }
                }
            }
            return "";
        }
    }
}
