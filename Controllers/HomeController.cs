using MVC_App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC_App.Controllers
{
    public class HomeController : Controller
    {
        int month;
        
        public ActionResult Index()
        {
            
            
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 && hour > 4  ? "Tere hommikust!" : "Tere päevast";
            ViewBag.Greeting = hour < 16 && hour > 12 ? "Tere päevast!" : "Tere õhtust";
            ViewBag.Greeting = hour > 16 && hour < 20 ? "Tere õhtust" : "Head ööd";
            ViewBag.Greeting = hour > 20 && hour < 4 ? "Head ööd" : "Tere hommikust!";
            string[] peod = new string[12] { "Uus aasta", "Taasiseseisvumispäev, Eesti Vabariigi aastapäev"
                , "Naistepäev", "Kosmonautikapäev", "Emadepäev", "Lastepäev", "Ülemaailmne vaalade ja delfiinide päev"
                , "Naistepäev", "Taganttuule päev", "Viisakate inimeste päev", "Rahvusvaheline loomade mälestuspäev", "Uus aasta" };
            
            month = DateTime.Now.Month;
            /*string pidu ="";
            if (DateTime.Now.Month == 1){pidu = "Uus aasta";}
            else if (DateTime.Now.Month == 2){pidu = "Taasiseseisvumispäev, Eesti Vabariigi aastapäev";}
            else if (DateTime.Now.Month == 3){pidu = "Naistepäev";}
            else if (DateTime.Now.Month == 4){pidu = "Kosmonautikapäev";}
            else if (DateTime.Now.Month == 5){pidu = "Emadepäev";}
            else if (DateTime.Now.Month == 6){pidu = "Lastepäev";}
            else if (DateTime.Now.Month == 7){pidu = "Ülemaailmne vaalade ja delfiinide päev";}
            else if (DateTime.Now.Month == 8){pidu = "Naistepäev";}
            else if (DateTime.Now.Month == 9){pidu = "Taganttuule päev";}
            else if (DateTime.Now.Month == 10){pidu = "Viisakate inimeste päev";}
            else if (DateTime.Now.Month == 11){pidu = "Rahvusvaheline loomade mälestuspäev";}
            else if (DateTime.Now.Month == 12){pidu = "Uus aasta";}*/
            ViewBag.Message = $"Ootan sind minu peoale! {peod[month - 1]} Palun tule!!!";
            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        { 

            return View();
        }
        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                ViewBag.Greeting = guest.Email;
                return View("Aitäh", guest);
                
            }
            else
            {
                return View();
            }
        }

        private void E_mail(Guest guest)
        {
            string[] peod = new string[12] { "Uus aasta", "Taasiseseisvumispäev, Eesti Vabariigi aastapäev"
                , "Naistepäev", "Kosmonautikapäev", "Emadepäev", "Lastepäev", "Ülemaailmne vaalade ja delfiinide päev"
                , "Naistepäev", "Taganttuule päev", "Viisakate inimeste päev", "Rahvusvaheline loomade mälestuspäev", "Uus aasta" };

            month = DateTime.Now.Month;
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "programmeeriminetthk2@gmail.com";
                WebMail.Password = "2.kuursus tarpv20";
                WebMail.From = "programmeeriminetthk2@gmail.com";
                WebMail.Send("artem1223148@gmail.com", "Vastus kutsele",guest.Name + " vastas " + ((guest.WillAttend ?? false) ?
                    "tuleb peole, " + peod[month - 1] + "  " : " ei tule peole"));
                WebMail.Send(guest.Email, "Vastus kutsele", guest.Name + " vastas " + ((guest.WillAttend ?? false) ?
                    "tuleb peole, "+ peod[month - 1] + "  ": " ei tule peole"));
                ViewBag.Message = "Kiri on saatnud";
            }
            catch (Exception)
            {

                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!!!";
            }
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        GuestContext db = new GuestContext();
        [Authorize]
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        public ActionResult Accept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == true);
            return View(guests);
        }
    }
}