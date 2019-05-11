using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VMMAPI.Models
{
    public class UserLogin
    {
        [Required]
        [Display(Name = "Pleasae enter userid")]
        public string UserId { set; get; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = " Please enter password")]
        public string Password { set; get; }
    }
}