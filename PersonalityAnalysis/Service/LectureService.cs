using PersonalityAnalysis.Models;
using PersonalityAnalysis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Service
{
    public class LectureService
    {
        private readonly IRepository<Lecture> _lectureRepository;

        public LectureService(IUnitOfWork unitOfWork)
        {
            _lectureRepository = new Repository<Lecture>(unitOfWork);
        }

        public void Add(string name)
        {
            _lectureRepository.Create(new Lecture
            {
                Name = name,
                Create_Date = DateTime.Now,
                Key = Guid.NewGuid()
            });
        }

        public Lecture GetLecture(Guid Key)
        {
            return _lectureRepository.GetSingle(d => d.Key == Key);
        }

        public List<Lecture> GetAll()
        {
            return _lectureRepository.All().OrderByDescending(d=>d.Create_Date).ToList();
        }       

        public void Edit(Lecture oldLecture, Lecture newLecture)
        {
            oldLecture.Name = newLecture.Name;            
        }

    }
}