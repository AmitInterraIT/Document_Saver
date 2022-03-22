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
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

        public int Created_By { get; set; }
        public int Updated_By { get; set; }

        public string Process_Id { get; set; }

    }
}
