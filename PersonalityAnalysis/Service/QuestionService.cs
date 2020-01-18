using PersonalityAnalysis.Models;
using PersonalityAnalysis.Models.ViewModels;
using PersonalityAnalysis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Service
{
    public class QuestionService
    {
        private readonly IRepository<Question> _questionRepository;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _questionRepository = new Repository<Question>(unitOfWork);
        }

        public List<Question> GetQuestionnaireList()
        {
            return _questionRepository.Query(d => d.Parent_ID == 0).ToList();
        }

        public Question GetQuestionnaire(int questionID)
        {
            return _questionRepository.GetSingle(d => d.ID == questionID);
        }
        public Question NextQuestionnaire(int questionID)
        {
            Question now = _questionRepository.GetSingle(d=>d.ID==questionID);
            Question next = _questionRepository.Query(d => (d.Parent_ID == 0) && (d.Number > now.Number)).OrderBy(d=>d.Number).FirstOrDefault();
            return next;
        }

        public List<Question> GetQuestionsTitleList(int questionID)
        {
            var data = _questionRepository.Query(d => d.Parent_ID == questionID && d.Number<6);

            return data.ToList();
                       
        }

        public List<QuestionViewModel> GetQuestions(int questionID)
        {
            
            if (_questionRepository.Query(d => d.ID == questionID && d.Parent_ID == 0).Any())
            {
                var data = from d in _questionRepository.Query(d => d.Parent_ID == questionID)
                                                    select new QuestionViewModel
                                                    {
                                                        ID = d.ID,
                                                        Name = d.Name,
                                                        Number = d.Number,
                                                        SecendName = d.Second_Name
                                                    };
                List<QuestionViewModel> questions = data.ToList();
                int i = 0;
                
                foreach (var item in questions)
                {
                    var a   = GetQuestionsChild(item);
                    questions[i].Items = a;
                    i++;
                }
                return questions;
            }
            else
                return null;
        }
        private bool ChildAny(QuestionViewModel q)
        {
            return _questionRepository.Query(d => d.Parent_ID == q.ID).Any();
        }

        private QuestionViewModel[] GetQuestionsChild(QuestionViewModel q)
        {
            var a = _questionRepository.Query(d => d.Parent_ID == q.ID).OrderBy(d => d.Number)
                        .Select(d => new QuestionViewModel
                        {
                            ID = d.ID
                        ,
                            Name = d.Name
                        ,
                            Number = d.Number
                        }).ToArray();
            int i = 0;
            foreach(var item in a)
            {
                if(ChildAny(item))
                    a[i].Items = GetQuestionsChild(item);
                i++;
            }
            return a;
        }

        
           
    }
}