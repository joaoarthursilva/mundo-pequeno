using System;
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

        private enum Animations
        {
            Up,
            Down,
            Right,
            Left,
            L1,
            L2,
            L3,
            L4
        }

        [SerializeField] private bool isHead;
        [SerializeField] private bool isTail;
        private bool _isHorizontalMovement;

        [SerializeField] private float movementDelay;
        private Rigidbody2D _rb;
        private Transform _transform;
        private Animator _animator;

        private Vector2 _target;
        private float _minX, _maxX, _minY, _maxY;


        private Direction _directionToTarget;
        private Direction _directionOfPreviousTarget;
        private Animations _currentAnim;

        private static readonly int Direction1 = Animator.StringToHash("direction");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _isHorizontalMovement = true;
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody2D>();

            FindLimits();

            _target = new Vector2(_maxX, transform.position.y);
            _directionToTarget = Direction.Right;
            _directionOfPreviousTarget = _directionToTarget;
            _currentAnim = Animations.Right;
            CheckAnimation();
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
            if (HasArrivedOnTarget()) UpdateTarget();
        }

        private void UpdateTarget()
        {
            if (_target == new Vector2(_maxX, _minY))
            {
                _isHorizontalMovement = false;
            }
            else if (_target == new Vector2(_minX, _maxY))
            {
                _isHorizontalMovement = true;
            }

            if (_isHorizontalMovement)
            {
                switch (_directionToTarget)
                {
                    case Direction.Up:
                        SetTargetToRight();
                        if (isHead)
                            _currentAnim = Animations.Up;
                        else if (isTail)
                            _currentAnim = Animations.Right;
                        else
                            _currentAnim = Animations.L4;
                        _directionOfPreviousTarget = Direction.Down;
                        _directionToTarget = Direction.Right;
                        break;
                    case Direction.Left when _directionOfPreviousTarget == Direction.Down:
                        // anim Left
                        SetTargetToMaxLeft();
                        _currentAnim = Animations.Left;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Left;
                        break;
                    case Direction.Right when _directionOfPreviousTarget == Direction.Down:
                        // anim Right
                        SetTargetToMaxRight();
                        _currentAnim = Animations.Right;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Right;
                        break;
                    case Direction.Left when _directionOfPreviousTarget == Direction.Left:
                        // anim L4
                        SetTargetToDown();
                        _currentAnim = Animations.L4;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Down;
                        break;
                    case Direction.Right when _directionOfPreviousTarget == Direction.Right:
                        //anim L1
                        SetTargetToDown();
                        _currentAnim = Animations.L1;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Down;
                        break;
                    case Direction.Down when _directionOfPreviousTarget == Direction.Right:
                        // anim L2
                        _currentAnim = Animations.L2;
                        SetTargetToLeft();
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Left;
                        break;
                    case Direction.Down when _directionOfPreviousTarget == Direction.Left:
                        // anim L3
                        SetTargetToRight();
                        _currentAnim = Animations.L3;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Right;
                        break;
                }
            }
            else
            {
                switch (_directionToTarget)
                {
                    case Direction.Right:
                        SetTargetToUp();
                        if (isHead)
                            _currentAnim = Animations.Right;
                        else if (isTail)
                            _currentAnim = Animations.Up;
                        else
                            _currentAnim = Animations.L2;
                        _directionOfPreviousTarget = Direction.Left;
                        _directionToTarget = Direction.Up;
                        break;
                    case Direction.Up when _directionOfPreviousTarget == Direction.Left:
                        SetTargetToMaxUp();
                        _currentAnim = Animations.Up;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Up;
                        break;
                    case Direction.Down when _directionOfPreviousTarget == Direction.Left:
                        SetTargetToMaxDown();
                        _currentAnim = Animations.Down;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Down;
                        break;
                    case Direction.Up when _directionOfPreviousTarget == Direction.Up:
                        SetTargetToLeft();
                        if (isHead)
                            _currentAnim = Animations.Up;
                        else if (isTail)
                            _currentAnim = Animations.Left;
                        else
                            _currentAnim = Animations.L1;

                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Left;
                        break;
                    case Direction.Down when _directionOfPreviousTarget == Direction.Down:
                        SetTargetToLeft();
                        _currentAnim = isTail ? Animations.Left : Animations.L2;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Left;
                        break;
                    case Direction.Left when _directionOfPreviousTarget == Direction.Down:
                        SetTargetToUp();
                        if (isHead)
                            _currentAnim = Animations.Left;
                        else if (isTail)
                            _currentAnim = Animations.Up;
                        else
                            _currentAnim = Animations.L3;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Up;
                        break;
                    case Direction.Left when _directionOfPreviousTarget == Direction.Up:
                        SetTargetToDown();
                        _currentAnim = Animations.L4;
                        _directionOfPreviousTarget = _directionToTarget;
                        _directionToTarget = Direction.Down;
                        break;
                }
            }

            CheckAnimation();
        }

        private void SetTargetToLeft()
        {
            _target = new Vector2(_target.x - 1, _target.y);
        }

        private void SetTargetToRight()
        {
            _target = new Vector2(_target.x + 1, _target.y);
        }

        private void SetTargetToDown()
        {
            _target = new Vector2(_target.x, _target.y - 1);
        }

        private void SetTargetToUp()
        {
            _target = new Vector2(_target.x, _target.y + 1);
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
            var position = _transform.position;
            return Math.Abs(_target.x - position.x) < .01f && Math.Abs(_target.y - position.y) < .01f;
        }

        private void Move()
        {
            var position = _transform.position;

            switch (_directionToTarget)
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

        private void CheckAnimation()
        {
            switch (_currentAnim)
            {
                case Animations.Down:
                    _animator.SetInteger(Direction1, 1);
                    break;
                case Animations.Up:
                    _animator.SetInteger(Direction1, 3);
                    break;
                case Animations.Left:
                    _animator.SetInteger(Direction1, 0);
                    break;
                case Animations.Right:
                    _animator.SetInteger(Direction1, 2);
                    break;
                case Animations.L1:
                    _animator.SetInteger(Direction1, 4);
                    break;
                case Animations.L2:
                    _animator.SetInteger(Direction1, 5);
                    break;
                case Animations.L3:
                    _animator.SetInteger(Direction1, 6);
                    break;
                case Animations.L4:
                    _animator.SetInteger(Direction1, 7);
                    break;
            }
        }
    }
}