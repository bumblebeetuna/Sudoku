using System;
using Autofac;

namespace Sudoku.Infrastructure
{
    public static class AutofacExtensions
    {
        public static bool TryResolveNamed<T>(this IComponentContext container, string serviceName, out T instance)
        {
            object value;

            if (container.TryResolveNamed(serviceName, typeof(T), out value))
            {
                instance = (T)value;

                return true;
            }

            instance = default(T);

            return false;
        }

        public static bool TryResolve<T>(this IComponentContext container, Type serviceType, out T instance) where T : class
        {
            object value;

            if (ResolutionExtensions.TryResolve(container, serviceType, out value))
            {
                instance = value as T;

                return true;
            }

            instance = default(T);

            return false;
        }
    }
}
