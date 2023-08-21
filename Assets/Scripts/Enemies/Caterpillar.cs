using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Caterpillar : Enemy
    {
        public enum Direction
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
                _previousDirection = _currentDirection;
                _currentDirection = Direction.Up;
                _isHorizontalMovement = false;
            }
            else if (new Vector2(_transform.position.x, _transform.position.y) == new Vector2(_minX, _maxY))
            {
                _target = new Vector2(_maxX, _maxY);
                _previousDirection = _currentDirection;
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
                switch (_currentDirection)
                {
                    case Direction.Right or Direction.Left:
                        SetTargetToDown();
                        _previousDirection = _currentDirection;
                        _currentDirection = Direction.Down;
                        break;
                    case Direction.Down when _previousDirection == Direction.Right:
                        SetTargetToMaxLeft();
                        _previousDirection = _currentDirection;
                        _currentDirection = Direction.Left;
                        break;
                    case Direction.Down when _previousDirection == Direction.Left:
                        SetTargetToMaxRight();
                        _previousDirection = _currentDirection;
                        _currentDirection = Direction.Right;
                        break;
                }
            }
            else
            {
                switch (_currentDirection)
                {
                    case Direction.Up or Direction.Down:
                        SetTargetToLeft();
                        _previousDirection = _currentDirection;
                        _currentDirection = Direction.Left;
                        break;
                    case Direction.Left when _previousDirection == Direction.Down:
                        SetTargetToMaxUp();
                        _previousDirection = _currentDirection;
                        _currentDirection = Direction.Up;
                        break;
                    case Direction.Left when _previousDirection == Direction.Up:
                        SetTargetToMaxDown();
                        _previousDirection = _currentDirection;
                        _currentDirection = Direction.Down;
                        break;
                }
            }
        }

        private void SetTargetToLeft() // so acontece no verticalmovement
        {
            _target = new Vector2(_target.x - 1, _target.y);
        }

        private void SetTargetToDown() // so acontece no horizontalmovement
        {
            _target = new Vector2(_target.x, _target.y - 1);
        }

        private void SetTargetToMaxLeft() // so acontece no horizontalmovement
        {
            _target = new Vector2(_minX, _target.y);
        }

        private void SetTargetToMaxRight() // so acontece no horizontalmovement
        {
            _target = new Vector2(_maxX, _target.y);
        }

        private void SetTargetToMaxUp() // so acontece no verticalmovement
        {
            _target = new Vector2(_target.x, _maxY);
        }

        private void SetTargetToMaxDown() // so acontece no verticalmovement
        {
            _target = new Vector2(_target.x, _minY);
        }


        private bool HasArrivedOnTarget()
        {
            return _target.x == _transform.position.x && _target.y == _transform.position.y;
        }

        public bool IsHorizontalMovement()
        {
            return _isHorizontalMovement;
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

        public Direction GetDirection()
        {
            return _currentDirection;
        }

        public Direction GetPreviousDirection()
        {
            return _previousDirection;
        }
    }
}