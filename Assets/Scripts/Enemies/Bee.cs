using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bee : Enemy
    {
        private Rigidbody2D _rb;
        private Transform _transform;
        private PlayerController _playerController;
        private Vector2 _playerPos;
        [SerializeField] private float movementDelay;
        private SpriteRenderer _beeSprite;
        private Animator _beeAnimator;
        private bool _toggleMovementDirection;

        private void Start()
        {
            _beeAnimator = GetComponent<Animator>();
            _beeSprite = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
            _playerController = FindObjectOfType<PlayerController>();
            InvokeRepeating(nameof(Move), 0f, movementDelay);
            _toggleMovementDirection = false;
        }

        private Vector2 _position;
        private float _playerX;
        private float _playerY;
        private float _beeX;
        private float _beeY;

        private void Update()
        {
            _position = _transform.position;
            _playerX = Mathf.FloorToInt(_playerPos.x) + .5f;
            _playerY = Mathf.FloorToInt(_playerPos.y) + .5f;
            _beeX = Mathf.FloorToInt(_position.x) + .5f;
            _beeY = Mathf.FloorToInt(_position.y) + .5f;

            _playerPos = _playerController.transform.position;
            ManageAnimDirection();
        }

        private AnimDirection _currentAnimDirection;
        private static readonly int AnimDirectionVariable = Animator.StringToHash("direction");

        private void ManageAnimDirection()
        {
            _beeSprite.flipX = false;
            switch (_currentAnimDirection)
            {
                case AnimDirection.Down:
                    _beeAnimator.SetInteger(AnimDirectionVariable, 0);
                    break;
                case AnimDirection.Up:
                    _beeAnimator.SetInteger(AnimDirectionVariable, 1);
                    break;
                case AnimDirection.Left:
                    _beeAnimator.SetInteger(AnimDirectionVariable, 3);
                    break;
                case AnimDirection.Right:
                    _beeSprite.flipX = true;
                    _beeAnimator.SetInteger(AnimDirectionVariable, 3);
                    break;
            }
        }

        private void UpdateAnimDirection(AnimDirection dir)
        {
            _currentAnimDirection = dir;
        }

        private enum AnimDirection
        {
            Left,
            Right,
            Down,
            Up
        }

        private enum Direction
        {
            None,
            Left,
            Right,
            Down,
            Up,
            DiagUpRight,
            DiagDownRight,
            DiagUpLeft,
            DiagDownLeft,
        }

        private Direction CheckTargetDirection()
        {
            if (_playerX > _beeX && _playerY > _beeY)
            {
                return Direction.DiagUpRight;
            }

            if (_playerX < _beeX && _playerY < _beeY)
            {
                return Direction.DiagDownLeft;
            }

            if (_playerX > _beeX && _playerY < _beeY)
            {
                return Direction.DiagDownRight;
            }

            if (_playerX < _beeX && _playerY > _beeY)
            {
                return Direction.DiagUpLeft;
            }

            if (_playerX > _beeX)
            {
                return Direction.Right;
            }

            if (_playerY > _beeY)
            {
                return Direction.Up;
            }

            if (_playerX < _beeX)
            {
                return Direction.Left;
            }

            if (_playerY < _beeY)
            {
                return Direction.Down;
            }

            return Direction.None;
        }

        private void Move()
        {
            var direction = CheckTargetDirection();

            switch (direction)
            {
                case Direction.DiagDownLeft:
                    if (_toggleMovementDirection)
                    {
                        MoveLeft();
                        _toggleMovementDirection = false;
                    }
                    else
                    {
                        MoveDown();
                        _toggleMovementDirection = true;
                    }

                    break;
                case Direction.DiagDownRight:
                    if (_toggleMovementDirection)
                    {
                        MoveRight();
                        _toggleMovementDirection = false;
                    }
                    else
                    {
                        MoveDown();
                        _toggleMovementDirection = true;
                    }

                    break;
                case Direction.DiagUpLeft:
                    if (_toggleMovementDirection)
                    {
                        MoveLeft();
                        _toggleMovementDirection = false;
                    }
                    else
                    {
                        MoveUp();
                        _toggleMovementDirection = true;
                    }

                    break;
                case Direction.DiagUpRight:
                    if (_toggleMovementDirection)
                    {
                        MoveRight();
                        _toggleMovementDirection = false;
                    }
                    else
                    {
                        MoveUp();
                        _toggleMovementDirection = true;
                    }

                    break;
                case Direction.Right:
                    MoveRight();
                    break;
                case Direction.Left:
                    MoveLeft();
                    break;
                case Direction.Down:
                    MoveDown();
                    break;
                case Direction.Up:
                    MoveUp();
                    break;
            }
        }

        private void MoveUp()
        {
            _rb.MovePosition(new Vector2(_beeX, _beeY + 1));
            UpdateAnimDirection(AnimDirection.Up);
        }

        private void MoveDown()
        {
            _rb.MovePosition(new Vector2(_beeX, _beeY - 1));
            UpdateAnimDirection(AnimDirection.Down);
        }

        private void MoveLeft()
        {
            _rb.MovePosition(new Vector2(_beeX - 1, _beeY));
            UpdateAnimDirection(AnimDirection.Left);
        }

        private void MoveRight()
        {
            _rb.MovePosition(new Vector2(_beeX + 1, _beeY));
            UpdateAnimDirection(AnimDirection.Right);
        }
    }
}