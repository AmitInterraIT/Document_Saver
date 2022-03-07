using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Document_Saver.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [StringLength(30, ErrorMessage = "Name Cannot Exceed")]
        public string User_Name { get; set; }
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter Valid Email")]
        public string User_Email { get; set; }
        [Required]
        /*[RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a one special character")]*/
        [StringLength(15, ErrorMessage = "Password Cannot Exceed")]
        public string User_Password { get; set; }
        [Required]
        [Range(1, 100000000000000, ErrorMessage = "Enter Right Value")]
        public int User_Phone { get; set; }
        [StringLength(6, ErrorMessage = "Emp_Id Cannot Exceed")]
        public string User_Emp_Id { get; set; }

        public string User_Image { get; set; } = "";

        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; }=   DateTime.Now;
        public string Created_By { get; set; } = "";
        public string Updated_By { get; set; } = "";
        public int Status { get; set; } = 0;

       
    }
}
