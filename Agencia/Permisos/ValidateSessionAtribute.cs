using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agencia.Permisos
{
    public class ValidateSessionAtribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Current.Session["usuario"]== null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
                
            }
            base.OnActionExecuted(filterContext);
        }
    }
}