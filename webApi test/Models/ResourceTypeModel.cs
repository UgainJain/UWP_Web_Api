using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi_test.Models
{
    public class ResourceTypeModel
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50),Required]
        public string Name { get; set; }
        [MaxLength(200),Required]
        public string Description { get; set; }


    }
}
