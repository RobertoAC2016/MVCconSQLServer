using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using MVCconSQLServer.Models;
using MVCconSQLServer.Services;
using System.Diagnostics;

namespace MVCconSQLServer.Controllers
{
    public class HomeController : Controller
    {
        private database db;
        public HomeController(IConfiguration conf)
        {
            db = new database(conf);
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult<List<user>> Users()
        {
            var users = db.get_user_list();
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Save(user u)
        {
            if (u != null)
            {
                var status = db.save_new_user(u);
                if (status)
                    return RedirectToAction("Users");
                else
                    return RedirectToAction("Create");
            }
            else
                return RedirectToAction("Create");
        }
        public IActionResult Delete(user u)
        {
            db.delete_user(u);
            return RedirectToAction("Users");
        }
        public ActionResult<user> Details(user u)
        {
            user usr = db.get_user_details(u);
            return View(usr);
        }
        public ActionResult<user> Edit(user u)
        {
            user usr = db.get_user_details(u);
            return View(usr);
        }
        public IActionResult Update(user u)
        {
            db.update_user(u);
            return RedirectToAction("Users");
        }
    }
}