using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string RUN = "Run";
    private const string FAIL = "Fail";
    private const string DANCE = "Dance";
    private const string IDLE = "Idle";
    private static readonly int Idle = Animator.StringToHash(IDLE);
    private static readonly int Run = Animator.StringToHash(RUN);
    private static readonly int Fail = Animator.StringToHash(FAIL);
    private static readonly int Dance = Animator.StringToHash(DANCE);
    [SerializeField] private float roadWidth = 3;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float turnRotationAngle = 20;
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private InputHandler inputHandler;
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
        if (!_isActive)
            return;

        Move();
    }

    public void SetIdle()
    {
        _animator.SetTrigger(Idle);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FinishComponent>())
        {
            Finish();
        }

        if (collision.gameObject.GetComponent<WallComponent>())
        {
            Died();
        }
        
        if (collision.gameObject.TryGetComponent(out NonDamageWall noWall))
        {
            noWall.OffWall();
        }
    }
    
    private void Move()
    {
        float offset = inputHandler.HorizontalAxis;
        Vector3 position = transform.localPosition;
        position.x += offset;
        float roadDelta = roadWidth * 0.5f;
        position.x = Mathf.Clamp(position.x, -roadDelta, roadDelta);
        Vector3 rotation = view.localRotation.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, offset != 0 ? Mathf.Sign(offset) * turnRotationAngle : 0, lerpSpeed * Time.deltaTime);
        view.localRotation = Quaternion.Euler(rotation);
        transform.localPosition = position;
        transform.Translate(transform.forward * (forwardSpeed * Time.deltaTime));
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

