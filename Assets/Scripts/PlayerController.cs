using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _body;

    private float _horizontal;
    private float _vertical;

    [SerializeField] private float normalRunSpeed = 7.0f;
    private float _currentMoveSpeed;
    private bool _canMove;
    private SpriteRenderer _playerSpriteRenderer;

    private Animator _animator;
    private static readonly int HorizontalSpeed = Animator.StringToHash("horizontalSpeed");
    private static readonly int Vertical = Animator.StringToHash("vertical");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _currentMoveSpeed = normalRunSpeed;
        TurnOnMovement();
        _body = GetComponent<Rigidbody2D>();
        _body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");


        if (Mathf.Abs(_horizontal) > Mathf.Abs(_vertical))
        {
            if (_horizontal > 0)
            {
                _playerSpriteRenderer.flipX = true;
            }
            else
            {
                _playerSpriteRenderer.flipX = false;
            }

            _animator.SetFloat(HorizontalSpeed, Mathf.Abs(_horizontal));
            _animator.SetFloat(Vertical, 0);
        }
        else
        {
            _animator.SetFloat(HorizontalSpeed, 0);
            _animator.SetFloat(Vertical, _vertical);
        }
    }

    private void FixedUpdate()
    {
        if (_canMove)
            Move();
        else
            StopMoving();
    }

    private void Move()
    {
        _body.velocity = new Vector2(_horizontal * _currentMoveSpeed, _vertical * _currentMoveSpeed);
    }

    private void StopMoving()
    {
        _body.velocity = Vector2.zero;
    }

    public void TurnOffMovement()
    {
        _canMove = false;
    }

    private void TurnOnMovement()
    {
        _canMove = true;
    }

    public void SlowDown(float time)
    {
        _currentMoveSpeed /= 2;
        Invoke(nameof(NormalizeMovement), time);
    }

    private void NormalizeMovement()
    {
        _currentMoveSpeed = normalRunSpeed;
    }
}