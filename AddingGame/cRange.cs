using System;
using System.Collections.Generic;
using System.Text;


namespace AddingGame
{
    public class cRange
    {
        Random _rand;
        int _min;
        int _max;

        public cRange(int min, int max)
        {
            _rand = new Random();
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Return a random value betwen _min and _max, inclusive.
        /// </summary>
        /// <returns>
        /// Returns a random value between and including the values
        /// _min and max.
        /// </returns>
        public int Next()
        {
            if (_max == 0)
                return 0;
            else
                return _rand.Next(_min, _max + 1);
        }

        /// <summary>
        /// Set a new Range for this instance of cRange.
        /// </summary>
        /// <param name="min">new minimum value</param>
        /// <param name="max">new maximum value</param>
        public void SetRange(int min, int max)
        {
            _min = min;
            _max = max;
        }


    }
}
