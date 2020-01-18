using PersonalityAnalysis.Models;
using PersonalityAnalysis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Service
{
    public class ResultService
    {
        private readonly IRepository<Result> _repository;

        public ResultService(IUnitOfWork unitOfWork)
        {
            _repository = new Repository<Result>(unitOfWork);
        }

        public void Add(Result result)
        {
            _repository.Create(result);
        }

        public Result GetResult(Student student)
        {
            return _repository.Query(d => d.Student_ID == student.ID).FirstOrDefault();
        }
    }
}