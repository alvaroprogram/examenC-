using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Player p1 = new Player("Alvaro", "alvaroter@email.com", Countries.Brazil);
            Player p2 = new Player("Fernando", "fer@email.com", Countries.Canada);
            Player p3 = new Player("Antonio", "antoniom@gmail.com", Countries.Germany);
            Player p4 = new Player("Javier", "Javierrrr@hotmail.com", Countries.France);

            Score s1 = new Score("Primero", 200);
            Score s2 = new Score("Segundo", 195);
            Score s3 = new Score("Tercero", 150);
            Score s4 = new Score("Cuarto", 100);

            List<Score> l1 = new List<Score>();
            List<Score> l2 = new List<Score>();
            l1.Add(s1);
            l1.Add(s2);
            l2.Add(s3);
            l2.Add(s4);

            Ranking r1 = new Ranking("Primero", l1);
            Ranking r2 = new Ranking("Segundo", l2);
            Ranking r3 = new Ranking("Tercero", l1);
            Ranking r4 = new Ranking("Cuarto", l2);

            List<Plataforms> plat1 = new List<Plataforms>();
            List<Plataforms> plat2 = new List<Plataforms>();
            plat1.Add(Plataforms.Linux);
            plat1.Add(Plataforms.PC);
            plat2.Add(Plataforms.XBOXONE);
            plat2.Add(Plataforms.PS4);

            Dictionary<Plataforms, Ranking> d1 = new Dictionary<Plataforms,Ranking>();
            Dictionary<Plataforms, Ranking> d2 = new Dictionary<Plataforms, Ranking>();
            d1.Add(Plataforms.PC, r2);
            d2.Add(Plataforms.PS4, r1);

            Game g1 = new Game("Rayman", Genres.Adventure, plat1, 1900, d1);
            Game g2 = new Game("Metal gear", Genres.Action, plat2, 2015, d2);
            Game g3 = new Game("Gran Turismo Sport", Genres.Simulation, plat2, 2017, d2);
            Game g4 = new Game("Call Of Duty", Genres.Survival, plat2, 2016, d2);

            GameServices.Games.Add(g1);
            GameServices.Games.Add(g2);
            GameServices.Games.Add(g3);
            GameServices.Games.Add(g4);

            GameServices.Player.Add(p1);
            GameServices.Player.Add(p2);
            GameServices.Player.Add(p3);
            GameServices.Player.Add(p4);

            Console.WriteLine("---Bienvenido al programa GameCenter---\n");
            Console.WriteLine("Introduce un comando:");
            String comando = Console.ReadLine();
            if (comando == "import")
            {
                GameServices.Import();
            }
            else if (comando == "export")
            {
                GameServices.Export();
            }
            else if (comando == "oldest")
            {
                Console.WriteLine("El juego mas antiguo es:");
                Console.WriteLine(GameServices.OldestGame());

            }
            else if (comando == "scoreCount")
            {
                Console.WriteLine("Introduce el Nombre del Juego");
                Console.ReadLine();
                Console.WriteLine("Introduce el Nombre del Ranking");
                Console.ReadLine();
                Console.WriteLine(GameServices.PointsRegistered());
            }

            else if (comando == "gamesCountByGenre")
            {
                Console.WriteLine("Numero de juegos por genero");
                Console.ReadLine();
                Console.WriteLine(GameServices.GameGenre());
            }
            else if (comando == "gamesByPlayer")
            {
                Dictionary<String, List<Game>> dict = GameServices.GamesPlayedAllPlayer();
                foreach (String nickname in dict.Keys)
                {
                    String games = "";
                    Console.WriteLine("->" + nickname);
                    int count = 0;
                    foreach (Game game in dict[nickname])
                    {
                        count++;
                        games += game.Name;
                        if (count != dict[nickname].Count)
                        {
                            games += ",";
                        }
                    }
                    Console.WriteLine("=========> " + games);
                }

            }
            Console.ReadLine();

        }
}
}
