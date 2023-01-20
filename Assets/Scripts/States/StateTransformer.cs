using PuzzlePlatformer.Abstract.States;

namespace PuzzlePlatformer.States
{

    public class StateTransformer
    {
        public IState To { get; }
        public IState From { get; }
        public System.Func<bool> Condition { get; }

        public StateTransformer(IState from, IState to, System.Func<bool> condition)
        {
            To = to;
            From = from;
            Condition = condition;
        }
    }
}