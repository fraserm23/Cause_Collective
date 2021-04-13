using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PFTW_CW2.Models
{
    public class Cause
    {
        public int id { get; set; }
        public String title { get; set; }
        public String description { get; set; }
        public int signatureCount { get; set; }
        public List<User> signatureList { get; set; }
        public String imageURL { get; set; }
        public Boolean isActive { get; set; }
        public User owner { get; set; }

        public Cause()
        {
            signatureList = new List<User>();
            signatureCount = signatureList.Count();
        }
    }

}