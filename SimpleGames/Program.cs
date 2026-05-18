using Singleton;

namespace GameMain
{
    public class Program
    {
        public static void Main()
        {
            var gameList = GameList.GetGameList();
            gameList.ShowListGame();
        }
    }
}
