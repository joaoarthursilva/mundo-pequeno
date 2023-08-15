using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _body;

    private float _horizontal;
    private float _vertical;

    [SerializeField] private float runSpeed = 7.0f;
    private bool _canMove;

    private void Start()
    {
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
        _body.velocity = new Vector2(_horizontal * runSpeed, _vertical * runSpeed);
    }

    private void StopMoving()
    {
        _body.velocity = Vector2.zero;
    }

    public void TurnOffMovement()
    {
        _canMove = false;
    }

    public void TurnOnMovement()
    {
        _canMove = true;
    }
}