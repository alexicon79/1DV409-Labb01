using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumberGuessingGame.Models;
using NumberGuessingGame.ViewModels;

namespace NumberGuessingGame.Controllers
{
    public class GameController : Controller
    {
        // stores an object of type SecretNumber (model)
        SecretNumber m_secretNumber;
        
        // stores an object of type SecretNumberViewModel (view model)
        SecretNumberViewModel m_secretNumberViewModel;

        // string containing the name of current session
        private static string SECRET_NUMBER = "SecretNumber";

        //
        // GET: /Game/

        public ActionResult Index()
        {
            // instantiate ViewModel
            m_secretNumberViewModel = new SecretNumberViewModel();
            
            // if session is empty, instantiate SecretNumber and store in session 
            if (Session[SECRET_NUMBER] == null)
            {
                m_secretNumber = new SecretNumber();
                Session[SECRET_NUMBER] = m_secretNumber;
            }
            // else use object already stored in session.
            else
            {
                m_secretNumber = Session[SECRET_NUMBER] as SecretNumber;
            }

            // populate view model with relevant data from model
            m_secretNumberViewModel = this.PopulateViewModel(m_secretNumberViewModel, m_secretNumber);

            return View("Index", m_secretNumberViewModel);
        }

        [HttpPost]
        public ActionResult Index(SecretNumberViewModel m_secretNumberViewModel)
        {
            if (Session[SECRET_NUMBER] == null)
            {
                return View("SessionExpired");
            }
            else
            {
                m_secretNumber = Session[SECRET_NUMBER] as SecretNumber;
            }

            if (ModelState.IsValid)
            {
                // guess the secret number
                m_secretNumber.MakeGuess(m_secretNumberViewModel.UserGuess);

                // populate view model with updated data from model
                m_secretNumberViewModel = this.PopulateViewModel(m_secretNumberViewModel, m_secretNumber);

                return View("Index", m_secretNumberViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        // GET: /Game/New()
        public ActionResult New()
        {
            // clear session
            Session[SECRET_NUMBER] = null;

            // clear model
            m_secretNumber = null;

            // clear view model
            m_secretNumberViewModel = null;

            return RedirectToAction("Index");
        }

        // assigns data from model to view model
        public SecretNumberViewModel PopulateViewModel(SecretNumberViewModel viewModel, SecretNumber model)
        {
            viewModel.MaxNumberOfGuesses = SecretNumber.MaxNumberOfGuesses;
            viewModel.LastGuessedNumber = model.LastGuessedNumber;
            viewModel.GuessedNumbers = model.GuessedNumbers;
            viewModel.Number = model.Number;
            viewModel.CanMakeGuess = model.CanMakeGuess;
            
            return viewModel;
        }

    }
}
