using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Seguridad
{
    public class Session : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext Filter)
        {
            if (HttpContext.Current.Session["User"] == null)
            {
                Filter.Result = new RedirectResult("~/Acceso/Login");
            
            }
            base.OnActionExecuting(Filter);
        }
    }
}