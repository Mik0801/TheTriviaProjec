using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTriviaProject.Model
{
    internal class GameLogic
    {
        private string ans1;
        private string ans2;
        private string ans3;
        private string ans4;
        private string correctanswer;
        private int countq;
        public GameLogic(string ans1, string ans2, string ans3, string ans4, string correctanswer)
        {
            this.ans1 = ans1;
            this.ans2 = ans2;
            this.ans3 = ans3;
            this.ans4 = ans4;
            this.correctanswer = correctanswer;
            countq = 0;
        }
        public bool IsCorrectAns(string ans)
        {
            return ans == this.correctanswer;
        }

    }
}
