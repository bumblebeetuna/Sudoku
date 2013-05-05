using System.Diagnostics.Contracts;
using System.Globalization;

namespace Sudoku.Infrastructure
{
    public static class StringExtensions
    {
        public static string FormatInvariant(this string format, params object[] args)
        {
            Contract.Requires(format != null);
            Contract.Requires(args != null);
            Contract.Ensures(Contract.Result<string>() != null);

            return string.Format(CultureInfo.InvariantCulture, format, args);
        }
    }
}
