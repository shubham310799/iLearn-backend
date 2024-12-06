using iLearn.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iLearn.Common
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _policy;

        public AuthorizeAttribute(string policy = "")
        {
            _policy = policy;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User?)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            if (!string.IsNullOrEmpty(_policy))
            {
                if (!CheckPolicy(_policy, user))
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
            }
        }

        private bool CheckPolicy(string policy, User user)
        {
            bool res = false;
            foreach(var role in _policy.Split(','))
            {
                if (user.Roles.Any(r => r.RoleName == role.Trim()))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
    }
}
