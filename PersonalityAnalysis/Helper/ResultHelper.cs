using PersonalityAnalysis.Models;
using PersonalityAnalysis.Models.ViewModels;
using PersonalityAnalysis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Helper
{
    public class ResultHelper
    {
        private DBModel db = new DBModel();

        public int GetDISC_Type(Q2ResultViewModel q2)
        {
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            pairs.Add("D", q2.D);
            pairs.Add("I", q2.I);
            pairs.Add("S", q2.S);
            pairs.Add("C", q2.C);
            var first = pairs.OrderByDescending(d => d.Value).FirstOrDefault();
            int number = 0;
            switch(first.Key){
                case "D":number = 1;
                    break;
                case "I":number = 2;
                    break;
                case "S":number = 3;
                    break;
                case "C":number = 4;
                    break;
            }
            
            return number;
        }

        public Q2ResultViewModel GetStudentQ2Result(Student student, Question Questionnaire)
        {
            var Q2 = db.Response_View
                        .Where(d => d.學生編號 == student.ID && d.試卷編號 == Questionnaire.ID);
            Q2ResultViewModel q2 = new Q2ResultViewModel() { D = 0, I = 0, S = 0, C = 0 };
            foreach (var item in Q2)
            {
                var scroe = CalculateScore(item.反轉, item.選項編號);
                switch (scroe)
                {
                    case 1:
                        q2.D++;
                        break;
                    case 2:
                        q2.I++;
                        break;
                    case 3:
                        q2.S++;
                        break;
                    case 4:
                        q2.C++;
                        break;
                }

            }
            q2.Number = GetDISC_Type(q2);

            return q2;
        }
        public int GetStudentQ3Result(Student student, Question Questionnaire)
        {
            var Q3 = db.Response_View.Where(d => d.學生編號 == student.ID && d.試卷編號 == Questionnaire.ID);
            int result = 0;
            foreach (var item in Q3)
            {
                result = result + CalculateScore(item.反轉, item.選項編號);
            }
            return result;
        }
        public Q4ResultViewModel GetStudentQ4Result(Student student, Question Questionnaire)
        {
            var Q4 = db.Response_View
                        .Where(d => d.學生編號 == student.ID && d.試卷編號 == Questionnaire.ID);
            Q4ResultViewModel result = new Q4ResultViewModel() { 橫坐標 = 0, 縱坐標 = 0 };
            int i = 1;
            foreach (var item in Q4)
            {
                if (i < 11)
                {
                    result.橫坐標 = result.橫坐標 + CalculateScore(item.反轉, item.選項編號);
                }
                else
                {
                    result.縱坐標 = result.縱坐標 + CalculateScore(item.反轉, item.選項編號);
                }
                i++;
            }
            return result;
        }

        private int CalculateScore(bool 反轉, int number)
        {
            if (反轉 == false)
            {
                return number;
            }
            else
            {
                return 5 - number;
            }
        }

        public Result_View CalculateResult(Student student, ResponseGoalService _responseGoalService, QuestionService _questionService)
        {
            bool haveResult = db.Result.Where(d => d.Student_ID == student.ID).Any();
            if (haveResult == false)
            {

                List<Question> list = _questionService.GetQuestionnaireList();

                ResponseGoal goal = _responseGoalService.GetGoal(student);
                Q2ResultViewModel q2 = GetStudentQ2Result(student, list[1]);
                int q3 = GetStudentQ3Result(student, list[2]);
                Q4ResultViewModel q4 = GetStudentQ4Result(student, list[3]);
                int[] importanceTop3 = ResponseImportanceSort(student);

                Result result = new Result()
                {
                    Student_ID = student.ID,
                    Goal_ID = goal.First_Goal_ID,
                    DISC_Number =q2.Number,
                    D = q2.D,
                    I = q2.I,
                    S = q2.S,
                    C = q2.C,
                    Ability = q3,
                    Horizontal_Score = q4.橫坐標,
                    Vertical_Score = q4.縱坐標,
                    Top_1 = importanceTop3[0],
                    Top_2 = importanceTop3[1],
                    Top_3 = importanceTop3[2]

                };
                db.Result.Add(result);
                db.SaveChanges();
            }

            return db.Result_View.Where(d => d.Student_ID == student.ID).FirstOrDefault();
        }

        public Result_View GetByStusent(Student student)
        {
            var data = db.Result_View.Where(d => d.Student_ID == student.ID).FirstOrDefault();
            return data;
        }
        public IEnumerable<Result_View> GetByLecture(Lecture lecture)
        {
            List<int> students = db.Student.Where(d => d.Lecture_ID == lecture.ID).Select(d => d.ID).ToList();
            var data = db.Result_View.Where(d => students.Contains(d.Student_ID)).OrderBy(d=>d.DISC_Number);

            return data.ToList();
        }

        public IEnumerable<ExportResult_View> ExportGetByLecture(Lecture lecture)
        {
            List<int> students = db.Student.Where(d => d.Lecture_ID == lecture.ID).Select(d => d.ID).ToList();
            var data = db.ExportResult_View.Where(d => students.Contains(d.Student_ID)).OrderBy(d => d.DISC_Number);

            return data.ToList();
        }

        public int[] ResponseImportanceSort(Student student)
        {
            var data = from d in db.ResponseImportanceSort
                       where d.Student_ID == student.ID
                       orderby d.Number
                       select d.Response_ID;
            return data.Take(3).ToArray();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}