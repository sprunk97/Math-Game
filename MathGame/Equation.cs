using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    class Equation
    {
        public int Left { get; private set; }
        public int Right { get; private set; }
        public double Result { get; private set; }
        public bool Correct { get; private set; } = false;
        public double? UserResult { get; private set; }

        private readonly int digitsAmount;
        private readonly Operations operation;

        public Equation(int digitsAmount, Operations operation)
        {
            this.digitsAmount = digitsAmount;
            this.operation = operation;

            GenerateOperands();
            Perform();
        }

        /// <summary>
        /// Performs specific operation and sets exact result
        /// </summary>
        public void Perform()
        {
            switch (operation)
            {
                case Operations.Addition:
                    Result = Left + Right;
                    break;

                case Operations.Subtraction:
                    Result = Left - Right;
                    break;

                case Operations.Multiplication:
                    Result = (double)Left * Right;
                    break;

                case Operations.Division:
                    Result = (double)Left / Right;
                    break;
            }
        }

        /// <summary>
        /// Compares exact result with user's one
        /// </summary>
        /// <param name="userResult">User answer</param>
        /// <returns></returns>
        public bool IsCorrect(double userResult)
        {
            Result = Math.Round(Result, 3, MidpointRounding.AwayFromZero);
            userResult = Math.Round(userResult, 3, MidpointRounding.AwayFromZero);

            if (Result == userResult)
            {
                Correct = true;
                return true;
            }
            else
            {
                UserResult = userResult;
                return false;
            }
        }

        /// <summary>
        /// Generates left and right operand with spicified digits amount
        /// </summary>
        public void GenerateOperands()
        {
            Random random = new Random();

            switch (digitsAmount)
            {
                case 2:
                    Left = random.Next(10, 100);
                    Right = random.Next(10, 100);
                    break;
                case 3:
                    Left = random.Next(100, 1000);
                    Right = random.Next(100, 1000);
                    break;
                case 4:
                    Left = random.Next(1000, 10000);
                    Right = random.Next(1000, 10000);
                    break;
            }
        }

        public override string ToString()
        {
            String equation = "";

            if (Correct)
            {
                switch (operation)
                {
                    case Operations.Addition:
                        equation = $"{Left} + {Right} = {Result}";
                        break;

                    case Operations.Subtraction:
                        equation = $"{Left} - {Right} = {Result}";
                        break;

                    case Operations.Multiplication:
                        equation = $"{Left} * {Right} = {Result}";
                        break;

                    case Operations.Division:
                        equation = $"{Left} / {Right} = {Result}";
                        break;
                }
                return equation;
            }
            else
            {
                switch (operation)
                {
                    case Operations.Addition:
                        equation = $"{Left} + {Right} = {UserResult}({Result})";
                        break;

                    case Operations.Subtraction:
                        equation = $"{Left} - {Right} = {UserResult}({Result})";
                        break;

                    case Operations.Multiplication:
                        equation = $"{Left} * {Right} = {UserResult}({Result})";
                        break;

                    case Operations.Division:
                        equation = $"{Left} / {Right} = {UserResult}({Result})";
                        break;
                }
                return equation;
            }
        }
    }
}
