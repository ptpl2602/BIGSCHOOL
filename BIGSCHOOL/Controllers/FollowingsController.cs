﻿using BIGSCHOOL.DTOs;
using BIGSCHOOL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BIGSCHOOL.Controllers
{
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            var existingFollowing = _dbContext.Followings.FirstOrDefault(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId);
            if (existingFollowing != null)
            {
                _dbContext.Followings.Remove(existingFollowing);
                _dbContext.SaveChanges();
                return Ok();
            }

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };

            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
