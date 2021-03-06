﻿
namespace EGL.FSM
{
    public class StateMachine<T>
    {
        private AbstractState<T> currentState = State<T>.GetEmpty();
        private StateManager<T> Manager;
        private T Entity;
        public StateMachine(StateManager<T> manager)
        {
            Manager = manager;
            Entity = manager.Entity;
        }
        public StateMachine(T entity)
        {
            Entity = entity;
            Manager = new StateManager<T>(Entity);
        }

        public StateManager<T> GetManager()
        {
            return Manager;
        }

        public void Run() 
        {
            AbstractState<T> previous = currentState;
            if (Manager.GetNewState(ref currentState))
            {
                previous.OnExit(Entity);
                currentState.OnEnter(Entity);
                currentState.InState(Entity);
            }
            else
            {
                currentState.InState(Entity);
            }
        }
    }
}
