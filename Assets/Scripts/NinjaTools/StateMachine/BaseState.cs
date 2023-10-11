using System;
using UnityEngine;

namespace NinjaTools {
    public abstract class BaseState<EState> where EState : Enum {
        private PlayerStateMachine.PlayerState key;

        public BaseState(StateManager<EState> stateMachine, EState key) {
            var logId = "BaseState_ctor";
            if(stateMachine==null) {
                Utils.logw(logId, "StateMachine is null => no-op");
                return;
            }
            StateMachine = stateMachine;
            StateKey = key;
        }

        protected BaseState(PlayerStateMachine.PlayerState key)
        {
            this.key = key;
        }

        public StateManager<EState> StateMachine { get; private set; }
        public EState StateKey { get; private set; }
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract EState GetNextState();
        public abstract void OnTriggerEnter(Collider other);
        public abstract void OnTriggerStay(Collider other);
        public abstract void OnTriggerExit(Collider other);
    }
}