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
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string User_Phone { get; set; }
        [StringLength(6, ErrorMessage = "Emp_Id Cannot Exceed")]
        public string User_Emp_Id { get; set; }

        public string User_Image { get; set; } = "";

        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; }=   DateTime.Now;
        public string Created_By { get; set; } = "";
        public string Updated_By { get; set; } = "";
        public int Status { get; set; } = 0;

        public string Process_Id { get; set; } = "";
    }
}
