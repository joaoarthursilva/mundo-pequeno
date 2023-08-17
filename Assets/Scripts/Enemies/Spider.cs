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

        private void Start()
        {
            _canMove = true;
            _rb = GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
            InvokeRepeating(nameof(ReleaseSpiderWeb), 0f, releaseSpiderWebDelay);
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