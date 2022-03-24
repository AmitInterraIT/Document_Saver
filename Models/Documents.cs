using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Saver.Models
{
    public class Documents
    {
        [Key]
        public int Document_Id { get; set; }
        public int Project_Id { get; set; }
       
        public string Document_Name { get; set; }
        [ForeignKey("Project_Id")]
        public virtual ProjectDetails ProjectDetails { get; set; }
        public string File_Name { get; set; }
        public DateTime Created_At { get; set; }= DateTime.Now;
        public DateTime Updated_At { get; set; }=   DateTime.Now;
        public string Created_By { get; set; }
        public string Updated_By { get; set; }
        public bool Is_Deleted { get; set; }
        public bool Is_Active { get; set; }
        public string Process_Id { get; set; } = "";
    }
}
