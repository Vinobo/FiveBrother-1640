using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiveBrother.Models
{
    public class Department
    {
        [Key]
        public int DepId { set; get; }
        public string Name { set; get; }
        public virtual ICollection<CUser> Users { set; get; }
    }
}
