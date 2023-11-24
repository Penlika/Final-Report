using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Report.Models
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set;}
        [Required]
        public string Password { get; set;}
    }
}