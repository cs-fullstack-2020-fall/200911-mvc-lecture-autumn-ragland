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
            // if a matching prof is found
            if(matchingProf != null)
            {
                // render view and pass matching prof
                return View("ViewCourse", matchingProf);
            } else 
            // if a matching prof is not found
            {
                // return error message
                return Content("No matching professor Found");                
            }
        }
/*------------------------------------------------------CREATE PROFESSOR------------------------------------------------------*/
        // add professor to db via form
        [HttpPost]
        public IActionResult AddProfessor(ProfessorModel newProf)
        {
            // if object passed to endpoint meets model validation
            if(ModelState.IsValid)
            {
                // add to db
                _context.professors.Add(newProf);
                _context.SaveChanges();
                // redirect to Index method
                return RedirectToAction("Index"); 
            } else 
            // if object does not meet model validation
            {
                // display form again with invalid info
                return View("CreateForm", newProf);
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
            // if object passed to endpoint meet model validation
            if(ModelState.IsValid)
            {
                // update properties of matching object and save changes
                matchingProf.lastName = updateProf.lastName;
                _context.SaveChanges();
                // redirect to Index method
                return RedirectToAction("Index"); 
            } else 
            // if object passed does not meet model validation
            {
                // display form again with invalid info
                return View("Update", updateProf);
            }
        }
        // display form to update professor in db
        public IActionResult DisplayUpdateFrom(int professorID)
        {
            ProfessorModel foundProfessor = _context.professors.FirstOrDefault(prof => prof.id == professorID);
            // if matching professor is found
            if(foundProfessor != null)
            {
                return View("Update", foundProfessor);                
            } else 
            // if matching professor is not found
            {
                return Content("Matching not found");
            }

        }
/*------------------------------------------------------CREATE COURSE------------------------------------------------------*/
        // add course to db via form
        [HttpPost]
        public IActionResult AddCourse(CourseModel newCrse)
        {
            // MUST USE INCLUDE SO THAT NEW COURSE CAN BE ADDED TO MATCHING PROFESSOR
            ProfessorModel matchingProf = _context.professors.Include(prof => prof.currentCourses).FirstOrDefault(prof => prof.id == newCrse.professorID);
            // if matching prof is found
            if(matchingProf != null)
            {
                // if object passes model validation
                if(ModelState.IsValid)
                {
                    // add to list and db
                    matchingProf.currentCourses.Add(newCrse);
                    _context.courses.Add(newCrse);
                    _context.SaveChanges();
                    return RedirectToAction("Index"); 
                } else 
                // if model doesn't pass model validation
                {
                    // display form again with invalid info
                    return View("CreateCourse", newCrse);
                }                
            } else
            // if matching prof is not found
            {
                return Content("No matching professor Found");
            }
        }
        // display form to update course in db
        public IActionResult DisplayCourseCreateForm(int professorID)
        {
            ViewData["profID"] = professorID;
            return View("CreateCourse");
        }

    }
}