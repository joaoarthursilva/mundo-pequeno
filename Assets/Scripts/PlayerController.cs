using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _body;

    private float _horizontal;
    private float _vertical;

    [SerializeField] private float runSpeed = 7.0f;

    private void Start()
    {
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
        _body.velocity = new Vector2(_horizontal * runSpeed, _vertical * runSpeed);
    }
}