using PersonalityAnalysis.Models;
using PersonalityAnalysis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Service
{
    public class ResponseService
    {
        private readonly IRepository<Response> _responseRepository;

        public ResponseService(IUnitOfWork unitOfWork)
        {
            _responseRepository = new Repository<Response>(unitOfWork);
        }

        public void Add(Response response)
        {
            _responseRepository.Create(response);
        }
    }
}