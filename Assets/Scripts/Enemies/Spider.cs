using UnityEngine;

namespace Enemies
{
    public class Spider : Enemy
    {
        private Vector2 _targetPosition;
        private Vector2 _startingAnthillPosition;
        private bool _canMove;
        private Rigidbody2D _rb;
        private Transform _transform;

        private void Start()
        {
            _canMove = true;
            _rb = GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
        }

        private void FixedUpdate()
        {
            if (_canMove) Move();
        }

        private void Move()
        {
            _rb.MovePosition(Vector2.MoveTowards(_transform.position, _targetPosition, .1f));
        }
    }
}