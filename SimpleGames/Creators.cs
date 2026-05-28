using GameInterfaces;
using Games;

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

    public class AdivinarCreator : Creator
    {
        public override string Name => "Adivinar numero de simbolos";
        public override IGame CreateGame() => new Adivinar();
    }

    public class MemoramaCreator : Creator
    {
        public override string Name => "Memorama";
        public override IGame CreateGame() => new Memorama();
    }
}
