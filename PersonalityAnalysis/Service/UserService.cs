using PersonalityAnalysis.Models;
using PersonalityAnalysis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalityAnalysis.Service
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            _userRepository = new Repository<User>(unitOfWork);
        }

        public User Find(string Account,string Password)
        {
            return _userRepository.GetSingle(d => d.ID == Account && d.Password == Password);
        }

        public User GetByAccount(string Account)
        {
            return _userRepository.GetSingle(d => d.ID == Account );
        }

        public void Edit(User old,User newData)
        {
            old.Name = newData.Name;
            old.Password = newData.Password;
        }
    }
}