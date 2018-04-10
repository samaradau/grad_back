using System.Web.Mvc;
using NLog.Fluent;

namespace DemoLab
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Log.Error().Exception(filterContext.Exception).Write();

            base.OnException(filterContext);
        }
    }
}
