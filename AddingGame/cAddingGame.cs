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
        const int REPORTDIVIDERLEN = 40;
        String _sReportDivider;
        int _iNumber = 0;
        cCurrencyValue _randomValueGenerator;
        cLevel _levelManager;
        public cAddingGame()
        {
            _levelManager = new cLevel();
            _amounts = new List<decimal>();
            _sReportDivider = new string('#', REPORTDIVIDERLEN);
            _randomValueGenerator = new cCurrencyValue();

            var result = Begin();
        }

        //private async Task<bool> Update(bool newGame = false, int delay = 1)
        bool Update(bool newGame= false, int delay = 1)
        {
            //const int MILLISECONDS = 1000;
            Console.Clear();
            Console.WriteLine(_sReportDivider);
            if (newGame)
            {
                Console.WriteLine("New Game: Add the Values in your Head!");
                Console.WriteLine(_sReportDivider);
            }
            else
            {
                Console.WriteLine(_sReportDivider);
                Console.WriteLine($"{_randomValueGenerator.Amount:C}");
                Console.WriteLine(_sReportDivider);
            }

            Console.Write("Press the AnyKey: ");
            Console.ReadKey();
            //await Task.Delay(delay * MILLISECONDS); // int to milliseconds
            return true;

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
            decimal amount = _randomValueGenerator.Next();
            _totalAmount += amount;
            _amounts.Add(amount);
            return amount;
        }
    }
}
