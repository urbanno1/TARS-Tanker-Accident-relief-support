﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TARS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TARS.Web.Controllers
{
    public class RoleController : ApiController
    {
        [Route("api/GetAllRoles")]
        [HttpGet]
        public HttpResponseMessage GetAllRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roles = roleManager.Roles.Select(x => x.Name).ToList();
            return this.Request.CreateResponse(HttpStatusCode.OK, roles);
        }

    }
}
