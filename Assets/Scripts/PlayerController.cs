using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private const string RUN = "Run";
        private const string FAIL = "Fail";
        private const string DANCE = "Dance";

        private static readonly int Run = Animator.StringToHash(RUN);
        private static readonly int Fail = Animator.StringToHash(FAIL);
        private static readonly int Dance = Animator.StringToHash(DANCE);

        [Header("Move settings")] [SerializeField]
        private float roadWidth = 3;

        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private float turnRotationAngle = 20;
        [SerializeField] private float lerpSpeed = 5;

        [Header("References")] [SerializeField]
        private InputHandler inputHandler;

        [SerializeField] private Transform view;

        private Animator _animator;
        private bool _isActive;

        public event Action OnDied;
        public event Action OnFinish;

        public bool IsActive
        {
            get => _isActive;

            set
            {
                _isActive = value;
                if (_isActive)
                {
                    _animator.SetTrigger(Run);
                }
            }
        }

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (!IsActive)
                return;

            Move();
        }

        private void Move()
        {
            //horizontal move
            float offset = inputHandler.HorizontalAxis * roadWidth;
            Vector3 position = transform.localPosition;
            position.x += offset;
            float roadDelta = roadWidth * 0.5f;
            position.x = Mathf.Clamp(position.x, -roadDelta, roadDelta);

            Vector3 rotation = view.localRotation.eulerAngles;
            rotation.y = Mathf.LerpAngle(rotation.y, offset != 0 ? Mathf.Sign(offset) * turnRotationAngle : 0, lerpSpeed * Time.deltaTime);
            view.localRotation = Quaternion.Euler(rotation);


            transform.localPosition = position;
            //fps == 60 dt = 0.01666 // 0.01666 * 60 == 1
            //fps == 120 dt == 0.008333; // 0.008333 * 120 == 1

            //speed == 5 
            //5 * dt == 
            transform.Translate(transform.forward * forwardSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("OnCollisionEnter");
            if (collision.gameObject.GetComponent<FinishComponent>())
            {
                Finish();
            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                Died();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter");
            GlassWall glassWall = other.gameObject.GetComponentInParent<GlassWall>();
            if (glassWall)
            {
                glassWall.CrashWall();
            }
        }

        private void Died()
        {
            _animator.SetTrigger(Fail);
            IsActive = false;
            OnDied?.Invoke();
        }

        private void Finish()
        {
            _animator.SetTrigger(Dance);
            IsActive = false;
            OnFinish?.Invoke();
        }
    }
}