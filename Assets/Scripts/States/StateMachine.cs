using System;
using System.Collections.Generic;
using PuzzlePlatformer.Abstract.States;
namespace PuzzlePlatformer.States
{

    public class StateMachine
    {
        List<StateTransformer> _stateTransformers = new List<StateTransformer>(); // Conditional change from state to state
        List<StateTransformer> _anyStateTransformers = new List<StateTransformer>(); // For example, Dead state

        IState _currentState;
        public void SetState(IState state)
        {
            if (_currentState == state) return;

            _currentState?.OnExit();
            _currentState = state;
            _currentState.OnStart();
        }
        public void StateControl()
        {
            StateTransformer stateTransformer = CheckForTransformer();
            if(stateTransformer != null)
            {
                SetState(stateTransformer.To);
            }
            _currentState.StateControl();
        }
        

        private StateTransformer CheckForTransformer()
        {
            foreach (StateTransformer stateTransformer in _anyStateTransformers)
            {
                if (stateTransformer.Condition.Invoke()) 
                    return stateTransformer;
            }
            foreach (StateTransformer stateTransformer in _stateTransformers)
            {
                if (stateTransformer.Condition.Invoke() && _currentState == stateTransformer.From) 
                    return stateTransformer;
            }
            return null;
        }

        public void AddState(IState to, IState from, System.Func<bool> condition)
        {
            StateTransformer stateTransformer = new StateTransformer(to, from, condition);
            _stateTransformers.Add(stateTransformer);
        }

        public void AddAnyState(IState to, System.Func<bool> condition)
        {
            StateTransformer stateTransformer = new StateTransformer(to, null, condition);
            _anyStateTransformers.Add(stateTransformer);
        }

    }
}
