using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TARS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using TARS.BusinessService.Abstract;
using System.Web;
using TARS.BusinessEntity;

namespace TARS.Web.Controllers
{

    //[EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        private readonly IVictimService _victim;
        public AccountController(IVictimService victim)
        {
            this._victim = victim;
        }

        [Route("api/user/register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(AccountModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.RegisteredDate = DateTime.Now;
            
           
            IdentityResult result = userManager.Create(user, model.Password);
            if (result.Succeeded)
                userManager.AddToRole(user.Id, "Victim");
            var data = new VictimModel();
            var v = _victim.InsertVictim(data, user.Id);
            return result;
        }

        [HttpGet]
        [Route("api/user/GetUserClaims")]
        public AccountModel GetUserClaims()
        {
            var user = HttpContext.Current.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst("UserId").Value;
            var dashboard = _victim.GetDashboard(userId);

            var identityClaim = (ClaimsIdentity)User.Identity;
            var accountModel = new AccountModel()
            {
                PercentagePhoto = (dashboard != null) ? dashboard.PercentagePhoto: 0 ,
                PercentageProfile = (dashboard != null) ? dashboard.PercentageProfile: 0,
                PercentageGeneralPercentageProfile = (dashboard != null) ? dashboard.PercentageGeneralPercentageProfile : 0,
                PercentageGeneralPhoto = (dashboard != null) ? dashboard.PercentageGeneralPhoto : 0,
                TotalGeneralProfile = (dashboard != null) ? dashboard.TotalGeneralProfile : 0,
                TotalGeneralPhoto = (dashboard != null) ? dashboard.TotalGeneralPhoto : 0,
                TotalGeneralDonors = (dashboard != null) ? dashboard.TotalGeneralDonors : 0,
                UserId = identityClaim.FindFirst("UserId").Value,
                FirstName = identityClaim.FindFirst("FirstName").Value,
                Email = identityClaim.FindFirst("Email").Value,
                LastName = identityClaim.FindFirst("LastName").Value,
                PhoneNumber = identityClaim.FindFirst("PhoneNumber").Value,
                LoggedOn = identityClaim.FindFirst("LoggedOn").Value,
            };

            return accountModel;
        }
       
    }
}
