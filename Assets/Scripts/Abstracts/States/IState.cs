using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzlePlatformer.Abstract.States
{

    public interface IState
    {
        void StateControl();
        void OnStart();
        void OnExit();
    }
}
