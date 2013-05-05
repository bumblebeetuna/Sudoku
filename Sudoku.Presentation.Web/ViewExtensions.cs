namespace System.Web.Mvc
{
    public static class ViewExtensions
    {
        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}