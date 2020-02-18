using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi_test.Models.DTO
{
    public class Resource_DTO
    {
                    
        public string Name { get; set; }

        public string Description { get; set; }

        public int TypeID { get; set; }

        public string Client { get; set; }
    }
}
