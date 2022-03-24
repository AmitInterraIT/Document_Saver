using System.ComponentModel.DataAnnotations;

namespace Document_Saver.Models
{
    public class Activities
    {
        [Key]
        public int Activity_Id { get; set; }
        [Required]
        public bool Activity_Type { get; set; }
        [Required]
        public string Description { get; set; }
        public string Activity_User_Id { get; set; }
        public DateTime Project_Created_At { get; set; }= DateTime.Now;
        public string Device_Name { get; set; }
        public string Device_Type { get; set; }
        public string Browser_Name { get; set; }
        public string PI_Address { get; set; }
        public string Process_Id { get; set; } = "";
    }
 }
