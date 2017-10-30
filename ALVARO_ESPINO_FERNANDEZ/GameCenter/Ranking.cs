using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter
{

    public class Ranking
    {
        #region Getters && Setters
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<Score> scores;

        public List<Score> Scores
        {
            get { return scores; }
        }
        #endregion

        #region Construct
        public Ranking(string name, List<Score> scores)
        {
            this.name = name;
            this.scores = scores;
        }


        public Ranking(string data)
        {
            string[] splittedData = data.Split('-');           

            string[] splittedData2 = splittedData[3].Split();
            List<Score> score = new List<Score>();

            foreach (string scores in splittedData2)
            {
                score.Add(new Score(scores));

            }
            this.name = splittedData[2];
            this.scores = score;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            string s;
            s = string.Format("Ranking: {0}\n", name);
            int i = 0;
            foreach (Score scores in scores)
            {
                i++;
                s += string.Format("{0}.{1}-{2}\n", i, scores.Nickname, scores.Points);
            }
            return s;
        }
        #endregion
    }
}