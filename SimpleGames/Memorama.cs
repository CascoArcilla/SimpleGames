using GameInterfaces;
using System.Collections;

namespace Games
{
    public class Memorama : IGame
    {
        public string Name => "Memorama";

        private readonly string[] charactersValues = new string[] { "S", "D", "F", "H" };
        private List<Piece> _pieces;
        private Piece[,] _structurePieces;

        private Piece _first;
        private Piece _second;
        private string _message;
        private readonly int _dimension = 4;
        public Memorama()
        {
            // Crear piezas del memorama
            List<Piece> piecesSorted = new List<Piece>();
            foreach (string item in this.charactersValues)
            {
                piecesSorted.Add(new Piece(item));
                piecesSorted.Add(new Piece(item));
                piecesSorted.Add(new Piece(item));
                piecesSorted.Add(new Piece(item));
            }

            // Revolver las piezas
            this._pieces = ListExtensions.Shuffle(piecesSorted);

            // Crear la estructura del tablero 4x4
            _structurePieces = new Piece[this._dimension, this._dimension];

            // Agregar las piezas a la estructura del tablero
            int index = 0;
            for (int i = 0; i < this._dimension; i++)
            {
                for (int j = 0; j < this._dimension; j++)
                {
                    this._structurePieces[i, j] = this._pieces[index];
                    index++;
                }
            }
        }

        public void Play()
        {
            Console.WriteLine($"Jugando {this.Name} ahora\n");
            this.ShowBoard();
            this.ShowMenu();
        }

        private void ShowBoard()
        {
            // Imprimir cabecera
            Console.WriteLine("    1   2   3   4");

            string incognit = "?";
            int checkIndex = 0;

            foreach (Piece item in this._structurePieces)
            {
                switch (checkIndex)
                {
                    case 0:
                        Console.WriteLine("   --- --- --- ---");
                        Console.Write("1 |");
                        break;
                    case 4:
                        Console.WriteLine("\n   --- --- --- ---");
                        Console.Write("2 |");
                        break;
                    case 8:
                        Console.WriteLine("\n   --- --- --- ---");
                        Console.Write("3 |");
                        break;
                    case 12:
                        Console.WriteLine("\n   --- --- --- ---");
                        Console.Write("4 |");
                        break;
                }

                string show = !item.Revealed ? item.Value : incognit;
                Console.Write($" {show} |");

                checkIndex++;
            }

            Console.WriteLine("\n   --- --- --- ---");
        }

        private void ShowMenu()
        {
            Console.WriteLine("Se ingresa primero una casila y despues la siguiente.");
            Console.WriteLine("Estructura de casilla: [posicion_x][espacio][posicion_y]");
            Console.WriteLine("Ej.\"1 1\", \"2 3\", \"4 2\"");

            if (this._first != null) Console.WriteLine($"Primera casilla: {this._first.Value}");
            if (this._second != null) Console.WriteLine($"Segunda casilla: {this._second.Value}");
            if (this._message != null || this._message != "") Console.WriteLine(this._message);
        }

        private bool ReadValidateInput()
        {
            string input;
            bool isValid;
            int posX;
            int posY;
            string message;

            do
            {
                Console.WriteLine("Intenta adivinar el numero de uno de los caracteres.");
                Console.Write("Ingrese respuesta: ");
                input = Console.ReadLine() ?? string.Empty;

                (isValid, posX, posY, message) = ValidateInputText(input);

                if (isValid) break;

                Console.WriteLine($"Entrada no valida. {message}");
            }
            while (true);

            var success = SetPieces(posX, posY);

            return success;
        }

        private Tuple<bool, int, int, string> ValidateInputText(string response)
        {
            var parts = response.Trim().Split(' ');

            if (parts.Length != 2) return Tuple.Create(false, 0, 0, "Su repuesta puede no tener espacio");
            if (!int.TryParse(parts[0], out int px)) return Tuple.Create(false, 0, 0, "Las entradas deben ser numeros");
            if (!int.TryParse(parts[1], out int py)) return Tuple.Create(false, 0, 0, "Las entradas deben ser numeros");
            if (px < 1 || px > this._dimension || py < 1 || py > this._dimension) return Tuple.Create(false, 0, 0, "Una entrada esta fuera de rango");

            return Tuple.Create(true, px, py, "Validacion de entrada exitosa.");
        }

        private bool SetPieces(int posX, int posY)
        {
            bool success = true;

            return success;
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