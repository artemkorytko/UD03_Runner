using UnityEngine;

namespace DefaultNamespace.BotStates
{
    [CreateAssetMenu(fileName = "AttackBotLogic", menuName = "Bot/AttackBotLogic")]
    public class AttackBotLogic : BaseBotLogic
    {
        [SerializeField] private int damage;
        [SerializeField] private int speed;
        private float timer;

        public override void Initialize(BaseData data)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            Debug.Log("AttackBotLogic");
            //logic attack
            timer += Time.deltaTime;
            if (timer > speed)
            {
                //Animation;
                timer = 0;
            }
        }
    }
}