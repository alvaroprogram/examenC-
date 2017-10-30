using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter
{
    public class Game
    {
        #region Getters && Setters
        private string name;

        public string Name
        {
            get { return name; }
        }

        private Genres genres;

        public Genres Genres
        {
            get { return genres; }
        }

        private List<Plataforms> plataforms;

        public List<Plataforms> Plataforms
        {
            get { return plataforms; }
        }

        private int releasedate;

        public int ReleaseDate
        {
            get { return releasedate; }
        }

        private Dictionary<Plataforms, Ranking> rankings;

        public Dictionary<Plataforms, Ranking> Rankings
        {
            get { return rankings; }
        }
        #endregion

        public void addRanking(Plataforms platform, Ranking value)
        {
            this.rankings.Add(platform, value);
        }

        #region Construct
        public Game(string name, Genres genres, List<Plataforms> plataforms, int releasedate, Dictionary<Plataforms, Ranking> rankings)
        {
            this.name = name;
            this.genres = genres;
            this.plataforms = plataforms;
            if (releasedate >= 1980 || releasedate <= 2018)
            {
                this.releasedate = releasedate;
            }
            else
            {
                releasedate = 0;
            }
            this.rankings = rankings;
        }

        public Game(string data)
        {
            string[] splittedData = data.Split('-');
            string[] splittedData2 = splittedData[3].Split();

            List < Plataforms > plat = new List<Plataforms>();
            foreach (string plataforms in splittedData2)
            {
                plat.Add((Plataforms)int.Parse(plataforms));               

            }        
            
            this.name = splittedData[0];
            this.genres = (Genres)int.Parse(splittedData[1]);
            this.plataforms = plat;
            this.releasedate = int.Parse(splittedData[3]);
        }
        #endregion

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj is Game)
            {
                Game aux = (Game)obj;
                return this.Name == aux.Name;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            string s = string.Format("---{0}({1})-", name, releasedate);
            foreach (Plataforms plataforms in Plataforms)
            {
                s += "," + plataforms;
            }

            s += string.Format("-{0}---", genres);
            s += string.Format("\n\tRankings:\n");

            foreach (Ranking ranking in rankings.Values)
            {
                s += string.Format("\t\t-{0}", name);
                s += string.Format("({0})\n", ranking.Scores.Count);
            }
            return s;
        }
        #endregion
    }
}
