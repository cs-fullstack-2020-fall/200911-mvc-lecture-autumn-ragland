using System.Collections.Generic;
using System.Linq;
using Starter.DAO;
using Starter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Starter.Controllers
{
    public class Primary : Controller
    {
        // ref to db
        private readonly StarterDbContext _context;
        public Primary(StarterDbContext context)
        {
            _context = context;
        }
/*------------------------------------------------------READ------------------------------------------------------*/
        // view all professors
        public IActionResult Index()
        {
            return View(_context);
        }
        // view all courses
        public IActionResult ViewCourses(int professorID)
        {
            // MUST USE INCLUDE SO THAT NEW COURSE CAN BE ADDED TO MATCHING PROFESSOR
            ProfessorModel matchingProf = _context.professors.Include(prof => prof.currentCourses).FirstOrDefault(prof => prof.id == professorID);
            if(matchingProf != null)
            {
                return View("ViewCourse", matchingProf);
            } else 
            {
                return Content("No matching professor Found");                
            }
        }
/*------------------------------------------------------CREATE PROFESSOR------------------------------------------------------*/
        // add professor to db via form
        [HttpPost]
        public IActionResult AddProfessor(ProfessorModel newProf)
        {
            if(ModelState.IsValid)
            {
                _context.professors.Add(newProf);
                _context.SaveChanges();
                return RedirectToAction("Index"); 
            } else 
            {
                return Content("Invalid Model");
            }
        }
        // display form to add professor to db
        public IActionResult DisplayCreateForm()
        {
            return View("CreateForm");
        }
/*------------------------------------------------------UPDATE PROFESSOR------------------------------------------------------*/
        // update professor in db via form
        [HttpPost]
        public IActionResult UpdateProfessor(ProfessorModel updateProf)
        {
            ProfessorModel matchingProf = _context.professors.FirstOrDefault(prof => prof.id == updateProf.id);
            if(ModelState.IsValid)
            {
                matchingProf.lastName = updateProf.lastName;
                _context.SaveChanges();
                return RedirectToAction("Index"); 
            } else 
            {
                return Content("Invalid Model");
            }
        }
        // display form to update professor in db
        public IActionResult DisplayUpdateFrom()
        {
            return View("Update");
        }
/*------------------------------------------------------CREATE COURSE------------------------------------------------------*/
        // add course to db via form
        [HttpPost]
        public IActionResult AddCourse(CourseModel newCrse, int professorID)
        {
            // MUST USE INCLUDE SO THAT NEW COURSE CAN BE ADDED TO MATCHING PROFESSOR
            ProfessorModel matchingProf = _context.professors.Include(prof => prof.currentCourses).FirstOrDefault(prof => prof.id == professorID);
            if(matchingProf != null)
            {
                if(ModelState.IsValid)
                {
                    matchingProf.currentCourses.Add(newCrse);
                    _context.courses.Add(newCrse);
                    _context.SaveChanges();
                    return RedirectToAction("Index"); 
                } else 
                {
                    return Content("Invalid Model");
                }                
            } else
            {
                return Content("No matching professor Found");
            }
        }
        // display form to update course in db
        public IActionResult DisplayCourseCreateForm(int professorID)
        {
            return View("CreateCourse");
        }

    }
}