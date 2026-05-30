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

        // El caracter "?" se usa para permitir null en las piezas
        private Piece? _first;
        private Piece? _second;
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
            bool finished = false;

            do
            {
                this.ShowBoard();

                if (this.CheckFinished()) break;

                this.ShowMenu();
                this.ReadValidateInput();


                Console.Clear();

                (bool isMatch, string message) = this.CheckMatch();

                if (message != "")
                {
                    this.ShowBoard();
                    Console.Write($"{message}\nPresione una tecla para continuar...");
                    Console.ReadKey(true);

                    if (!isMatch)
                    {
                        this._first?.Revealed = false;
                        this._second?.Revealed = false;
                    }

                    this.NullPieces();
                    Console.Clear();
                }

            } while (!finished);

            Console.WriteLine("\nGenial, has terminado la partida.");
            Console.WriteLine("Aqui no puedes perder, tu intentos son ilimiatados.");
        }

        // Muestra el tablero con las piezas reveladas o no dependiendo de su estado
        private void ShowBoard()
        {
            // Imprimir cabecera
            Console.WriteLine("    1   2   3   4");

            string incognit = "?";
            int checkIndex = 0;

            foreach (Piece item in this._structurePieces)
            {
                // Imprimir separadores y numeros de fila
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

                // Mostrar el valor de la pieza o el incognito dependiendo de su estado
                string show = item.Revealed ? item.Value : incognit;
                Console.Write($" {show} |");

                checkIndex++;
            }

            Console.WriteLine("\n   --- --- --- ---");
        }

        // Muestra instrucciones sobre el input para las piezas
        private void ShowMenu()
        {
            Console.WriteLine("Se ingresa primero una casila y despues la siguiente.");
            Console.WriteLine("Estructura de casilla: [posicion_x][espacio][posicion_y]");
            Console.WriteLine("Ej.\"1 1\", \"2 3\", \"4 2\"");

            if (this._first != null) Console.WriteLine($"Primera casilla: {this._first.Value}");
            if (this._second != null) Console.WriteLine($"Segunda casilla: {this._second.Value}");
        }

        // Lee el input del usuario y valida que sea correcto, si es correcto se asignan las piezas seleccionadas
        private bool ReadValidateInput()
        {
            string input;
            bool isValid;
            int posX;
            int posY;
            string message;

            do
            {
                Console.Write("\nIngrese respuesta: ");
                input = Console.ReadLine() ?? string.Empty;

                (isValid, posX, posY, message) = ValidateInputText(input);

                if (isValid) break;

                Console.WriteLine($"Entrada no valida. {message}");
            }
            while (true);

            SetPieces(posX, posY);

            return isValid;
        }

        // Valida el formato un string de entrada asi como la validez de las coordenadas
        private Tuple<bool, int, int, string> ValidateInputText(string response)
        {
            var parts = response.Trim().Split(' ');

            if (parts.Length != 2) return Tuple.Create(false, 0, 0, "Su repuesta puede no tener espacio");

            if (!int.TryParse(parts[0], out int px)) return Tuple.Create(false, 0, 0, "Las entradas deben ser numeros");

            if (!int.TryParse(parts[1], out int py)) return Tuple.Create(false, 0, 0, "Las entradas deben ser numeros");

            if (px < 1 || px > this._dimension || py < 1 || py > this._dimension) return Tuple.Create(false, 0, 0, "Una entrada esta fuera de rango");

            if (this._structurePieces[py - 1, px - 1].Matched || this._structurePieces[py - 1, px - 1].Revealed) return Tuple.Create(false, 0, 0, "Casilla ya fue seleccionada");

            return Tuple.Create(true, px, py, "Validacion de entrada exitosa.");
        }

        // Comprueba si las piezas seleccionadas son iguales, si lo son se marcan como emparejadas, si no se ocultan de nuevo
        private Tuple<bool, string> CheckMatch()
        {
            if (this._first == null || this._second == null) return Tuple.Create(false, "");

            bool match = true;
            string message = "Bien, ve por otro.";

            if (this._first.Value == this._second.Value)
            {
                this._first.Matched = true;
                this._second.Matched = true;
            }
            else
            {
                message = "Mal intento, pruebe de nuevo.";
                match = false;
            }

            return Tuple.Create(match, message);
        }

        // Asigna las piezas seleccionadas a las variables de pieza 1 y pieza 2 dependiendo de su estado
        private void SetPieces(int posX, int posY)
        {
            if (this._first == null)
            {
                this._first = this._structurePieces[posY - 1, posX - 1];
                this._first.Revealed = true;
                return;
            }

            if (this._second == null)
            {
                this._second = this._structurePieces[posY - 1, posX - 1];
                this._second.Revealed = true;
                return;
            }
        }

        private void NullPieces()
        {
            this._first = null;
            this._second = null;
        }

        private bool CheckFinished()
        {
            foreach (Piece item in this._structurePieces)
            {
                if (!item.Matched) return false;
            }
            return true;
        }
    }

    class Piece
    {
        public string Value { get; set; }
        public bool Revealed { get; set; }
        public bool Matched { get; set; }

        public Piece(string value)
        {
            this.Value = value;
            this.Revealed = false;
            this.Matched = false;
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