using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PFTW_CW2.Models
{
    public class User
    {
        public int id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public Boolean isAdmin { get; set; }
        public Boolean isActive { get; set; }
        public List<Cause> UserCauses { get; set; }

        public User()
        {
            UserCauses = new List<Cause>();
        }

    }

}