using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Models.Request
{
    public class Register
    {
        [Required(ErrorMessage = "Please enter username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string password { get; set; }
    }
}
