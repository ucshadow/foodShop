using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace FoodStore.HtmlHelpers
{
    public static class IdentityExtensions
    {
        public static string GetImage(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            if (identity is ClaimsIdentity ci)
            {
                return ci.FindFirstValue("Image");
            }
            return "";
        }

        public static string GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            if (identity is ClaimsIdentity ci)
            {
                return ci.FindFirstValue("UserId");
            }
            return "";
        }
    }
}