using GameInterfaces;

namespace Games
{
    public class Adivinar : IGame
    {
        public string Name => "Adivinar numero de simbolos";

        public void Play()
        {
            Console.WriteLine($"Jugando a {Name}");
        }
    }
}
