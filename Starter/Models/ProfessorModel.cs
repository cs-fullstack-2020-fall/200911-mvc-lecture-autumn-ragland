using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Starter.Models
{
    public class ProfessorModel 
    {
        [Key]
        public int id{get;set;}

        [Required(ErrorMessage = "A last name is required for all professors")]
        public string lastName{get;set;}
        
        public List<CourseModel> currentCourses{get;set;}
    }
}