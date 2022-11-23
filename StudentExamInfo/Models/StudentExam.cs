using System.ComponentModel.DataAnnotations;

namespace StudentExamInfo.Models
{
    public class StudentExam
    {
        [Required]
        public string StudentID { get; set; } = "";

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Module { get; set; } = "";

        [Required]
        [Range(0, 1000)]
        public int Grade { get; set; }

        public bool MatureStudent { get; set; } = false;
    }
}