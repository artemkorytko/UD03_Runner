using System;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        private int _value;
        public event Action OnCrash;
        public void OnCrashGlassWall()
        {
            _value += 1;
            Debug.Log(_value);
            OnCrash?.Invoke();
        }
    }
}