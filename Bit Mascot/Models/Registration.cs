using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bit_Mascot.Models
{
    public class Registration
    {
        [Key]
        public string Email { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public DateTime Birthdate { get; set; }
        
        public byte Identity { get; set; }
    }
}