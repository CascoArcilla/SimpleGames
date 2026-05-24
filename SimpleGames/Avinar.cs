using GameInterfaces;

namespace Games
{
    public class Adivinar : IGame
    {
        public string Name => "Adivinar numero de simbolos";
        private int _dimension = 4;
        private char[] _characters;
        private char[,] _space;

        public Adivinar()
        {
            this._characters = new char[] { 'A', 'B', 'C', 'D' };
            this._space = new char[this._dimension, this._dimension];
            this.FillSpace();
        }

        public void Play()
        {
            Console.WriteLine($"Jugando a {Name}\n");

            int iterations;

            do
            {
                Console.WriteLine("Numero de partida (minimo 1, maximo 6)");
                Console.Write("Partidas: ");
                try
                {
                    iterations = int.Parse(Console.ReadLine().Trim());
                    if (iterations > 0 && iterations <= 6) break;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Valor no valido, vuelva a intentar");
                }
            }
            while (true);


            for (int i = 0; i < iterations; i++)
            {
                Console.Clear();
                Console.WriteLine("Adivina el caracter");
                this.MainLogic();
                Console.WriteLine($"\nRonda {i + 1} de {iterations}");
                Console.WriteLine("Pulse cualquier tecla para cotinuar con la siguente ronda.");
                Console.ReadKey(true);
            }
        }

        private void MainLogic()
        {
            this.ShowSpace();
            this.ShowInfo();

            var validSuccess = this.ReadValidateInput();

            if (validSuccess)
            {
                Console.WriteLine("¡¡¡Impresionante!!!, lo has logrado.");
            }
            else
            {
                Console.WriteLine("Uhhh, lastima, bueno realmente no somos videntes");
            }

            Console.WriteLine("Espacio revelado");
            this.ShowSpace(true);
        }

        private void ShowInfo()
        {
            Console.Write("Caracteres: ");
            foreach (var item in _characters)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            Console.WriteLine("Estructura de respuesta: [caracter(mayuscula)][espacio][numero]");
            Console.WriteLine("Ejm: A 4, C 6, B 2");
        }

        private bool ReadValidateInput()
        {
            string input;
            bool isValid;
            char character;
            int count;
            string message;

            do
            {
                Console.WriteLine("\nIntenta adivinar el numero de uno de los caracteres.");
                Console.Write("Ingrese respuesta: ");
                input = Console.ReadLine() ?? string.Empty;

                (isValid, character, count, message) = ValidateInputText(input);

                if (isValid)
                {
                    break;
                }

                Console.WriteLine($"Entrada no valida. {message}");
            }
            while (true);

            var success = CheckResponse(character, count);

            return success;
        }

        private bool CheckResponse(char character, int userCount)
        {
            bool success = true;

            int count = 0;

            foreach (char item in _space)
            {
                if (item == character) count++;
            }

            return count == userCount ? success : !success;
        }

        private Tuple<bool, char, int, string> ValidateInputText(string response)
        {
            var parts = response.Trim().Split(' ');

            if (parts.Length != 2) return Tuple.Create(false, 'N', 0, "Su repuesta puede no tener espacio.");

            char character = parts[0][0];

            if (!this._characters.Contains(character)) return Tuple.Create(false, 'N', 0, "Su caracter no coincide con los disponibles.");
            if (!int.TryParse(parts[1], out int count)) return Tuple.Create(false, 'N', 0, "Debe indicar un numero.");

            return Tuple.Create(true, character, count, "Validacion de entrada exitosa.");
        }

        private void ShowSpace(bool show = false)
        {
            if (_space == null)
            {
                Console.WriteLine("Space sin contenido");
                return;
            }

            for (int i = 0; i < this._dimension; i++)
            {
                Console.WriteLine("-----------------");
                Console.Write("|");
                for (int j = 0; j < this._dimension; j++)
                {
                    if (show) Console.Write($" {this._space[i, j]} |");
                    else Console.Write(" ? |");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-----------------");
        }

        private void FillSpace()
        {
            var random = new Random();
            for (int i = 0; i < this._dimension; i++)
            {
                for (int j = 0; j < this._dimension; j++)
                {
                    char randomChar = this._characters[random.Next(this._characters.Length)];
                    this._space[i, j] = randomChar;
                }
            }
        }
    }
}
