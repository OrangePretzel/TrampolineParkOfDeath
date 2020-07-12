using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public interface IAIState
    {
        void Reset();
    }

    public abstract class AbstractStateAI : MonoBehaviour
    {
        /******* Events *******/

        /******* Variables & Properties*******/

        private bool _isAIRunning;
        protected abstract IAIState aiState { get; }

        /******* Monobehavior Methods *******/

        /******* Methods *******/

        public void StartAI()
        {
            _isAIRunning = true;
            aiState.Reset();
            StartCoroutine(RunAIEnumerator(aiState));
        }

        public IEnumerator RunAIEnumerator(IAIState aiState)
        {
            while (_isAIRunning)
            {
                aiState = GetNextState(aiState);
                yield return GetEnumeratorForState(aiState);
            }
        }

        public abstract IAIState GetNextState(IAIState currentState);
        public abstract IEnumerator GetEnumeratorForState(IAIState currentState);
    }
}
