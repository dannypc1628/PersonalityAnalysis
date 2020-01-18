using PersonalityAnalysis.Models;
using PersonalityAnalysis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Service
{
    public class ResponseImportanceSortService
    {
        private readonly IRepository<ResponseImportanceSort> _responseImportanceSortRepository;

        public ResponseImportanceSortService(IUnitOfWork unitOfWork)
        {
            _responseImportanceSortRepository = new Repository<ResponseImportanceSort>(unitOfWork);
        }

        public void Add(ResponseImportanceSort responseImportanceSort)
        {
            _responseImportanceSortRepository.Create(responseImportanceSort);
        }
    }
}