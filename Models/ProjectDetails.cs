using System.ComponentModel.DataAnnotations;

namespace Document_Saver.Models
{
    public class ProjectDetails
    {
        [Key]
        public int Project_Id { get; set; }
        [Required]
        public string Project_Name { get; set; }
        
        public string Project_Description { get; set; }
        public string Associated_Managers { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

    }
}
