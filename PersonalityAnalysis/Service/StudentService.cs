using PersonalityAnalysis.Models;
using PersonalityAnalysis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Service
{
    public class StudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _studentRepository = new Repository<Student>(unitOfWork);
        }

        public void Add(Student student)
        {
            _studentRepository.Create(student);
        }

        public Student GetStudentByKey(Guid key)
        {
            return _studentRepository.GetSingle(d => d.Key == key);
        }

        public Student GetStudentByID(int ID)
        {
            return _studentRepository.GetSingle(d => d.ID == ID);
        }

        public List<Student> LectureStudents(Lecture lecture)
        {
            return _studentRepository.Query(d => d.Lecture_ID == lecture.ID).ToList();

        }

        public List<Student> All()
        {
            return _studentRepository.All().ToList();
        }
    }
}