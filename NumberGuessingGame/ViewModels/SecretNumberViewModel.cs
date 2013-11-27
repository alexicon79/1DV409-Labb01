using NumberGuessingGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.ViewModels
{
    public class SecretNumberViewModel
    {
        [Required(ErrorMessage = "Please enter a number between 1 and 100")]
        [Range(1, 100, ErrorMessage = "You have to enter a number between 1 and 100")]
        public int UserGuess { get; set; }

        public int MaxNumberOfGuesses { get; set; }
        public GuessedNumber LastGuessedNumber { get; set; }
        public IList<GuessedNumber> GuessedNumbers { get; set; }
        public int? Number { get; set; }
        public bool CanMakeGuess { get; set; }

        public string GuessMessage
        {
            get
            {
                return "GuessMessage";
            }
        }

        public string ResultMessage
        {
            get
            {
                if (GuessedNumbers.Count == MaxNumberOfGuesses)
                {
                    return "No guesses left. The secret number was " + Number;
                }

                switch (LastGuessedNumber.Outcome)
                {
                    case Outcome.OldGuess:
                        return "Try again. You have already guessed on number " + LastGuessedNumber.Number;
                    case Outcome.Low:
                        return LastGuessedNumber.Number + " is too low";
                    case Outcome.High:
                        return LastGuessedNumber.Number + " is too high";
                    case Outcome.Indefinite:
                        return "";
                    case Outcome.Right:
                        return Number + " is correct!";
                    default:
                        return "";
                }

            }
        }

    }
}