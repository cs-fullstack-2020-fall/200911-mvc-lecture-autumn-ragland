using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Starter.Models
{
    // data model for a professor 
    public class ProfessorModel 
    {
        [Key]
        public int id{get;set;}

        [Required(ErrorMessage = "A last name is required for all professors")]
        [Display(Name = "Last Name")]
        public string lastName{get;set;}
        
        public List<CourseModel> currentCourses{get;set;}
    }
}