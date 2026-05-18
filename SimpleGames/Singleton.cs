using GameInterfaces;
using GameGato;
using System.Numerics;

namespace Singleton
{
    // Singlenton para controlar la lista de juegos
    public class GameList
    {
        private static GameList gameList;
        private List<IGame> _games;
        private IGame currentGame;

        private GameList()
        {
            this._games = new List<IGame>();
            this._games.Add(new Gato());
        }

        public static GameList GetGameList()
        {
            if (gameList == null)
            {
                gameList = new GameList();
            }
            return gameList;
        }

        public void ShowListGame()
        {
            if (this._games == null || this._games.Count == 0)
            {
                Console.WriteLine("No hay juegos disponibles");
                return;
            }
            this._games.ForEach(game =>
            {
                Console.WriteLine($"{this._games.IndexOf(game) + 1} {game.Name}");
            });
        }

        public void AddGame(IGame game)
        {
            this._games.Add(game);
        }

        public bool SetCurrentGame(int index)
        {
            if (index < 0 || index >= this._games.Count)
            {
                Console.WriteLine("Índice de juego no válido");
                return false;
            }
            this.currentGame = this._games[index];
            return true;
        }

        public bool SelectCurrentGame()
        {
            Console.WriteLine("Selecciona el juego:");
            int index = int.Parse(s: Console.ReadLine().Trim());
            var successes = this.SetCurrentGame(index);
            return successes;
        }
    }
}