using UnityEngine;

namespace DefaultNamespace.BotStates
{
    public abstract class BaseBotLogic : ScriptableObject
    {
        [SerializeField] private States _states;

        public States States => _states;

        public abstract void Initialize(BaseData data);
        public abstract void Execute();
    }

    public class BaseData
    {
        
    }
}