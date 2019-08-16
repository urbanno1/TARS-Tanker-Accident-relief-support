using TARS.BusinessEntity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TARS.BusinessService.Abstract;
using System.IO;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using System.Net.Http.Headers;
using System.Reflection;
using System.Linq;
using System;

namespace TARS.Web.Controllers
{
    public class AdminController : ApiController
    {
        private readonly IAdminService _admin;
        public AdminController(IAdminService admin)
        {
            _admin = admin;
        }

        [HttpPost]
        [Route("api/admin/GetVictimProfiles")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage GetVictimProfiles(AppFilter filter)
        {

            var profiles = _admin.GetListOfVictimsProfile(filter);
            if (profiles != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, profiles);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Victim Profiles not found.");
        }

        [HttpGet]
        [Route("api/admin/GetVictimPhotos/{victimId}")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage GetVictimPhotos(int victimId)
        {

            var profiles = _admin.GetListOfVictimsPhotos(victimId);
            if (profiles != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, profiles);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Victim Photos not found.");
        }

       
        [HttpPost]
        [Route("api/admin/GetDonors")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage GetDonors(AppFilter filter)
        {

            var donors = _admin.GetListOfDonors(filter);
            if (donors != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, donors);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Donors not found.");
        }

    }
}
