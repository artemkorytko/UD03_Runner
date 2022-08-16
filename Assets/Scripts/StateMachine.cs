using System;
using DefaultNamespace.BotStates;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public enum States
    {
        None,
        IDLE,
        ATTACK,
        FOLLOW,
        DEATH
    }

    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private States states;
        [SerializeField] private BaseBotLogic[] logics;

        private BaseBotLogic _currentLogic;

        private States States
        {
            get => states;
            set
            {
                if (states == value)
                    return;
                states = value;
                foreach (var logic in logics)
                {
                    if (logic.States == states)
                    {
                        _currentLogic = logic;
                        break;
                    }
                }

                switch (states)
                {
                    case States.IDLE:
                        _currentLogic.Initialize(new IdleData() {delayForChangeAnimation =  Random.Range(3, 5)});
                        break;
                    case States.ATTACK:
                        _currentLogic.Initialize(new AttackData() {});
                        break;
                    case States.FOLLOW:
                        _currentLogic.Initialize(new FollowData() {});
                        break;
                    case States.DEATH:
                        _currentLogic.Initialize(new DeathData() {});
                        break;
                }
            }
        }

        private void Update()
        {
            _currentLogic.Execute();
        }
    }

    internal class DeathData : BaseData
    {
        public ParticleSystem[] _particleSystems;
    }

    internal class AttackData : BaseData
    {
        public Transform target;
    }

    internal class IdleData : BaseData
    {
        public float delayForChangeAnimation;
    }
}