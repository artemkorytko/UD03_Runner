using UnityEngine;

namespace DefaultNamespace.BotStates
{
    [CreateAssetMenu(fileName = "FollowBotLogic", menuName = "Bot/FollowBotLogic")]
    public class FollowBotLogic : BaseBotLogic
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float radius;

        private Vector3 start;
        private Vector3 finish;

        public override void Initialize(BaseData data)
        {
            if (data is FollowData d)
            {
                start = d.point1;
                finish = d.point2;
            }
        }

        public override void Execute()
        {
            MoveBetween(start, finish);
        }

        private void MoveBetween(Vector3 vector3, Vector3 finish1)
        {
            //logic
        }
    }

    public class FollowData : BaseData
    {
        public Vector3 point1;
        public Vector3 point2;
    }
}