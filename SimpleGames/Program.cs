using Singleton;

namespace GameMain
{
    public class Program
    {
        public static void Main()
        {
            var gameList = GameList.GetGameList();
            gameList.ShowListGame();

            // Dar al usario elejir un juego de la lista
            var selectGame = false;
            do
            {
                selectGame = gameList.SelectCurrentGame();
            }
            while (!selectGame);

            // Iniciar el juego seleccionado
            gameList.PlayCurrentGame();
        }
    }
}
