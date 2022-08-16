using UnityEngine;

namespace DefaultNamespace.BotStates
{
    [CreateAssetMenu(fileName = "DeathBotLogic", menuName = "Bot/DeathBotLogic")]
    public class DeathBotLogic : BaseBotLogic
    {
        [SerializeField] private ParticleSystem[] _particleSystems;

        public override void Initialize(BaseData data)
        {
            
        }

        public override void Execute()
        {
            _particleSystems[Random.Range(0, _particleSystems.Length)].Play();
            Debug.Log("DeathBotLogic");
        }
    }
}