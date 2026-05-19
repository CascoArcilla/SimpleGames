using GameInterfaces;

namespace GameGato
{
    public class Gato : IGame
    {
        public string Name { get; }

        private char[] _players;
        private char _turn;
        private bool _winner;
        private bool _turnOne;
        private char[] _board;

        public Gato()
        {
            Name = "Tik Tak Toe";
            _winner = false;
            _turnOne = true;
            _players = new char[2] { 'X', 'O' };
            _turn = _players[0];
            _board = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9', };
        }

        public void Play()
        {
            Console.WriteLine($"Jugando ahora {this.Name}");

            do
            {
                // Limpiar consola si no es el primer turno
                if (!_turnOne) Console.Clear();

                Console.WriteLine($"Turno de: {_turn}");
                ShowBoard();

                ControlInputPlayer();

                ChangeTurn();
                _turnOne = false;
            }
            while (!_winner);
        }

        private void ChangeTurn()
        {
            if (_turn == _players[0])
                _turn = _players[1];
            else
                _turn = _players[0];
        }

        private void ControlInputPlayer()
        {
            bool isValid = true;
            do
            {
                Console.Write("Ingresa posisicion: ");
                try
                {
                    int index = Convert.ToInt32(Console.ReadLine()) - 1;
                    isValid = SetPositionTurn(index);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Valor no valido, ingresa un numero");
                    isValid = false;
                }
            }
            while (!isValid);
        }

        private bool SetPositionTurn(int index)
        {
            bool isValid = true;

            if (index < 0 || index >= _board.Length)
            {
                Console.WriteLine("Indice no valido");
                return !isValid;
            }

            _board[index] = _turn;

            return isValid;
        }

        private void ShowBoard()
        {
            Console.WriteLine("Tablero actual\n");

            Console.WriteLine($"| {_board[0]} | {_board[1]} | {_board[2]} |");
            Console.WriteLine($"| {_board[3]} | {_board[4]} | {_board[5]} |");
            Console.WriteLine($"| {_board[6]} | {_board[7]} | {_board[8]} |");

            Console.WriteLine();
        }
    }
}
