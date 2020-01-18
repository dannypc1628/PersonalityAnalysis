using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PersonalityAnalysis.Helper;
using PersonalityAnalysis.Models;
using PersonalityAnalysis.Models.ViewModels;
using PersonalityAnalysis.Repository;
using PersonalityAnalysis.Service;

namespace PersonalityAnalysis.Controllers
{
    [AuthorizeAttribute]
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly UnitOfWork _unitOfWork;

        public UsersController()
        {
            _unitOfWork = new UnitOfWork();
            _userService = new UserService(_unitOfWork);
        }

        

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel user,bool guest = false)
        {
            if (ModelState.IsValid)
            {
                User u = _userService.Find(user.ID, user.Password);
                if (u != null)
                {
                    var cookie = UserHelper.SetCookie(u.ID, u.Name);

                    Response.Cookies.Add(cookie);                    
                    return RedirectToAction("Index", "Lectures");
                }
                
            }else if (guest==true)
            {
                var cookie = UserHelper.SetCookie("guest", "訪客");

                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Lectures");
            }
            ModelState.AddModelError("", "無效的帳號或密碼。");
            return View(user);
        }

        public ActionResult Logout()
        {
            //Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        

        //GET: Users/Edit/5
        
        public ActionResult Edit()
        {
            string ID =  UserHelper.GetUserID();
            if (String.IsNullOrEmpty(ID)||String.IsNullOrWhiteSpace(ID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetByAccount(ID);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserEditViewModel userEdit = new UserEditViewModel
            {
                ID = user.ID,
                Name = user.Name,
                Password = user.Password,
                SecendPassword = user.Password
            };
            return View(userEdit);
        }

        //// POST: Users/Edit/5
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditViewModel user)
        {
            if (ModelState.IsValid)
            {
                User oldUser = _userService.GetByAccount(user.ID);
                if (oldUser != null)
                {
                    User newUserData = new User
                    {
                        ID = oldUser.ID,
                        Name = user.Name,
                        Password = user.Password
                    };
                    _userService.Edit(oldUser, newUserData);
                    _unitOfWork.Commit();
                    return RedirectToAction("Index","Lectures");
                }                
            }
            return View(user);
        }

        
    }
}
