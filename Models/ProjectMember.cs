using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Saver.Models
{
    public class ProjectMember
    {
        [Key]
        public int Project_Member_Id { get; set; }
        [Required]
        public int Project_Id { get; set; }
        [ForeignKey("Project_Id")]
        public virtual ProjectDetails ProjectDetails { get; set; }

        public string Access_Permission { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public string Created_By { get; set; }
        [Required]
        public int User_Id { get; set; }
        [ForeignKey("User_Id")]

        public virtual User User { get; set; }
        public string Process_Id { get; set; } = "";
    }
}
