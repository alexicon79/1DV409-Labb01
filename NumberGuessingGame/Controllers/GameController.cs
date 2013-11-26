using NumberGuessingGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NumberGuessingGame.Controllers
{
    public class GameController : Controller
    {
        SecretNumber m_secretNumber;
        private static string gameSession = "KeepGuessing";

        //
        // GET: /Game/

        public ActionResult Index()
        {
            this.New();
            return View("Index");
        }

        // GET: /Game/New()
        public ActionResult New()
        {
            Session[gameSession] = null;
            m_secretNumber = null;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(int guess)
        {

            if (Session[gameSession] != null)
            {
                m_secretNumber = Session[gameSession] as SecretNumber;
            }
            else
            {
                m_secretNumber = new SecretNumber();
            }

            m_secretNumber.MakeGuess(guess);

            Session[gameSession] = m_secretNumber;

            return View("Index", m_secretNumber);
        }


    }
}
