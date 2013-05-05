using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Sudoku.Infrastructure
{
    public static class Member
    {
        public static string Name<T>(Expression<Func<T>> member)
        {
            Contract.Requires(member != null);

            var memberExpression = member.Body as MemberExpression;

            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }

            return "";
        }

        public static string Name<T, TValue>(Expression<Func<T, TValue>> member)
        {
            Contract.Requires(member != null);

            var memberExpression = member.Body as MemberExpression;

            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }

            return "";
        }
    }
}
