using GameInterfaces;
using System.Collections;

namespace Games
{
    public class Memorama : IGame
    {
        public string Name => "Memorama";

        private readonly string[] charactersValues = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };
        private List<Piece> Pieces;

        public Memorama() 
        {
            List<Piece> piecesSorted = new List<Piece>();
            foreach (string item in this.charactersValues)
            {
                piecesSorted.Add(new Piece(item));
                piecesSorted.Add(new Piece(item));
            }

            this.Pieces = ListExtensions.Shuffle(piecesSorted);
        }

        public void Play()
        {
            Console.WriteLine($"Jugando {this.Name} ahora");
        }
    }

    class Piece
    {
        public string Value { get; set; }
        public bool Revealed { get; set; }

        public Piece(string value)
        {
            this.Value = value;
            this.Revealed = false;
        }
    }

    public static class ListExtensions
    {
        private static Random rnd = new Random();

        public static List<T> Shuffle<T>(this List<T> list)
        {
            List<T> shuffledList = new List<T>(list);
            int n = shuffledList.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }

            return shuffledList;
        }
    }
}
