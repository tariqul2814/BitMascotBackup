using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bit_Mascot.Models.ViewModel
{
    public class RegistrationUserDetails
    {
        public string Email { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Birthdate { get; set; }

        public byte Identity { get; set; }
    }
}