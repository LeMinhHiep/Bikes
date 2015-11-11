using System.Web.Mvc;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;

using MVCBase.Enums;
using MVCClient.Models;

namespace MVCClient.Controllers
{
    public class GenericSimpleAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var Db = new ApplicationDbContext();

            BaseController baseController = filterContext.Controller as BaseController;

            string aspUserID = filterContext.HttpContext.User.Identity.GetUserId();

            baseController.BaseService.UserID = Db.Users.Where(w => w.Id == aspUserID).FirstOrDefault().UserID;

            base.OnAuthorization(filterContext);
        }
    }



    public class AccessLevelAuthorizeAttribute : AuthorizeAttribute
    {
        private GlobalEnums.AccessLevel accessLevel;
        private BaseController baseController;

        public AccessLevelAuthorizeAttribute()
            : this(GlobalEnums.AccessLevel.Editable)
        { }

        public AccessLevelAuthorizeAttribute(GlobalEnums.AccessLevel accessLevel)
        {
            this.accessLevel = accessLevel;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            this.baseController = filterContext.Controller as BaseController;

            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized) return false;

            return this.baseController.BaseService.GetAccessLevel() >= this.accessLevel;
        }
    }





    public class OnResultExecutingFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);

            if (filterContext.Result is ViewResult)
            {
                var controller = filterContext.Controller as BaseController;
                controller.AddRequireJsOptions();
            }
        }
    }


}