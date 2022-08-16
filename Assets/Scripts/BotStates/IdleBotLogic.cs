using UnityEngine;

namespace DefaultNamespace.BotStates
{
    [CreateAssetMenu(fileName = "IdleBotLogic", menuName = "Bot/IdleBotLogic")]
    public class IdleBotLogic : BaseBotLogic
    {
        [SerializeField] private AnimationClip[] clips;
        [SerializeField] private Animation animation;
        private int currentClip;
        private Transform _target;
        public override void Initialize(BaseData data)
        {
            
        }

        public void SetTarget(Transform transform)
        {
            _target = transform;
        }

        public override void Execute()
        {
            if (!animation.isPlaying)
            {
                int random;
                do
                {
                    random = Random.Range(0, clips.Length);
                } while (currentClip == random);

                currentClip = random;
                animation.clip = clips[currentClip];
                animation.Play();
            }

            Debug.Log("IdleBotLogic");
        }
    }
}