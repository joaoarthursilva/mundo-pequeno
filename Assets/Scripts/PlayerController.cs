using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _body;

    private float _horizontal;
    private float _vertical;

    [SerializeField] private float normalRunSpeed = 7.0f;
    private float _currentMoveSpeed;
    private bool _canMove;

    private void Start()
    {
        _currentMoveSpeed = normalRunSpeed;
        TurnOnMovement();
        _body = GetComponent<Rigidbody2D>();
        _body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
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