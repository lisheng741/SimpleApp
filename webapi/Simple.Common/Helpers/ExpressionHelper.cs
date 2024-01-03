using System.Linq.Expressions;

namespace Simple.Common.Helpers;

public enum ExpressionCombineType
{
    AndAlso,
    OrElse
}

public static class ExpressionHelper
{
    public static Expression<Func<T, bool>>? Combine<T>(Expression<Func<T, bool>>? expression1, Expression<Func<T, bool>>? expression2, ExpressionCombineType combineType = ExpressionCombineType.AndAlso)
    {
        var parameter = Expression.Parameter(typeof(T));

        if (expression1 == null) return expression2;
        if (expression2 == null) return expression1;

        var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expression1.Body);
        if (left == null) throw new ArgumentNullException(nameof(left));

        var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expression2.Body);
        if (right == null) throw new ArgumentNullException(nameof(right));

        return combineType switch
        {
            ExpressionCombineType.AndAlso => Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter),
            ExpressionCombineType.OrElse => Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter),
            _ => throw new NotImplementedException($"{combineType}"),
        };
    }

    class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression? Visit(Expression? node)
        {
            if (node == _oldValue)
            {
                return _newValue;
            }

            return base.Visit(node);
        }
    }
}
