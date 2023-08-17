using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Caterpillar : Enemy
    {
        private enum Direction
        {
            Left,
            Right,
            Down,
            Up
        }

        private Queue<Direction> _pathDown;
        private Queue<Direction> _pathUp;
        [SerializeField] private float speed = 7;
        private bool _canMove;
        private Rigidbody2D _rb;
        private Transform _transform;
        private Vector2 _targetPosition;

        [SerializeField] private Transform topLeft;
        [SerializeField] private Transform topRight;
        [SerializeField] private Transform bottomLeft;
        [SerializeField] private Transform bottomRight;

        private void Start()
        {
            _pathDown = new Queue<Direction>();
            _pathUp = new Queue<Direction>();
            ResetPathDownQueue();
            ResetPathUpQueue();

            _canMove = true;
        }

        private void ResetPathDownQueue()
        {
            _pathDown.Clear();
            _pathDown.Enqueue(Direction.Right);
            _pathDown.Enqueue(Direction.Down);
            _pathDown.Enqueue(Direction.Left);
            _pathDown.Enqueue(Direction.Down);
        }

        private void ResetPathUpQueue()
        {
            _pathUp.Clear();
            _pathUp.Enqueue(Direction.Up);
            _pathUp.Enqueue(Direction.Left);
            _pathUp.Enqueue(Direction.Up);
            _pathUp.Enqueue(Direction.Right);
        }

        private void Update()
        {
            if (_canMove)
            {
                VerifyPosition();
            }
        }

        private void VerifyPosition()
        {
            if (new Vector2(_transform.position.x, _transform.position.y) == _targetPosition) // arrived on target
            {
            }
        }

        private void MoveRight()
        {
            var position = _transform.position;
            var target = new Vector2(position.x + 1, position.y);
            _rb.MovePosition(Vector2.MoveTowards(position, target, speed * Time.deltaTime));
        }

        private void MoveDown()
        {
            var position = _transform.position;
            var target = new Vector2(position.x, position.y - 1);
            _rb.MovePosition(Vector2.MoveTowards(position, target, speed * Time.deltaTime));
        }

        private void MoveLeft()
        {
            var position = _transform.position;
            var target = new Vector2(position.x - 1, position.y);
            _rb.MovePosition(Vector2.MoveTowards(position, target, speed * Time.deltaTime));
        }

        private void MoveUp()
        {
            var position = _transform.position;
            var target = new Vector2(position.x, position.y + 1);
            _rb.MovePosition(Vector2.MoveTowards(position, target, speed * Time.deltaTime));
        }

        // private void Move()
        // {
        //     _rb.MovePosition(Vector2.MoveTowards(_transform.position, _targetPosition, speed * Time.deltaTime));
        // }
    }
}