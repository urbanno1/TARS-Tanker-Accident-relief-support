using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using TARS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TARS.Web
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

       
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
                context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
          // var user = context.OwinContext.GetUserManager

            var user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("UserId", user.Id));
            identity.AddClaim(new Claim("UserName", user.UserName));
            identity.AddClaim(new Claim("Email", user.Email));
            identity.AddClaim(new Claim("PhoneNumber", user.PhoneNumber));
            identity.AddClaim(new Claim("FirstName", user.FirstName));
            identity.AddClaim(new Claim("LastName", user.LastName));
            identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));

            var userRoles = userManager.GetRoles(user.Id);
            foreach(var roleName in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            }

            var additionalData = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                "role", Newtonsoft.Json.JsonConvert.SerializeObject(userRoles)
                }
            });
            var token = new AuthenticationTicket(identity, additionalData);
            context.Validated(token);
           
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
           foreach(KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }


    }
}