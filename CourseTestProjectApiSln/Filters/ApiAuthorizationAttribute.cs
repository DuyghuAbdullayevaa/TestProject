using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using CourseTestProjectApiSln.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CourseTestProjectApiSln.API.Filters
{
    public class ApiAuthorizationAttribute : Attribute, IActionFilter
    {
        private readonly RoleEnum[] _roles;

        public ApiAuthorizationAttribute(params RoleEnum[] roles)
        {
            _roles = roles;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity?.IsAuthenticated == false)
            {
                SetUnauthorizedResult(context);
                return;
            }

            var userRoles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => Enum.TryParse<RoleEnum>(c.Value, out var role) ? role : (RoleEnum?)null)
                .Where(role => role.HasValue)
                .Select(role => role.Value)
                .ToList();

            if (!_roles.Any(r => userRoles.Contains(r)))
            {
                SetForbiddenResult(context);
            }
        }

        private static void SetUnauthorizedResult(ActionExecutingContext context)
        {
            context.Result = new ObjectResult(new
            {
                Error = "Unauthorized",
                IsSuccess = false
            })
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
        }

        private static void SetForbiddenResult(ActionExecutingContext context)
        {
            context.Result = new ObjectResult(new
            {
                Error = "Forbidden",
                IsSuccess = false
            })
            {
                StatusCode = (int)HttpStatusCode.Forbidden
            };
        }
    }
}
