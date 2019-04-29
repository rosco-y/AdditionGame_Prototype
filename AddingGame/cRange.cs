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
        /// Call Random.Next(_min, _max+1)
        /// </summary>
        /// <returns>
        /// Returns a random value between and including the values
        /// _min and max.
        /// </returns>
        public int Next()
        {
            return _rand.Next(_min, _max + 1);
        }


    }
}
