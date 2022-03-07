using System.ComponentModel.DataAnnotations;

namespace Document_Saver.Models
{
    public class Documents
    {
        [Key]
        public int Project_Id { get; set; }
        public int Document_Id { get; set; }
        public string Document_Name { get; set; }
        public string File_Name { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public string Created_By { get; set; }
        public string Updated_By { get; set; }
    }
}
