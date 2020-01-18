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
    public class QuestionController : Controller
    {
        
        private readonly QuestionService _questionService;
        private readonly ResponseService _responseService;
        private readonly ResponseGoalService _responseGoalService;
        private readonly ResponseImportanceSortService _responseImportanceSortService;
        private readonly ResultService _resultService;
        private readonly StudentService _studentService;
        private readonly UnitOfWork _unitOfWork;

        public QuestionController()
        {
            _unitOfWork = new UnitOfWork();
            _questionService = new QuestionService(_unitOfWork);
            _responseService = new ResponseService(_unitOfWork);
            _responseGoalService = new ResponseGoalService(_unitOfWork);
            _responseImportanceSortService = new ResponseImportanceSortService(_unitOfWork);
            _resultService = new ResultService(_unitOfWork);
            _studentService = new StudentService(_unitOfWork);
        }

        public ActionResult Index1(int ID, string key)
        {
            ViewBag.Questionnaire = _questionService.GetQuestionnaire(ID);
            if (ID == 1)
            {
                var List = _questionService.GetQuestionsTitleList(ID);
                ViewBag.First_Goal_ID = new SelectList(List, "ID", "Name");
            }
            ViewBag.Key = key;
            List<QuestionViewModel> list = _questionService.GetQuestions(ID);
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index1(Guid key, int QuestionnaireID, ResponseViewModel[] responses,ResponseGoal goal)
        {

            if (ModelState.IsValid)
            {
                Student student = _studentService.GetStudentByKey(key);
                if (student != null)
                {
                    foreach (var item in responses)
                    {
                        _responseService.Add(new Response
                        {
                            Question_ID = item.Question_ID,
                            Response_ID = item.Response_ID,
                            Student_ID = student.ID
                        });
                    }
                    _responseGoalService.Add(new ResponseGoal {
                        Student_ID=student.ID,
                        First_Goal_ID = goal.First_Goal_ID,
                        Why = goal.Why
                    });
                    _unitOfWork.Commit();
                    Question next = _questionService.NextQuestionnaire(QuestionnaireID);

                    if (next == null)
                        return RedirectToAction("Finish", new { key = key });
                    else
                    {
                        return RedirectToAction("Index", new { ID = next.ID, key = key });
                    }
                }
            }
            ViewBag.Questionnaire = _questionService.GetQuestionnaire(QuestionnaireID);
            return View();
        }

        // GET: Question
        public ActionResult Index(int ID, string key)
        {
            if (ID == 1)
                return RedirectToRoute("Default");
            Question Questionnaire = _questionService.GetQuestionnaire(ID);
            if (Questionnaire != null)
                ViewBag.Questionnaire = Questionnaire;
            ViewBag.Key = key;
            List<QuestionViewModel> list = _questionService.GetQuestions(ID);
            return View(list);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index( int ID,Guid key, ResponseViewModel[] responses)
        {
            if(ID==1)
            {
                return RedirectToRoute("Default");
            }
            if (ModelState.IsValid)
            {
                Student student = _studentService.GetStudentByKey(key);
                if (student != null)
                {

                    foreach (var item in responses)
                    {
                        _responseService.Add(new Response
                        {
                            Question_ID = item.Question_ID,
                            Response_ID = item.Response_ID,
                            Student_ID = student.ID

                        });
                    }
                    _unitOfWork.Commit();
                    Question next = _questionService.NextQuestionnaire(ID);
                    List<Question> questionnaireList = _questionService.GetQuestionnaireList();
                    int len = questionnaireList.Count;
                    if(questionnaireList[len-1].ID==next.ID)
                        return RedirectToAction("ImportanceSort", new { key = key,ID=next.ID });
                    if (next == null)
                       return RedirectToAction("Result", new { key =key });
                    else
                    {
                       return RedirectToAction("Index", new { ID = next.ID, key = key });
                    }
                }
            }
            return View();
        }

        public ActionResult ImportanceSort(Guid key,int ID)
        {          
            Question Questionnaire = _questionService.GetQuestionnaire(ID);
            if (Questionnaire != null)
                ViewBag.Questionnaire = Questionnaire;
            else
                return RedirectToRoute("Default");
            Student student = _studentService.GetStudentByKey(key);
            if (student == null)
                return RedirectToRoute("Default");

            ViewBag.Key = key;
            List<SelectListItem> numbers = new List<SelectListItem>();
            for (int i = 1; i < 13; i++)
                numbers.Add(new SelectListItem {Text=i.ToString(), Value=i.ToString() });
            ViewBag.Numbers = numbers;
            List<QuestionViewModel> list = _questionService.GetQuestions(ID);
            ViewBag.list = list;
            ViewBag.ID = ID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportanceSort(Guid key, int ID, 
            [Bind(Exclude = "Student_ID",Include = "Response_ID,Number")]
                ResponseImportanceSort[] responses)
        {
            if (ModelState.IsValid)
            {
                Student student = _studentService.GetStudentByKey(key);
                if (student == null)
                    return RedirectToRoute("Default");
                foreach(var item in responses)
                {
                    _responseImportanceSortService.Add(new ResponseImportanceSort
                    {
                        Number = item.Number,
                        Response_ID = item.Response_ID,
                        Student_ID = student.ID
                    });                   
                }
                _unitOfWork.Commit();

                return RedirectToAction("Result", new { key = key });
            }
            else
                return View();
        }

        public ActionResult Result(Guid  key)
        {
            Student student = _studentService.GetStudentByKey(key);
            ResultHelper helper = new ResultHelper();
            Result_View result = helper.CalculateResult(student, _responseGoalService, _questionService);

            return View(result);
        }

     
    }
}
