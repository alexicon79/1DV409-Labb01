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
                if (!CanMakeGuess && LastGuessedNumber.Outcome == Outcome.Right)
                {
                    return "Congratulations, you got it!";
                }
                switch (GuessedNumbers.Count)
                {
                    case 0:
                        return "Welcome! Make your first guess...";
                    case 1:
                        return "Make your second guess...";
                    case 2:
                        return "Make your third guess...";
                    case 3:
                        return "Make your fourth guess...";
                    case 4:
                        return "Make your fifth guess...";
                    case 5:
                        return "Make your sixth guess...";
                    case 6:
                        return "Make your final guess...";
                    case 7:
                        return "Game over";
                    default:
                        return "Welcome";
                }
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
                    case Outcome.NoMoreGuesses:
                        return "No guesses left. The secret number was " + Number;
                    default:
                        return "";
                }

            }
        }

    }
}