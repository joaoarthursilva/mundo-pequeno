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

        private bool _isHorizontalMovement;

        [SerializeField] private float movementDelay;
        private Rigidbody2D _rb;
        private Transform _transform;

        private Direction _currentDirection;
        private Vector2 _target;
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        private void Start()
        {
            _isHorizontalMovement = true;
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();
            _minX = GameObject.FindWithTag("TopLeft").transform.position.x;
            _maxX = GameObject.FindWithTag("TopRight").transform.position.x;
            _minY = GameObject.FindWithTag("BottomLeft").transform.position.y;
            _maxY = GameObject.FindWithTag("TopLeft").transform.position.y;
            _target = new Vector2(_maxX, _maxY);
            _currentDirection = Direction.Right;
            InvokeRepeating(nameof(Move), movementDelay, movementDelay);
        }

        private void Update()
        {
            if (new Vector2(_transform.position.x, _transform.position.y) == new Vector2(_maxX, _minY))
            {
                _target = new Vector2(_maxX, _maxY);
                _currentDirection = Direction.Up;
                _isHorizontalMovement = false;
            }
            else if (new Vector2(_transform.position.x, _transform.position.y) == new Vector2(_minX, _maxY))
            {
                _target = new Vector2(_maxX, _maxY);
                _currentDirection = Direction.Right;
                _isHorizontalMovement = true;
            }

            if (HasArrivedOnTarget()) UpdateTarget();
        }

        private Direction _previousDirection;

        private void UpdateTarget()
        {
            if (_isHorizontalMovement)
            {
                if (_currentDirection is Direction.Right or Direction.Left)
                {
                    _target = new Vector2(_target.x, _target.y - 1);
                    _previousDirection = _currentDirection;
                    _currentDirection = Direction.Down;
                }
                else if (_currentDirection == Direction.Down && _previousDirection == Direction.Right)
                {
                    _target = new Vector2(_minX, _target.y);
                    _previousDirection = _currentDirection;
                    _currentDirection = Direction.Left;
                }
                else if (_currentDirection == Direction.Down && _previousDirection == Direction.Left)
                {
                    _target = new Vector2(_maxX, _target.y);
                    _previousDirection = _currentDirection;
                    _currentDirection = Direction.Right;
                }
            }
            else
            {
                if (_currentDirection is Direction.Up or Direction.Down)
                {
                    _target = new Vector2(_target.x - 1, _target.y);
                    _previousDirection = _currentDirection;
                    _currentDirection = Direction.Left;
                }
                else if (_currentDirection == Direction.Left && _previousDirection == Direction.Down)
                {
                    _target = new Vector2(_target.x, _maxY);
                    _previousDirection = _currentDirection;
                    _currentDirection = Direction.Up;
                }
                else if (_currentDirection == Direction.Left && _previousDirection == Direction.Up)
                {
                    _target = new Vector2(_target.x, _minY);
                    _previousDirection = _currentDirection;
                    _currentDirection = Direction.Down;
                }
            }
        }

        private bool HasArrivedOnTarget()
        {
            return _target.x == _transform.position.x && _target.y == _transform.position.y;
        }

        private void Move()
        {
            var position = _transform.position;

            switch (_currentDirection)
            {
                case Direction.Right:
                    _rb.MovePosition(new Vector2(position.x + 1, position.y));
                    break;
                case Direction.Left:
                    _rb.MovePosition(new Vector2(position.x - 1, position.y));
                    break;
                case Direction.Down:
                    _rb.MovePosition(new Vector2(position.x, position.y - 1));
                    break;
                case Direction.Up:
                    _rb.MovePosition(new Vector2(position.x, position.y + 1));
                    break;
            }
        }
    }
}