using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webApi_test.Models
{
    public class ResourceModel
    {
        public ResourceModel()
        {
            Children = new HashSet<ResourceModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? parentId { get; set; }

        public virtual ResourceModel parent { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Resource Type Can't be null")]
        public int TypeID { get; set; }
        [ForeignKey("TypeID")]
        public virtual ResourceTypeModel ResourceTypeModel { get; set; }
        [MaxLength(20)]
        public string Client { get; set; }

        public virtual ICollection<ResourceModel> Children { get; set; }
    }
}
