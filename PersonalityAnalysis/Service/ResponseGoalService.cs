using PersonalityAnalysis.Models;
using PersonalityAnalysis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Service
{
    public class ResponseGoalService
    {
        private readonly IRepository<ResponseGoal> _responseGoalRepository;

        public ResponseGoalService(IUnitOfWork unitOfWork)
        {
            _responseGoalRepository = new Repository<ResponseGoal>(unitOfWork);
        }

        public void Add(ResponseGoal responseGoal)
        {
            _responseGoalRepository.Create(responseGoal);
        }

        public ResponseGoal GetGoal(Student student)
        {
            return _responseGoalRepository.Query(d => d.Student_ID == student.ID).FirstOrDefault();
        }
    }
}