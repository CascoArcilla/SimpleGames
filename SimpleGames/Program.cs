using Singleton;

namespace GameMain
{
    public class Program
    {
        public static void Main()
        {
            // Obtener la instancia del GameList (Singleton) y mostrar los juegos
            var gameList = GameList.GetGameList();
            gameList.ShowListGame();

            // Dar al usario elejir un juego de la lista
            var selectGame = false;
            do
            {
                selectGame = gameList.SelectCurrentGame();
            }
            while (!selectGame);

            Console.Clear();

            // Iniciar el juego seleccionado
            gameList.PlayCurrentGame();
            Console.WriteLine("\nFin del juego, ejecute de nuevo para volver a seleccionar otra vez.");
            
            Console.Write("\n\nPulse cualquier tecla para cerrar...");
            Console.ReadKey(true);
        }
    }
}
