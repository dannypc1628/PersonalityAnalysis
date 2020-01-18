using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonalityAnalysis.Models;
using PersonalityAnalysis.Models.ViewModels;
using PersonalityAnalysis.Repository;
using PersonalityAnalysis.Service;
using PersonalityAnalysis.Helper;

namespace PersonalityAnalysis.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
             
        private readonly StudentService _studentService;
        private readonly LectureService _lectureService;
        private readonly UnitOfWork _unitOfWork;

        public StudentsController()
        {
            _unitOfWork = new UnitOfWork();
            _studentService = new StudentService(_unitOfWork);
            _lectureService = new LectureService(_unitOfWork);
        }

        // GET: Students
        public ActionResult All()
        {
            var student = _studentService.All();
            return View("Index",student);
        }

        public ActionResult Lecture(Guid key)
        {

            Lecture lecture = _lectureService.GetLecture(key);
            if (lecture == null)
                RedirectToRoute("Default");
            var student = _studentService.LectureStudents(lecture);
            return View("Index", student);
        }

        

        // GET: Students/Create
        [AllowAnonymous]
        public ActionResult Create(string key)
        {

            ViewBag.Lecture_Key = key;

            return View();
        }

        // POST: Students/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                Lecture thisLecture = _lectureService.GetLecture(student.Lecture_Key);
                if (thisLecture != null)
                {
                    Student s = new Student
                    {
                        Lecture_ID = thisLecture.ID,
                        Compeny = student.Compeny,
                        Employee_ID = student.Employee_ID,
                        Birthday = student.Birthday,
                        Email = student.Email,
                        Gender = (int)student.Gender,
                        Name = student.Name,
                        Phone_Number = student.Phone_Number,
                        CreateDate = DateTime.Now,
                        Key =Guid.NewGuid()
                    };
                    _studentService.Add(s);
                    _unitOfWork.Commit();

                    return RedirectToAction("Index1", "Question", new { ID = 1, key = s.Key });
                }               
                
            }
            ViewBag.Lecture_Key = student.Lecture_Key;
            return View(student);
        }

       
    }
}
