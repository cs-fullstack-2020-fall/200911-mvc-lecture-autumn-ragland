using System.ComponentModel.DataAnnotations;

namespace Starter.Models
{
    // data model for a professor's course
    public class CourseModel
    {
        [Key]
        public int id{get;set;}
        
        [Required(ErrorMessage = "Please include your professor ID")]
        [Display(Name = "Professor ID")]
        public int professorID{get;set;}

        [Required(ErrorMessage = "A course must have a title")]
        [Display(Name = "Title")]
        public string title{get;set;}

        [Range(3, 18, ErrorMessage = "A course but meet between 3 and 18 hours a week")]
        [Display(Name = "Course Hours")]
        public int hours{get;set;}

        [Required]
        [Display(Name = "Time of Day")]
        public string timeOfDay{get;set;}

        [Display(Name = "At Capacity")]
        public bool atCapacity{get;set;}
    }
}