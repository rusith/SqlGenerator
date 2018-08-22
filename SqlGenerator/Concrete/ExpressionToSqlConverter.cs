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

                            if (rightConstantExpression.Type.IsSubclassOf(typeof(Nullable)))
                            {
                                if (!(rightConstantExpression.Value as int?).HasValue)
                                {
                                    return $"[{member.Name}] IS NULL";
                                }
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
            else if (left.NodeType == ExpressionType.Convert)
            {
                
                var leftU = (UnaryExpression)left;
                var leftOparandExpresson = leftU.Operand;
                if (leftOparandExpresson is MemberExpression leftOperandMemberAccessException)
                {

                    if (right.NodeType == ExpressionType.MemberAccess &&
                        right is MemberExpression rightMeberAccess)
                    {
                        if (rightMeberAccess.Member is FieldInfo fieldInfo)
                        {

                            if (fieldInfo.FieldType.IsValueType && fieldInfo.GetValue(((ConstantExpression)rightMeberAccess.Expression).Value) == null)
                            {
                                return $"[{leftOperandMemberAccessException.Member.Name}] IS NULL";
                            }
                        }
                        //if (rightMember.DeclaringType.IsSubclassOf(typeof(Nullable)))
                        //{
                        //    if (rightMember.MemberType == MemberTypes.Property)
                        //    {
                        //        var ce = (ConstantExpression)rightMeberAccess.Expression;
                        //        var fieldInfo = ce.Value.GetType().GetField(leftOperandMemberAccessException.Member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        //        var value = fieldInfo.GetValue(ce.Value);

                        //        var propertyValue = rightMember.DeclaringType.GetProperty(rightMember.Name)
                        //            .GetValue(value);

                        //        if (propertyValue == null)
                        //        {
                        //            return $"[{leftOperandMemberAccessException.Member.Name}] IS NULL";
                        //        }

                        //    }
                        //}
                    }
               
                }
            }
            return "";
        }
    }
}
