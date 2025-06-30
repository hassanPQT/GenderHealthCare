using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Role
    {
        [Key]
        [MaxLength(10)]
        public Guid RoleId { get; set; }

        [MaxLength(10)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
