using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using PersonalityAnalysis.Helper;
using PersonalityAnalysis.Models;
using PersonalityAnalysis.Models.Enum;
using PersonalityAnalysis.Repository;
using PersonalityAnalysis.Service;

namespace PersonalityAnalysis.Controllers
{
    [Authorize]
    public class LecturesController : Controller
    {
        private readonly LectureService _lectureService;
        private readonly StudentService _studentService;
        private readonly UnitOfWork _unitOfWork;

        public LecturesController()
        {
            _unitOfWork = new UnitOfWork();
            _lectureService = new LectureService(_unitOfWork);
            _studentService = new StudentService(_unitOfWork);
        }

        // GET: Lectures
        public ActionResult Index()
        {
            return View(_lectureService.GetAll());
        }

        // GET: Lectures/Details/5
        public ActionResult Details(Guid? key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = _lectureService.GetLecture((Guid)key);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        public ActionResult StudentResult(Guid? key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = _lectureService.GetLecture((Guid)key);
            if (lecture == null)
            {
                return HttpNotFound();
            }

            ResultHelper resultHelper = new ResultHelper();
            IEnumerable<Result_View> results = resultHelper.GetByLecture(lecture);
            ViewBag.Lecture = lecture;
            return View(results);
        }

        public ActionResult ExportStudentResult(Guid? key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = _lectureService.GetLecture((Guid)key);
            if (lecture == null)
            {
                return HttpNotFound();
            }

            ResultHelper resultHelper = new ResultHelper();
            IEnumerable<ExportResult_View> results = resultHelper.ExportGetByLecture(lecture);

            var 學生列表 = results.Select(d => new
            {
                d.Employee_ID,
                d.Compeny,
                d.Student_Name,
                Gender = (GenderEnum)d.Gender,
                d.Birthday,
                d.Email,
                d.Phone_Number,
                d.Goal_Name,
                d.D,
                d.I,
                d.S,
                d.C,
                d.Ability,
                d.Horizontal_Score,
                d.Vertical_Score,
                d.T1_Name,
                d.T2_Name,
                d.T3_Name

            });
            XLWorkbook 活頁簿 = new XLWorkbook();
            IXLWorksheet 工作表1 = 活頁簿.Worksheets.Add("自訂工作表名稱");

            工作表1.Cell(1, 1).Value = "學號";
            工作表1.Cell(1, 2).Value = "系所";
            工作表1.Cell(1, 3).Value = "姓名";
            工作表1.Cell(1, 4).Value = "性別";
            工作表1.Cell(1, 5).Value = "生日";
            工作表1.Cell(1, 6).Value = "電子信箱";
            工作表1.Cell(1, 7).Value = "手機";
            工作表1.Cell(1, 8).Value = "圓夢新思維工作首要條件";
            工作表1.Cell(1, 9).Value = "D";
            工作表1.Cell(1, 10).Value = "I";
            工作表1.Cell(1, 11).Value = "S";
            工作表1.Cell(1, 12).Value = "C";
            工作表1.Cell(1, 13).Value = "業務能力";
            工作表1.Cell(1, 14).Value = "橫座標";
            工作表1.Cell(1, 15).Value = "縱座標";
            工作表1.Cell(1, 16).Value = "最重要";
            工作表1.Cell(1, 17).Value = "次重要";
            工作表1.Cell(1, 18).Value = "重要";



            工作表1.Cell(2, 1).InsertData(學生列表);
            string FileName = "StudentResult.xlsx";
            string FileUrl = Server.MapPath("~/XML/" + FileName);
            活頁簿.SaveAs(FileUrl);
            return File(FileUrl, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{lecture.Name}學生資料.xlsx");
        }

        public ActionResult ResultDetail(int ID)
        {
            Student student = _studentService.GetStudentByID(ID);

            return RedirectToAction("Result", "Question", new { key = student.Key });
        }

        // GET: Lectures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lectures/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                _lectureService.Add(lecture.Name);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(lecture);
        }

        // GET: Lectures/Edit/5
        public ActionResult Edit(Guid? key)
        {
            if (key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = _lectureService.GetLecture((Guid)key);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // POST: Lectures/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,Name,Create_Date")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                Lecture oldLecture = _lectureService.GetLecture(lecture.Key);
                _lectureService.Edit(oldLecture, lecture);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }

  
    }
}
