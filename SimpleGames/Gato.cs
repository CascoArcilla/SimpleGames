using GameInterfaces;

namespace GameGato
{
    public class Gato : IGame
    {
        public string Name { get; }

        public Gato() { Name = "Tik Tak Toe"; }

        public void Play()
        {
            Console.WriteLine("Jugando al Gato");
        }
    }
}
