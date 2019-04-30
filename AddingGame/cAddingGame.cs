using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public cAddingGame()
        {
            _amounts = new List<decimal>();
            _sReportDivider = new string('#', REPORTDIVIDERLEN);
            _randomValueGenerator = new cCurrencyValue();

            var result = Begin();
        }

        private async Task<bool> Update(bool newGame = false, int delay = 1)
        {
            const int MILLISECONDS = 1000;
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
            await Task.Delay(delay * MILLISECONDS); // int to milliseconds
            return true;

        }

        public async Task<bool> Begin()
        {
            bool more = await Update(true, 3);

            while (more)
            {
                Next();
                more = await Update();
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
