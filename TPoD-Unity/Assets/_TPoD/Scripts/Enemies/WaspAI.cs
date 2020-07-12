﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPoD
{
    public class WaspAIState : IAIState
    {
        public WaspAIAction action;

        public void Reset()
        {
            action = WaspAIAction.None;
        }

        public WaspAIState() 
        {
            this.action = WaspAIAction.None; 
        }
    }

    public enum WaspAIAction
    {
        None,
        Move,
        Attack
    }

    public class WaspAI : AbstractStateAI
    {

        /******* Events *******/

        /******* Variables & Properties*******/
        [Header("Attack Action Customizable Parameters")]
        [SerializeField] private float _startDelayTime;
        [SerializeField] private int _numShots;
        [SerializeField] private float _delayBetweenShots;

        [Header("Move Action Customizable Parameters")]
        [SerializeField] private float _movementDistance;
        [SerializeField] private float _maxVerticalDirectionMagnitude;

        [SerializeField] private float _timeToMove;

        private WaspAIState _aiState;
        protected override IAIState aiState => _aiState;

        /******* Monobehavior Methods *******/

        public void Awake()
        {
            _aiState = new WaspAIState();
            StartAI();
        }

        /******* Methods *******/
        public override IEnumerator GetEnumeratorForState(IAIState currentState)
        {
            switch(((WaspAIState)currentState).action)
            {
                case WaspAIAction.Move:
                    yield return MoveAction();
                    break;
                case WaspAIAction.Attack:
                    yield return AttackAction();
                    break;
            }
            yield break;
        }

        public IEnumerator AttackAction()
        {
            yield return new WaitForSeconds(_startDelayTime);
            for (int i = 0; i < _numShots; i++)
            {
                //Random.insideUnitCircle

                // Shoot Logic
                // TODO
                yield return new WaitForSeconds(_delayBetweenShots);
            }
        }

        public IEnumerator MoveAction()
        {
            while (true)
            {
                Vector3 moveDirection = GetRandomMoveDirection();
                RaycastHit raycastHit;
                int enemyShouldNotTouchLayer = 1 << LayerMask.NameToLayer(TrampolineConstants.LayerConstants.ENEMY_SHOULD_NOT_TOUCH);
                Debug.DrawRay(transform.position, moveDirection * _movementDistance, Color.red, 10f);
                if (!Physics.Raycast(transform.position, moveDirection, _movementDistance, enemyShouldNotTouchLayer, QueryTriggerInteraction.Collide))
                {
                    float _currentTime = 0f;
                    Vector3 startPosition = transform.position;
                    Vector3 targetPosition = startPosition + (moveDirection * _movementDistance);

                    while (_currentTime < _timeToMove)
                    {
                        _currentTime += Time.deltaTime;
                        float t = _currentTime / _timeToMove;
                        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                        yield return null;
                    }
                    break;
                }
            }

        }

        private Vector3 GetRandomMoveDirection()
        {
            Vector2 horizontalDirection = Random.insideUnitCircle;
            float verticalDirectionMagnitude = Random.Range(0, _maxVerticalDirectionMagnitude) * ((Random.value < 0.5f) ? -1 : 1);
            return new Vector3(horizontalDirection.x, verticalDirectionMagnitude, horizontalDirection.y).normalized;
        }

        public override IAIState GetNextState(IAIState currentState)
        {
            WaspAIState beeState = (WaspAIState)currentState;
            switch(beeState.action)
            {
                case WaspAIAction.Move:
                    beeState.action = WaspAIAction.Attack;
                    break;
                case WaspAIAction.None:
                case WaspAIAction.Attack:
                    beeState.action = WaspAIAction.Move;
                    break;
            }
            return beeState;
        }
    }
}
