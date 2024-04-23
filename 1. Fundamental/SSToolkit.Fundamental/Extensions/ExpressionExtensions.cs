namespace SSToolkit.Fundamental.Extensions
{
    using System;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static string GetPropertyName<T>(this Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((MemberExpression)expression).Member.Name;
                case ExpressionType.Convert:
                    return ((UnaryExpression)expression).Operand.GetPropertyName<T>();
                default:
                    throw new NotSupportedException(expression.NodeType.ToString());
            }
        }

        public static string ToExpressionString<T>(this Expression<Func<T, bool>> source)
        {
            if (source != null)
            {
                var result = source.ToString();
                // strip the parameter from the expression
                var name = result.SliceTill(" =>");
                return result.Replace($"{name}.", string.Empty).SliceFrom("=> ");
            }

            return string.Empty;
        }

        public static string ToExpressionString<T>(this Expression<Func<T, object>> source)
        {
            if (source != null)
            {
                var result = source.ToString();
                // strip the parameter from the expression
                var name = result.SliceTill(" =>");
                return result.Replace($"{name}.", string.Empty).SliceFrom("=> ");
            }

            return string.Empty;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);
            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expr)
        {
            var parameter = Expression.Parameter(typeof(T));

            var exprVisitor = new ReplaceExpressionVisitor(expr.Parameters[0], parameter);
            var newExpr = exprVisitor.Visit(expr.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.Not(newExpr), parameter);
        }

        public class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression oldValue;
            private readonly Expression newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                this.oldValue = oldValue;
                this.newValue = newValue;
            }

            public override Expression Visit(Expression node)
                => node is not null && node == this.oldValue
                    ? this.newValue
                    : base.Visit(node) ?? Expression.Empty();
        }
    }
}
