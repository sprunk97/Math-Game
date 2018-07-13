using System.Collections.Generic;
using MathGame.Enums;

namespace MathGame
{
    class Game
    {
        public List<Equation> equations { get; private set; } = new List<Equation>();
        public double Score { get; private set; }
        public int RightAnswers { get => rightAnswers; set => rightAnswers = value; }
        public int WrongAnswers { get => wrongAnswers; set => wrongAnswers = value; }
        public int Minutes { get => minutes; set => minutes = value; }

        private int minutes;
        private int rightAnswers = 0;
        private int wrongAnswers = 0;

        public Game(int duration)
        {
            this.minutes = duration;
        }

        /// <summary>
        /// Adds new equation to list
        /// </summary>
        /// <param name="digitsAmount"></param>
        /// <param name="operation"></param>
        public void Add(int digitsAmount, Operations operation)
        {
            equations.Add(new Equation(digitsAmount, operation));
        }

        /// <summary>
        /// Compares exact result with user's one
        /// </summary>
        /// <param name="userResult">User answer</param>
        public void CheckAnswer(double userResult)
        {
            equations[equations.Count - 1].IsCorrect(userResult);
        }

        /// <summary>
        /// Sets and returns score
        /// </summary>
        /// <returns></returns>
        public double CountScore()
        {
            foreach(var equ in equations)
                if (equ != equations[equations.Count - 1])
                    if (equ.Correct)
                        rightAnswers++;
                    else wrongAnswers++;

            Score = ((double)rightAnswers - wrongAnswers) / minutes;
            return Score;
        }

    }
}
