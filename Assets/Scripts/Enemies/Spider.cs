using UnityEngine;

namespace Enemies
{
    public class Spider : Enemy
    {
        private Vector2 _targetPosition;
        private Vector2 _startingPosition;
        private bool _canMove;
        private Rigidbody2D _rb;
        private Transform _transform;
        [SerializeField] private GameObject spiderWeb;
        [SerializeField] private float releaseSpiderWebDelay;

        [SerializeField] private Transform topLeft;
        [SerializeField] private Transform topRight;

        [SerializeField] private Transform bottomLeft;

        // [SerializeField] private Transform bottomRight;
        private Animator _spiderAnimator;
        private SpriteRenderer _spiderSpriteRenderer;
        private static readonly int IsMovingSideways = Animator.StringToHash("isMovingSideways");
        private static readonly int IsMovingUp = Animator.StringToHash("isMovingUp");
        private static readonly int IsMovingDown = Animator.StringToHash("isMovingDown");

        private void Start()
        {
            _spiderAnimator = GetComponent<Animator>();
            _spiderSpriteRenderer = GetComponent<SpriteRenderer>();
            _canMove = true;
            _rb = GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
            InvokeRepeating(nameof(ReleaseSpiderWeb), 0f, releaseSpiderWebDelay);
        }

        private void Update()
        {
            ManageDirection();
        }

        private void ManageDirection()
        {
            var position = _transform.position;
            var isMovingSideways = _targetPosition.x > position.x || _targetPosition.x < position.x;
            
            var isMovingUp = _targetPosition.y > position.y;
            var isMovingDown = _targetPosition.y < position.y;
            
            if(isMovingUp)
            {
                _spiderAnimator.SetBool(IsMovingDown, false);
                _spiderAnimator.SetBool(IsMovingSideways, false);
                _spiderAnimator.SetBool(IsMovingUp, true);
            }
            if (isMovingDown)
            {
                _spiderAnimator.SetBool(IsMovingUp, false);
                _spiderAnimator.SetBool(IsMovingDown, true);
                _spiderAnimator.SetBool(IsMovingSideways, false);
            }

            if (isMovingSideways)
            {
                _spiderAnimator.SetBool(IsMovingUp, false);
                _spiderAnimator.SetBool(IsMovingDown, false);
                _spiderAnimator.SetBool(IsMovingSideways, true);
            }
            _spiderSpriteRenderer.flipX = _targetPosition.x > position.x;
        }

        private void FixedUpdate()
        {
            if (new Vector2(_transform.position.x, _transform.position.y) == _targetPosition)
            {
                ChangeTarget();
            }

            if (_canMove) Move();
        }

        private void Move()
        {
            _rb.MovePosition(Vector2.MoveTowards(_transform.position, _targetPosition, .1f));
        }

        private void ChangeTarget()
        {
            var topLeftPosition = topLeft.position;
            var topRightPosition = topRight.position;
            var bottomLeftPosition = bottomLeft.position;
            do
            {
                _targetPosition = new Vector2(Random.Range(topLeftPosition.x + 1, topRightPosition.x - 1),
                    Random.Range(topLeftPosition.y - 1, bottomLeftPosition.y + 1));
            } while (_targetPosition == new Vector2(_transform.position.x, _transform.position.y));
        }

        private void ReleaseSpiderWeb()
        {
            Instantiate(spiderWeb, _transform.position, Quaternion.identity);
        }
    }
}