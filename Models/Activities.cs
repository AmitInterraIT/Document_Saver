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
        public string Activity_Title { get; set; }
        public DateTime Project_Created_At { get; set; }
        public DateTime Project_Updated_At { get; set; }
        public DateTime Document_Created_At { get; set; }
        public DateTime Document_Updated_At { get; set; }
        public DateTime Document_Deleted_At { get; set; }
    }
 }
