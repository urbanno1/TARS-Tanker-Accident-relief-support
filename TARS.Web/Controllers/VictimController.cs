using TARS.BusinessEntity;
using TARS.BusinessService.Abstract;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Security.Claims;
using System.Drawing.Imaging;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Net.Http.Headers;

namespace TARS.Web.Controllers
{
    public class VictimController : ApiController
    {
        private readonly IVictimService _victim;

        public VictimController(IVictimService victim)
        {
            this._victim = victim;
        }


        [Route("api/Victim/RegisterDonor")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage RegisterDonor(DonorEntityModel donor)
        {
            var donorEntity = _victim.InsertDonor(donor);
            if (donorEntity > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Registration successful");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Registration not successful");
        }


        [HttpGet]
        [Authorize(Roles = "Victim")]
        [Route("api/Victim/GetStates")]

        public HttpResponseMessage GetStates()
        {
            var states = _victim.GetStates();
            if (states != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, states);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, states);
        }

        [HttpGet]
        [Authorize(Roles = "Victim")]
        [Route("api/Victim/GetLga/{stateId}")]
        public HttpResponseMessage GetLga([FromUri]int stateId)
        {
            var lgas = _victim.GetLga(stateId);
            if (lgas != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, lgas);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, lgas);
        }

        [HttpGet]
        [Authorize(Roles = "Victim")]
        [Route("api/Victim/GetCity/{lgaId}")]
        public HttpResponseMessage GetCity([FromUri]int lgaId)
        {
            var cities = _victim.GetCity(lgaId);
            if (cities != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, cities);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, cities);
        }

        [HttpPost]
        [Authorize(Roles = "Victim")]
        [Route("api/Victim/addVictimProfile")]
        public HttpResponseMessage AddVictimProfile(VictimModel entity)
        {
            var user = HttpContext.Current.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst("UserId").Value;
            var city = _victim.InsertVictim(entity, userId);
            if (city > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, city);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, city);
        }

        [HttpPost]
        [Authorize(Roles = "Victim")]
        [Route("api/Victim/uploadImage")]
        public HttpResponseMessage UploadImage()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
            HttpContext.Current.Request.Files : null;
            if (file != null)
            {
                var user = HttpContext.Current.User.Identity as ClaimsIdentity;
                var userId = user.FindFirst("UserId").Value;
                var listPhoto = new List<Tuple<string, string>>();
                for (var i = 0; i < file.Count; i++)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file[i].FileName);
                    var fileNamee = Path.GetFileNameWithoutExtension(file[i].FileName);
                    string extention = Path.GetExtension(file[i].FileName) == "" ? ".jpg" : Path.GetExtension(file[i].FileName);

                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    var photoUrl = "~/UploadedImage/" + fileName;
                    var photoName = _victim.getImageName(fileNamee);

                    fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedImage/"), fileName);
                    file[i].SaveAs(fileName);

                    listPhoto.Add(new Tuple<string, string>(photoUrl, photoName));
                }
                _victim.UploadImage(listPhoto, userId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpGet]
        [Authorize(Roles = "Victim")]
        [Route("api/Victim/LoadVictimProfile")]
        public HttpResponseMessage LoadVictimProfile()
        {
            var user = HttpContext.Current.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst("UserId").Value;
            var loadedProfile = _victim.LoadVictimProfile(userId);
            if (loadedProfile != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, loadedProfile);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, loadedProfile);
        }

        [HttpGet]
        [Authorize(Roles = "Victim")]
        [Route("api/Victim/LoadVictimPhoto")]
        public HttpResponseMessage LoadVictimPhoto()
        {
            var user = HttpContext.Current.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst("UserId").Value;
            var loadedPhoto = _victim.LoadVictimPhoto(userId);
            if (loadedPhoto != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, loadedPhoto);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, loadedPhoto);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Victim/GetVictimPhoto")]
        public HttpResponseMessage GetVictimPhoto([FromUri]string photoUrl)
        {
            var loadedPhoto = _victim.GetVictimPhoto(photoUrl);
            if (loadedPhoto != null)
            {
                MemoryStream ms = new MemoryStream();
                HttpContext context = HttpContext.Current;

                string filePath = context.Server.MapPath(string.Concat("/UploadedImage/", loadedPhoto));
                string extension = Path.GetExtension(loadedPhoto);

                if (File.Exists(filePath))
                {
                    if (!string.IsNullOrWhiteSpace(extension))
                    {
                        extension = extension.Substring(extension.IndexOf(".") + 1);
                    }
                    ImageFormat format = GetImageFormat(extension);
                    //If invalid image file is requested the following line wil throw an exception  
                    new Bitmap(filePath).Save(ms, format != null ? format as ImageFormat : ImageFormat.Bmp);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(ms.ToArray())
                    };
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue(string.Format("image/{0}", Path.GetExtension(loadedPhoto)));

                    return result;
                }
            }

            //return Request.CreateResponse(HttpStatusCode.OK, loadedPhoto);
            return Request.CreateResponse(HttpStatusCode.NotFound, loadedPhoto);
        }

        public static ImageFormat GetImageFormat(string extension)
        {
            ImageFormat result = null;
            PropertyInfo prop = typeof(ImageFormat).GetProperties().Where(p => p.Name.Equals(extension, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (prop != null)
            {
                result = prop.GetValue(prop) as ImageFormat;
            }
            return result;
        }
    }
}
