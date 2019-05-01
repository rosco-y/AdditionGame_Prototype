using System;
using System.Collections.Generic;
using System.Text;
// using System.Threading.Tasks;

namespace AddingGame
{
    class cAddingGame
    {
        List<decimal> _amounts;
        decimal _totalAmount;
        decimal _userAnswer;
        const int REPORTDIVIDERLEN = 40;
        const int NUMPROBLEMS = 10;
        String _sReportDivider;
        cCurrencyValue _randomValueGenerator;
        cLevel _levelManager;
        int _problemNo = 0;

        public cAddingGame()
        {
            _levelManager = new cLevel();
            _amounts = new List<decimal>();
            _sReportDivider = new string('#', REPORTDIVIDERLEN);
            _randomValueGenerator = new cCurrencyValue();
            setLevel();

            var result = Begin();
        }

        //private async Task<bool> Update(bool newGame = false, int delay = 1)
        bool consoleTextColored = false;
        bool Update(bool newGame= false, int delay = 1)
        {
            //const int MILLISECONDS = 1000;
            Console.Clear();
            Console.WriteLine($"################### Level {_levelManager.Level} ###################");

            Console.WriteLine(_sReportDivider);
            if (newGame)
            {
                Console.WriteLine("New Game: Add the Values in your Head!");
                Console.WriteLine(_sReportDivider);
            }
            else
            {
                Console.WriteLine(_sReportDivider);
                Console.Write(new string('.', _problemNo));

                // toggle the amount color each time, to make it easier to spot
                // especially helpful when a value is repeated.
                if (consoleTextColored)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                consoleTextColored = !consoleTextColored;
                Console.WriteLine($"{_randomValueGenerator.Amount:C}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(_sReportDivider);
            }

            PressTheAnyKey();
            //await Task.Delay(delay * MILLISECONDS); // int to milliseconds
            return true;

        }

        private static void PressTheAnyKey()
        {
            Console.Write("Press the AnyKey (or Q to Quit): ");
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.Key == ConsoleKey.Q)
                Environment.Exit(0);
        }

        void setLevel()
        {
            cLevelSettings level = _levelManager.Settings;

            /// the random value generator is an instance of cCurrencyValue.cs.
            /// Each currency member is a cRange object, with a max and a min.
            /// I'm leaving the min as 0 for now, so I'm only interested in setting
            /// the max values in the random value generator.
            _randomValueGenerator.Dollars.Max = level.Dollars;
            _randomValueGenerator.HalfDollars.Max = level.HalfDollars;
            _randomValueGenerator.Quarters.Max = level.Quarters;
            _randomValueGenerator.Dimes.Max = level.Dimes;
            _randomValueGenerator.Nickels.Max = level.Nickels;
            _randomValueGenerator.Pennies.Max = level.Pennies;

        }

        public bool Begin()
        // public async Task<bool> Begin()
        {
            //bool more = await Update(true, 3);
            bool more = Update(true, 3);
            while (more)
            {
                Next();
                //more = await Update();
                more = Update();
            }
            return more;
        }

        /// <summary>
        /// Generate a new random currency amount, add it to the
        /// total amount, and to the amounts list.
        /// </summary>
        /// <returns>
        /// The amount randomly generated.
        /// </returns>
        decimal Next()
        {
            Decimal retAmount = 0m;

            if (_problemNo++ < NUMPROBLEMS)
            {
                decimal amount = _randomValueGenerator.Next();
                _totalAmount += amount;
                _amounts.Add(amount);
                retAmount =  amount;
            }
            else
            {
                bool success = PromptUser();

                Console.Clear();
                Console.WriteLine(_sReportDivider);
                if (success)
                {
                    _levelManager.LevelUp();
                    Console.WriteLine("GREAT JOB!! SUCCESS!");
                    Console.WriteLine($"You are Now at Level {_levelManager.Level}!");
                }
                else
                {
                    _levelManager.LevelDown();
                    Console.WriteLine($"Ooops, {_userAnswer:C} is not quite Right.  Better luck next time!!");
                    ErrorReport();
                    Console.WriteLine($"You are Now at Level {_levelManager.Level}.");
                   
                }
                Console.WriteLine(_sReportDivider);
                PressTheAnyKey();


                setLevel();
                _problemNo = 0;
                _totalAmount = 0;
                _amounts.Clear();
                return Next();
            }
            return retAmount;
        }

        void ErrorReport()
        {
            Console.WriteLine(_sReportDivider);
            foreach (var amount in _amounts)
            {
                Console.WriteLine($"\t\t\t{amount:C}");
            }
            Console.WriteLine(_sReportDivider);
            Console.WriteLine($"Correct Amount is:\t{_totalAmount}");
            Console.WriteLine(_sReportDivider);
            PressTheAnyKey();
        }

        /// <summary>
        /// Prompt the User for his Answer.
        /// </summary>
        /// <returns>true, if the user answers correctly, else false.</returns>
        bool PromptUser()
        {
            bool success = false;
            Console.Clear();
            Console.WriteLine(_sReportDivider);
            Console.WriteLine("Great Job!  Now it's time to check your Answer!");
            Console.Write("What is your Sum for all the values?  ");
            while(!decimal.TryParse(Console.ReadLine(), out _userAnswer))
            {
                Console.WriteLine("That's not a valid number.");
                Console.Write("Please try again: ");
            }
            success = _userAnswer == _totalAmount;
            return success;
        }
    }
}
