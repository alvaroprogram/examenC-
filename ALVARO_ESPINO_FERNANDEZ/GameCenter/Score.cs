using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter
{
    public class Score
    {
        #region Getters && Setters
        private string nickname;

        public string Nickname
        {
            get { return nickname; }
        }

        private int points;

        public int Points
        {
            get { return points; }
            set
            {
                if (points >= 0)
                {
                    points = value;
                }
                else
                {
                    points = 0;
                }
            }
        }
        #endregion

        #region Construct

        public Score(string nickname, int points)
        {
            this.nickname = nickname;
            
            if (points >= 0)
            {
                this.points = points;
            }
            else
            {
                this.points = 0;
            }
        }


        public Score(string data)
        {
            string[] splittedData = data.Split('=');
            this.nickname = splittedData[0];
            this.points = int.Parse(splittedData[1]);

        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return string.Format("{0}-{1}", nickname, points);
        }
        #endregion
    }
}