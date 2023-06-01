using BIGSCHOOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BIGSCHOOL.ViewModels
{
    public class CourseViewModels
    {
        [Required]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }
        [Required]
        public byte Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public DateTime GetDateTime()
        {
            string dateTimeString = $"{Date} {Time}";
            return DateTime.ParseExact(dateTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            //return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}