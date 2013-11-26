using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace NumberGuessingGame.Models
{
    public class SecretNumber
    {
        // max number of guesses
        public const int MaxNumberOfGuesses = 7;

        // stores secret number
        private int? m_secretNumber;

        // list of all previously guessed numbers
        private List<GuessedNumber> m_guessedNumbers;

        // last guessed number
        private GuessedNumber m_lastGuessedNumber;

        // constructor
        public SecretNumber()
        {
            m_guessedNumbers = new List<GuessedNumber>();
            this.Initialize();
        }

        // initialize new game
        public void Initialize()
        {
            m_guessedNumbers.Clear();
            Random secretNumber = new Random();
            Number = secretNumber.Next(1, 101);
        }
        
        // encapsulates secret number. return null if guesses can be made, otherwise return secret number 
        public int? Number
        {
            get
            {
                if (CanMakeGuess)
                {
                    return null;
                }
                else
                {
                    return m_secretNumber;
                }
            }
            private set
            {
                m_secretNumber = value;
            }
        }

        public bool CanMakeGuess
        {
            get
            {
                if (Count < MaxNumberOfGuesses && LastGuessedNumber.Number != m_secretNumber)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }   
        }

        public GuessedNumber LastGuessedNumber
        {
            get
            {
                return m_lastGuessedNumber;
            }
        }

        public int Count
        {
            get 
            {
                return m_guessedNumbers.Count;
            }
        }

        public IList<GuessedNumber> GuessedNumbers
        {
            get
            {
                List<GuessedNumber> guessedNumbers = m_guessedNumbers;
                ReadOnlyCollection<GuessedNumber> readOnlyGuessedNumbers = guessedNumbers.AsReadOnly();
                return readOnlyGuessedNumbers;
            }
        }

        public Outcome MakeGuess(int guess)
        {
            // kontrollerar att gissningen är mellan 1 och 100, om inte kastas undantag
            if (guess > 0 && guess <= 100)
            {
                var currentGuess = new GuessedNumber();
                currentGuess.Number = guess;

                if (!CanMakeGuess)
                {
                    currentGuess.Outcome = Outcome.NoMoreGuesses;
                    return Outcome.NoMoreGuesses;
                }
                if (IsOldGuess(currentGuess))
                {
                    currentGuess.Outcome = Outcome.OldGuess;
                    return Outcome.OldGuess;
                }
                else if (currentGuess.Number == m_secretNumber)
                {     
                    currentGuess.Outcome = Outcome.Right;
                    m_lastGuessedNumber = currentGuess;
                    m_guessedNumbers.Add(currentGuess);

                    return Outcome.Right;
                }
                else if (currentGuess.Number < m_secretNumber)
                {
                    currentGuess.Outcome = Outcome.Low;
                    m_lastGuessedNumber = currentGuess;
                    m_guessedNumbers.Add(currentGuess);

                    return Outcome.Low;
                }
                else if (currentGuess.Number > m_secretNumber)
                {
                    currentGuess.Outcome = Outcome.High;
                    m_lastGuessedNumber = currentGuess;
                    m_guessedNumbers.Add(currentGuess);

                    return Outcome.High;
                }
                else
                {
                    return Outcome.Indefinite;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Heltal måste ligga mellan 1 och 100");
            }
        }

        public bool IsOldGuess(GuessedNumber guess)
        {
            var check = m_guessedNumbers.Find(obj => obj.Number == guess.Number);
            if (check.Number.HasValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}