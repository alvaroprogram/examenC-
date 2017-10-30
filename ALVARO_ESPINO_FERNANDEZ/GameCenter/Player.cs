using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter
{   
    public class Player
    {
        #region Getters && Setters
        private string nickname;

        public string Nickname
        {
            get { return nickname; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private Countries country;

        public Countries Country
        {
            get { return country; }
            set { country = value; }
        }
        #endregion

        #region Construct
        public Player(string nickname, string email, Countries country)
        {
            this.nickname = nickname;
            this.email = email;
            this.country = country;
        }

        public Player(string data)
        {
            string[] splittedData = data.Split('-');
            this.nickname = splittedData[0];
            this.email = splittedData[1];
            this.country = (Countries)int.Parse(splittedData[2]);
        }
        #endregion

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj is Player)
            {
                Player aux = (Player)obj;
                return this.Nickname == aux.Nickname;
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
            return string.Format("{0}-{1}-{2}", nickname, email, country);
        }
        #endregion
    }
}
