using UnityEngine;

namespace Enemies
{
    public class Spider : Enemy
    {
        private Vector2 _targetPosition;
        private bool _canMove;
        private Rigidbody2D _rb;
        private Transform _transform;
        [SerializeField] private GameObject spiderWeb;
        [SerializeField] private float releaseSpiderWebDelay;

        private bool _toggleMovementDirection;
        private Animator _spiderAnimator;
        private SpriteRenderer _spiderSpriteRenderer;
        private float _minX, _maxX, _minY, _maxY;

        [SerializeField] private float movementDelay;
        private Vector2 _pos;
        [SerializeField] private float delayAfterArrivingOnTarget;

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

        private void Start()
        {
            _toggleMovementDirection = false;
            _transform = gameObject.transform;
            FindLimits();
            _spiderAnimator = GetComponent<Animator>();
            _spiderSpriteRenderer = GetComponent<SpriteRenderer>();
            _canMove = true;
            _rb = GetComponent<Rigidbody2D>();
            ChangeTarget();
            InvokeRepeating(nameof(ReleaseSpiderWeb), 0f, releaseSpiderWebDelay);
            InvokeRepeating(nameof(Move), movementDelay, movementDelay);
        }

        private void FindLimits()
        {
            _minX = GameObject.FindWithTag("TopLeft").transform.position.x;
            _maxX = GameObject.FindWithTag("TopRight").transform.position.x;
            _minY = GameObject.FindWithTag("BottomLeft").transform.position.y;
            _maxY = GameObject.FindWithTag("TopLeft").transform.position.y;
        }

        private void Update()
        {
            _pos = _transform.position;
        }

        private Direction CheckTargetDirection()
        {
            if (_targetPosition.x > _pos.x && _targetPosition.y > _pos.y)
            {
                return Direction.DiagUpRight;
            }

            if (_targetPosition.x < _pos.x && _targetPosition.y < _pos.y)
            {
                return Direction.DiagDownLeft;
            }

            if (_targetPosition.x > _pos.x && _targetPosition.y < _pos.y)
            {
                return Direction.DiagDownRight;
            }

            if (_targetPosition.x < _pos.x && _targetPosition.y > _pos.y)
            {
                return Direction.DiagUpLeft;
            }

            if (_targetPosition.x > _pos.x)
            {
                return Direction.Right;
            }

            if (_targetPosition.y > _pos.y)
            {
                return Direction.Up;
            }

            if (_targetPosition.x < _pos.x)
            {
                return Direction.Left;
            }

            if (_targetPosition.y < _pos.y)
            {
                return Direction.Down;
            }

            return Direction.None;
        }

        private void FixedUpdate()
        {
            if (new Vector2(_transform.position.x, _transform.position.y) == _targetPosition)
            {
                CantMove();
                ChangeTarget();
            }
        }

        private void CanMove()
        {
            _canMove = true;
        }

        private void CantMove()
        {
            _canMove = false;
            _spiderAnimator.SetInteger(Direction1, 0);
            _spiderSpriteRenderer.flipX = false;
            Invoke(nameof(CanMove), delayAfterArrivingOnTarget);
        }

        private Direction direction;
        private static readonly int Direction1 = Animator.StringToHash("direction");

        private void Move()
        {
            if (!_canMove) return;
            direction = CheckTargetDirection();

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
            _spiderAnimator.SetInteger(Direction1, 1);
            _spiderSpriteRenderer.flipX = false;
            _rb.MovePosition(new Vector2(_pos.x, _pos.y + 1));
        }

        private void MoveDown()
        {
            _spiderAnimator.SetInteger(Direction1, 0);
            _spiderSpriteRenderer.flipX = false;
            _rb.MovePosition(new Vector2(_pos.x, _pos.y - 1));
        }

        private void MoveLeft()
        {
            _spiderAnimator.SetInteger(Direction1, 2);
            _spiderSpriteRenderer.flipX = false;
            _rb.MovePosition(new Vector2(_pos.x - 1, _pos.y));
        }

        private void MoveRight()
        {
            _spiderAnimator.SetInteger(Direction1, 2);
            _spiderSpriteRenderer.flipX = true;
            _rb.MovePosition(new Vector2(_pos.x + 1, _pos.y));
        }

        private void ChangeTarget()
        {
            do
            {
                _targetPosition = new Vector2(Mathf.FloorToInt(Random.Range(_minX + 1, _maxX - 1)) + .5f,
                    Mathf.FloorToInt(Random.Range(_maxY - 1, _minY + 1)) + .5f);
            } while (_targetPosition == new Vector2(_transform.position.x, _transform.position.y));
        }

        private void ReleaseSpiderWeb()
        {
            Instantiate(spiderWeb, _transform.position, Quaternion.identity);
        }
    }
}