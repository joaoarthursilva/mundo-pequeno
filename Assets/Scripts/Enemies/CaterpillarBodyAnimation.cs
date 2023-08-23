using System;
using UnityEngine;

namespace Enemies
{
    public class CaterpillarBodyAnimation : MonoBehaviour
    {
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;
        private Caterpillar.Direction _currentDirection;
        private Caterpillar.Direction _previousDirection;
        private bool _isHorizontalMovement;
        private Caterpillar _caterpillar;
        private Animator _animator;
        private static readonly int Direction = Animator.StringToHash("direction");
        private Transform _transform;

        private void Start()
        {
            _transform = gameObject.transform;
            _caterpillar = GetComponent<Caterpillar>();
            _animator = GetComponent<Animator>();
            _minX = GameObject.FindWithTag("TopLeft").transform.position.x;
            _maxX = GameObject.FindWithTag("TopRight").transform.position.x;
            _minY = GameObject.FindWithTag("BottomLeft").transform.position.y;
            _maxY = GameObject.FindWithTag("TopLeft").transform.position.y;
        }

        private void Update()
        {
            _isHorizontalMovement = _caterpillar.IsHorizontalMovement();
            _currentDirection = _caterpillar.GetDirection();
            _previousDirection = _caterpillar.GetPreviousDirection();
            if (_isHorizontalMovement)
            {
                switch (_currentDirection)
                {
                    case Caterpillar.Direction.Down:
                        _animator.SetInteger(Direction, Math.Abs(_transform.position.x - _minX) < .1 ? 4 : 5);

                        break;
                    case Caterpillar.Direction.Right:
                        _animator.SetInteger(Direction, Math.Abs(_transform.position.x - _minX) < .1f ? 6 : 2);

                        break;
                    case Caterpillar.Direction.Left:
                        if (Math.Abs(_transform.position.x - _maxX) < .1)
                        {
                            _animator.SetInteger(Direction, 5);
                        }
                        else
                        {
                            _animator.SetInteger(Direction, 0);
                        }
                        
                        break;
                }
            }
            else
            {
                switch (_currentDirection)
                {
                    case Caterpillar.Direction.Down:
                        _animator.SetInteger(Direction, Math.Abs(_transform.position.y - _maxY) < .1 ? 7 : 1);

                        break;
                    case Caterpillar.Direction.Up:
                        _animator.SetInteger(Direction, Math.Abs(_transform.position.y - _minY) < .1 ? 6 : 3);
                        break;
                    case Caterpillar.Direction.Left:
                        _animator.SetInteger(Direction, Math.Abs(_transform.position.y - _minY) < .1f ? 6 : 3);
                        break;
                }
            }
        }
    }
}