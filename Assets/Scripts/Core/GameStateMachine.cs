using UnityEngine;

namespace GameFlow.Core
{
    public class GameStateMachine
    {
        public GameState State 
        { 
            get; 
            private set; 
        } = GameState.None;

        public bool Has(GameState flag) => (State & flag) != 0;

        public void Add(GameState flag) => State |= flag;

        public void Remove(GameState flag) => State &= ~flag;

        public void Clear() => State = GameState.None;
    }
}