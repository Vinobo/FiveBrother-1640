using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiveBrother.Models
{
    public enum Gender { Male, Female, Other };
    public enum Role { Admin, Staff };
    public class CUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { set; get; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { set; get; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { set; get; }
        [Required]
        public Gender Gender { set; get; }
        public int Phone { get; set; }
        [Required]
        public int DepartmentId { set; get; }
        public virtual Department Departments { set; get; }
    }
}
