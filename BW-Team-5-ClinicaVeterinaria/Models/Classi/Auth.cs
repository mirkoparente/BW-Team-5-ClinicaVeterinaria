using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BW_Team_5_ClinicaVeterinaria.Models.Classi
{
    public class Auth
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}