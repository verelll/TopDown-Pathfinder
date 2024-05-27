﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using verell.Architecture;

namespace verell.StateMachine
{
    public abstract class BaseStateMachine : IStateMachine
    {
        private readonly IContainer _container;
        private Dictionary<Type, IState> _allStates;
        private IState _activeState;

        protected BaseStateMachine(IContainer container)
        {
            _container = container;
        }
        
        void IStateMachine.Update() => _activeState?.Update();

        protected void AddState(IState newState)
        {
            _allStates ??= new Dictionary<Type, IState>();
            
            var newStateType = newState.GetType();
            if (_allStates.ContainsKey(newStateType))
            {
                Debug.LogError($"[StateMachine] State with the same type: {newStateType} already exist!");
                return;
            }

            _container.InjectAt(newState);
            _allStates[newStateType] = newState;
        }

        public virtual async UniTask ChangeState<T>() where T : class, IState
        {
            var newStateType = typeof(T);
            var prevState = _activeState;
            if(prevState != null)
            {
                await prevState.Exit();
            }
            
            if (!_allStates.TryGetValue(newStateType, out var state))
            {
                Debug.LogError($"[StateMachine] Cannot find state with type: {newStateType}");
                return;
            }

            _activeState = state;
            
            await _activeState.Enter();
        }
    }
}