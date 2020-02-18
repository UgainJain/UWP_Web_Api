using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webApi_test.ViewModels.Resource
{
    public class Resource_ViewModel
    {
        public Resource_ViewModel()
        {
            Id = -1;
            parentId = -1;
        }
        public int Id { get; set; }

        public int? parentId { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Resource Type Can't be null")]
        public int TypeID { get; set; }

        public string Client { get; set; }
    }
}
