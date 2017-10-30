using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter
{
    public class GameServices
    {
        #region Getters && Setters

        private const string data = "../../data.txt";

        private static List<Player> players = new List<GameCenter.Player>();

        public static List<Player> Player
        {
            get { return players; }
        }

        private static List<Game> games = new List<GameCenter.Game>();

        public static List<Game> Games
        {
            get { return games; }
        }
        
        #endregion

        #region Export
        public static void Export()
        {
            string playersData = ConvertPlayersToString();
            string gamesData = ConvertGamesToString();
            string rankingData = ConvertRankingToString();
            try
            {
                StreamWriter file = File.CreateText(data);
                string Data = playersData + "*+*+*+*\n" + gamesData + "*+*+*+*\n" + rankingData;
                file.Write(Data);
                file.Close();
                Console.WriteLine("Datos exportados correctamente");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al exportar los datos. " + e);
            }
        }

        private static string ConvertPlayersToString()
        {
            string data = "";
            List<Player> players = new List<Player>();
            foreach (Player player in players)
            {
                string playersData = "";
                playersData = string.Format("{0}-{1}-{2}", player.Nickname, player.Email, player.Country);
                playersData += "\n";
                data += playersData;
            }
            return data;
        }

        private static string ConvertPlataformsToString(List<Plataforms> plat)
        {
            string data = "";
            int count = 1;
            foreach (Plataforms plataforms in plat)
            {
                data = string.Format("{0}", plat);
                if (count != plat.Count)
                {
                    data += ",";
                }
            }
            return data;
        }

        private static string ConvertGamesToString()
        {
            string data = "";
            foreach (Game game in games)
            {
                string gamesData = "";
                gamesData = string.Format("{0}-{1}-{2}-{3}", game.Name, game.Genres, game.ReleaseDate, ConvertPlataformsToString(game.Plataforms));
                gamesData += "\n";
                data += gamesData;
            }
            return data;
        }

        private static string ConvertScoresToString(List<Score> scores)
        {
            string result = "";
            int i = 0;
            foreach (Score score in scores)
            {
                result += score.ToString();
                i++;
                if (i != scores.Count)
                {
                    result += ",";
                }
            }
            return result;
        }
        private static string ConvertRankingToString()
        {
            string data = "";
            foreach (Game game in games)
            {
                foreach (Plataforms plat in game.Rankings.Keys)
                {
                    string plataformsData = "";
                    data += string.Format("{0}-{1}-{2}-{3}", game.Name, plat, game.Rankings[plat].Name, ConvertScoresToString(game.Rankings[plat].Scores));
                    plataformsData += "\n";
                    data += plataformsData;
                }

            }
            return data;
        }
        #endregion

        #region Import
        public static void Import()
        {
            List<string> lines = ReadFile(data);
            List<string> playersLines = new List<string>();
            List<string> gamesLines = new List<string>();
            List<string> rankingLines = new List<string>();
            bool isGames = false;
            bool isRanking = false;
            foreach (string line in lines)
            {
                if (line == "*+*+*+*")
                {
                    if (isGames == false)
                    {
                        isGames = true;

                    }
                    else
                    {
                        isRanking = true;
                    }
                }
                else
                {
                    if (isGames && isRanking)
                    {
                        string[] splitted = line.Split('-');
                        getGame(splitted[0]).addRanking((Plataforms)int.Parse(splitted[1]), new Ranking(line));
                    }
                    else if (isGames && !isRanking)
                    {
                        games.Add(new Game(line));
                    }
                    else
                    {
                        players.Add(new Player(line));
                    }
                }
            }
        }

        public static Game getGame(String gameName)
        {
            Game result = null;
            foreach(Game game in games)
            {
                if (game.Name == gameName)
                {
                    result = game;
                    break;
                }
            }
            return result;
        }

        private static List<string> ReadFile(string data)
        {
            List<string> lines = new List<string>();
            try
            {
                StreamReader file = File.OpenText(data);
                string line = "";
                while (line != null)
                {
                    line = file.ReadLine();
                    if (line != null)
                    {
                        lines.Add(line);
                    }
                }
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al leer fichero\n" + e);
            }
            return lines;
        }
        #endregion

        #region OldestGame
        public static Game OldestGame()
        {
            Game oldestgame = null;
            int releaseDate = int.MaxValue;
            foreach (Game game in games)
            {
                int y = game.ReleaseDate;
                if (releaseDate > y)
                {
                    oldestgame = game;
                    releaseDate = y;
                }
            }
            
            return oldestgame;
            
        }
        #endregion

        #region PointsRegistered
        private static Ranking GetRankingByGameName(string gameName, string rankingName)
        {
            Ranking result = null;
            foreach (Game game in games)
            {
                if (game.Name == gameName)
                {
                    result = GetRankingByGameAndName(game, rankingName);
                    break;
                }
            }
            return result;
        }

        private static Ranking GetRankingByGameAndName(Game game, string rankingName)
        {
            Ranking result = null;
            foreach (Ranking ranking in game.Rankings.Values)
            {
                if (ranking.Name == rankingName)
                {
                    result = ranking;
                    break;
                }

            }
            return result;
        }


        public static int PointsRegistered(string gameName, string rankingName)
        {
            Ranking ranking = GetRankingByGameName(gameName, rankingName);
            return ranking.Scores.Count;
        }
        #endregion

        #region GameGenre
        public static int GameGenre(Genres genre)
        {
            int count = 0;
            foreach (Game game in games)
            {
                if (game.Genres == genre)
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region BestGamePoints
        public static Game BestGamePoints()
        {
            Game best = null;
            int bestPoints = -1;

            foreach (Game game in games)
            {
                int currentScoreNumber = 0;
                foreach (Ranking ranking in game.Rankings.Values)
                {
                    currentScoreNumber += ranking.Scores.Count;
                }
                if (bestPoints < currentScoreNumber)
                {
                    best = game;
                    bestPoints = currentScoreNumber;
                }
            }
            return best;
        }
        #endregion

        #region ExistCall
        public static bool ExistCall(string call = "call")
        {
            bool result = false;
            foreach (Game game in games)
            {
                if (game.Name.Contains(call))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion

        #region GamePlayer
        public static List<Game> GamePlayer(string nickName)
        {
            List<Game> result = new List<Game>();
            foreach (Game game in games)
            {
                foreach (Ranking ranking in game.Rankings.Values)
                {
                    foreach (Score score in ranking.Scores)
                    {
                        if (score.Nickname == nickName)
                        {
                            result.Add(game);
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region GamesPlayedAllPlayer
        public static Dictionary<string, List<Game>> GamesPlayedAllPlayer()
        {
            Dictionary<string, List<Game>> result = new Dictionary<string, List<Game>>();
            foreach (Player player in players)
            {
                List<Game> gamePlayed = GamePlayer(player.Nickname);
                result.Add(player.Nickname, gamePlayed);
            }
            return result;
        }
        #endregion

    }
}
