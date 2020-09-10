using System.ComponentModel.DataAnnotations;

namespace Starter.Models
{
    public class CourseModel
    {
        [Key]
        public int id{get;set;}
        
        [Required(ErrorMessage = "Please include your professor ID")]
        public int professorID{get;set;}

        [Required(ErrorMessage = "A course must have a title")]
        public string title{get;set;}

        [Range(3, 18, ErrorMessage = "A course but meet between 3 and 18 hours a week")]
        public int hours{get;set;}

        [Required]
        public string timeOfDay{get;set;}

        public bool atCapacity{get;set;}
    }
}