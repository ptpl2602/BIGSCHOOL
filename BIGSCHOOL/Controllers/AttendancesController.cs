using BIGSCHOOL.DTOs;
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
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            var existingAttendance = _dbContext.Attendances.FirstOrDefault(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId);
            if (existingAttendance != null)
            {
                _dbContext.Attendances.Remove(existingAttendance);
                _dbContext.SaveChanges();
                return Ok();
            }
            var attendance = new Attendance()
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
            };

            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
