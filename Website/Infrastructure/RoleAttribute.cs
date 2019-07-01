using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace Website.Infrastructure {
    public class RoleAttribute : Attribute, IAuthorizationFilter {
        private readonly string[] _roles;
        public RoleAttribute(params string[] types) {
            _roles = types;
        }

        public void OnAuthorization(AuthorizationFilterContext context) {
            // Automatically give admins access
            if (context.HttpContext.User.IsInRole(RoleTypes.Administrator))
                return;

            // Check if the user has any of the roles provided
            foreach (string type in _roles) {
                if (context.HttpContext.User.IsInRole(type)) {
                    return;
                }
            }

            context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                { "action", "NoAccess" },
                { "controller", "Error" }
            });
        }
    }
}
