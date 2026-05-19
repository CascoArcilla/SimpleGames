using GameInterfaces;
using GameGato;

namespace Crators
{
    public abstract class Creator
    {
        public abstract string Name { get; }

        public abstract IGame CreateGame();
    }

    public class GatoCreator : Creator
    {
        public override string Name => "Tik tak toe";

        public override IGame CreateGame() => new Gato();
    }
}
