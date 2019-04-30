using System;
using System.Collections.Generic;
using System.Text;

namespace AddingGame
{
    public class cProblem
    {


        public cProblem()
        {
            
        }

        public int ProblemNo { get; internal set; }

        public void NewProblem()
        {
            ProblemNo++;
        }


    }
}
