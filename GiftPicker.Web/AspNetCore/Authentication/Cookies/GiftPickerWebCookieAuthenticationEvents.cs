using GiftPicker.Db;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace GiftPicker.Web.AspNetCore.Authentication.Cookies
{
    public class GiftPickerWebCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var username = context.Principal.Identity.Name;

            if (!string.IsNullOrWhiteSpace(username))
            {
                var user = GiftPickerDb.Users.GetByUsername(username);

                if (user != null)
                {
                    var claims = context.Principal.Claims.ToList();

                    claims.Add(new Claim(ClaimTypes.Role, $"username-{username}"));

                    var identity = new ClaimsIdentity(claims, context.Principal.Identity.AuthenticationType);
                    var principal = new ClaimsPrincipal(identity);

                    context.ReplacePrincipal(principal);

                    return;
                }
            }

            context.RejectPrincipal();

            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}