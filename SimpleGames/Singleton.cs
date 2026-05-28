using GameInterfaces;
using Crators;

namespace Singleton
{
    // Singlenton para controlar la lista de juegos
    public class GameList
    {
        private static GameList gameList;
        private List<Creator> _creatorGames;
        private IGame currentGame;

        private GameList()
        {
            this._creatorGames = new List<Creator>();
            this._creatorGames.Add(new GatoCreator());
            this._creatorGames.Add(new AdivinarCreator());
            this._creatorGames.Add(new MemoramaCreator());
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
            if (this._creatorGames == null || this._creatorGames.Count == 0)
            {
                Console.WriteLine("No hay juegos disponibles");
                return;
            }
            Console.WriteLine("Lista de juegos disponibles:");
            this._creatorGames.ForEach(creatorGame =>
            {
                Console.WriteLine($"{this._creatorGames.IndexOf(creatorGame) + 1} {creatorGame.Name}");
            });
        }

        public bool SetCurrentGame(int index)
        {
            if (index < 0 || index >= this._creatorGames.Count)
            {
                Console.WriteLine("Índice de juego no válido");
                return false;
            }

            this.currentGame = this._creatorGames[index].CreateGame();
            Console.WriteLine($"Juego seleccionado: {this.currentGame.Name}");
            return true;
        }

        public bool SelectCurrentGame()
        {
            bool successes = false;
            Console.WriteLine("Selecciona el juego:");

            try
            {
                int index = int.Parse(s: Console.ReadLine().Trim()) - 1;
                successes = this.SetCurrentGame(index);
            }
            catch (FormatException)
            {
                Console.WriteLine("Seleccion invalida, solo numeros.");
                successes = false;
            }

            return successes;
        }

        public void PlayCurrentGame()
        {
            if (this.currentGame == null)
            {
                Console.WriteLine("No se ha seleccionado ningún juego");
                return;
            }
            this.currentGame.Play();
        }
    }
}