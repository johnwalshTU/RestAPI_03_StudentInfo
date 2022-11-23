using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentExamInfo.Models;
using System.Xml.Linq;

namespace StudentExamInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        //Keeping it simple here
        //Should use Repository but will just use Static list here as I just want to demonstrate
        //using routes on actions
        private static List<StudentExam> ExamResults = new List<StudentExam>()
        {
            new StudentExam{ StudentID = "X001122",  Name= "Pete",  Module="Maths",   Grade = 70, MatureStudent=true},
            new StudentExam{ StudentID = "X334455",  Name= "John",  Module="Maths",   Grade = 45, MatureStudent=false},
            new StudentExam{ StudentID = "X112233",  Name= "Mary",  Module="Science", Grade = 80, MatureStudent=true},
            new StudentExam{ StudentID = "X667788",  Name= "Mike",  Module="Science", Grade = 20, MatureStudent=false},
            new StudentExam{ StudentID = "X445566",  Name= "Ann",   Module="Irish",   Grade = 95, MatureStudent=true},
        };


        // ...........................................................
        /* EXAMPLE ROUTES TO USE BELOW
         * 
         *    ROUTE                                       METHOD                    Explanation
         *    -------                                    ----------                --------------
        * GET api/Exam/Students/all                       GetAll()                  get all Students                
        * GET api/Exam/Module/Maths                       GetAllForModule(Maths)    get info where module is math    
        * GET api/Exam/Students/Mature/true               GetForMature(true)        get Students that are mature     
        * GET api/Exam/Students/Mature/true/MinGrade/40   GetForMinGrade(true,40)   get Students that are mature and have grade above 40  GetStudentsWithMinGrades()
         

        //e.g 
        // https://localhost:7204/api/Exam/Students/all
        // https://localhost:7204/api/Exam/Module/Maths
        // https://localhost:7204/api/Exam/Students/Mature/true
        // https://localhost:7204/api/Exam/Students/Mature/true/MinGrade/40
        // ...........................................................
        */



        //  GET ae.g Students/all            
        [HttpGet("Students/all")]
        public ActionResult<IEnumerable<StudentExam>> GetAll()
        {
            //return Ok(_db.GetStocks().ToList());                      // 200 OK, listings serialized in response body
            return ExamResults.OrderBy(r => r.StudentID).ToList();      //can use this either!
        }


        //  GET e.g  /Module/Maths       
        [HttpGet("Module/{moduleName:alpha}")]                                            // {parameter:constraint}
        public ActionResult<IEnumerable<StudentExam>> GetAllForModule(string moduleName)
        {
            // LINQ query, find matching module (case-insensitive) 
            var moduleInfo = ExamResults.Where(r => r.Module.ToUpper() == moduleName.ToUpper());
            if (moduleInfo.Count() == 0)
            {
                return NotFound();                                                  // 404
            }
           
            return Ok(moduleInfo);          // 200 OK, StudentExam serialized in response body
            //return moduleInfo.ToList();   //can use this either!
        }


        //  GET  e.g /Students/Mature/true)
        [HttpGet("Students/Mature/{mature:bool}")]                                                // {parameter:constraint}
        public ActionResult<IEnumerable<string>> GetForMature(bool mature)
        {
            // LINQ query, find matching city (case-insensitive) or default value (null) if none matching
            var students = ExamResults.Where(r => r.MatureStudent == mature).Select(s => s.Name);
            if (students.Count() == 0)
            {
                return NotFound();                                                  // 404
            }

            //return Ok(students);      // 200 OK, string list serialized in response body
            return students.ToList();   //can use this either
        }


        //  GET e.g  /Students/Mature/true/MinGrade/40) 
        [HttpGet("Students/Mature/{mature:bool}/MinGrade/{minGrade}")]                                                
        public ActionResult<IEnumerable<string>> GetForMinGrade(bool mature, int minGrade)
        {
            // LINQ query, find matching city (case-insensitive) or default value (null) if none matching
            var students = ExamResults.Where(r => r.MatureStudent == mature && r.Grade >= minGrade)
                                       .Select(s => s.Name);
            if (students.Count() == 0)
            {
                return NotFound();                                                  // 404
            }

            //return Ok(students);       // 200 OK, string list serialized in response body
            return students.ToList();    //can use this either
        }
    }
}
