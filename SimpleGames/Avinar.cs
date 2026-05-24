using GameInterfaces;
using System.Numerics;

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
            this.ShowSpace();
            this.ShowInfo();
            this.ReadValidateInput();
        }

        private void ShowInfo()
        {
            Console.Write("Caracteres: ");
            foreach (var item in _characters)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            Console.WriteLine("Estructura de respuesta: [caracter][espacio][numero]");
            Console.WriteLine("Ejm: A 4, C 6, B 2");
        }

        private bool ReadValidateInput()
        {
            Console.WriteLine("\nIntenta adivinar el numero de uno de los caracteres.");
            Console.Write("Ingrese respuesta: ");
            string input = Console.ReadLine() ?? string.Empty;

            var (isValid, character, count) = ValidateInputText(input);

            if (!isValid) {
                Console.WriteLine("Entrada no valida");
                return isValid;
            }

            var success = CheckCorrect(character, count);

            return success;
        }

        private bool CheckCorrect(char character, int userCount)
        {
            bool success = true;

            int count = 0;

            foreach (char item in _space)
            {
                if (item == character) count++;
            }

            return count > 0 ? success : !success;
        }

        private Tuple<bool, char, int> ValidateInputText(string response)
        {
            var parts = response.Trim().Split(' ');

            if (parts.Length != 2) return Tuple.Create(false, 'N', 0);

            char character = parts[0][0];

            if (!this._characters.Contains(character)) return Tuple.Create(false, 'N', 0);
            if (!int.TryParse(parts[1], out int count)) return Tuple.Create(false, 'N', 0);

            return Tuple.Create(true, character, count);
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
