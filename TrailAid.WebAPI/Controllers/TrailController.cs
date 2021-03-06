﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrailAid.Models.Trail;
using TrailAid.Services;

namespace TrailAid.WebAPI.Controllers
{
    public class TrailController : ApiController
    {
        public IHttpActionResult Get()
        {
            TrailService TrailService = CreateTrailService();
            var user = TrailService.GetTrails();
            return Ok(user);
        }
        public IHttpActionResult Get(int id)
        {
            TrailService TrailService = CreateTrailService();
            var user = TrailService.GetTrailByID(id);
            return Ok(user);
        }
        public IHttpActionResult Post(TrailCreate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTrailService();

            if (!service.CreateTrail(user))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Put(TrailEdit user, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreateTrailService();

            if (!service.UpdateTrail(user, id)) return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var service = CreateTrailService();

            if (!service.DeleteTrail(id)) return InternalServerError();

            return Ok();
        }
        private TrailService CreateTrailService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var trailService = new TrailService(userID);
            return trailService;
        }
    }
}
