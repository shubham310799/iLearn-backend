using iLearn.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using iLearn.Data.DTOs;

namespace iLearn.Common
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _policy;

        public AuthorizeAttribute(string policy = null)
        {
            _policy = policy;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User?)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new GlobalResponse<string>(){ ErrorCode = ErrorCodes.UserIsNotAuthenticated.ErrorCode, Message = ErrorCodes.UserIsNotAuthenticated.ErrorMessage}) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            else
            {

            }

            if (!string.IsNullOrEmpty(_policy))
            {
                if (!CheckPolicy(_policy, user))
                {
                    context.Result = new JsonResult(new GlobalResponse<string>() { ErrorCode = ErrorCodes.UnauthorizedUser.ErrorCode, Message = ErrorCodes.UnauthorizedUser.ErrorMessage }) { StatusCode = StatusCodes.Status401Unauthorized };
                    return;
                }
            }
        }

        private bool CheckPolicy(string policy, User user)
        {
            bool res = false;
            foreach(var role in _policy.Split(','))
            {
                if (user.UserRoles.Any(r => r.Role.RoleName == role.Trim()))
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
    }
}
