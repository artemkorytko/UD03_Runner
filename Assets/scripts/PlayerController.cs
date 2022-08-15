using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float roadWidth = 3f;
    [SerializeField] private float turnRotationAngle = 20f;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private Transform view;
    [SerializeField] private GameObject deadEffect;

    private Animator _animator;

    private bool _isActive;

    public event Action OnDied;
    public event Action OnFinish;
    
    private const string RUN = "Run";
    private const string FAIL = "Fail";
    private const string DANCE = "Dance";
    
    private static readonly int Run = Animator.StringToHash(RUN);
    private static readonly int Fail = Animator.StringToHash(FAIL);
    private static readonly int Dance = Animator.StringToHash(DANCE);

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
        deadEffect.SetActive(false);
    }

    private void Update()
    {
        if (!_isActive)
            return;

        Move();
    }

    private void Move()
    {
        float offset = inputHandler.HorizontalAxis * roadWidth;
        Vector3 position = transform.localPosition;
        position.x += offset;
        float roadDelta = roadWidth * 0.5f;
        position.x = Mathf.Clamp(position.x, -roadDelta, roadDelta);

        
        Vector3 rotation = view.localRotation.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, offset !=0? Mathf.Sign(offset) * turnRotationAngle: 0, lerpSpeed*Time.deltaTime);
        view.localRotation = Quaternion.Euler(rotation);
        
        transform.localPosition = position;
        
        transform.Translate(transform.forward * (forwardSpeed * Time.deltaTime));
    }

    private void Died()
    {
        deadEffect.SetActive(true);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FinishComponent>())
        {
            Finish();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Died();
        }
        
    }

    
}
