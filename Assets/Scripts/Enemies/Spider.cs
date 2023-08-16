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
        private SpiderMovementPoint[] _spiderMovementPoints;
        [SerializeField] private GameObject spiderWeb;

        private void Start()
        {
            _spiderMovementPoints = FindObjectsOfType<SpiderMovementPoint>();
            _canMove = true;
            _rb = GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
        }

        private void FixedUpdate()
        {
            if (new Vector2(_transform.position.x, _transform.position.y) == _targetPosition)
            {
                ChangeTarget();
                ReleaseSpiderWeb();
            }

            if (_canMove) Move();
        }

        private void Move()
        {
            _rb.MovePosition(Vector2.MoveTowards(_transform.position, _targetPosition, .1f));
        }

        private void ChangeTarget()
        {
            do
            {
                _targetPosition = _spiderMovementPoints[Random.Range(0, _spiderMovementPoints.Length)].transform
                    .position;
            } while (_targetPosition == new Vector2(_transform.position.x, _transform.position.y));
        }

        private void ReleaseSpiderWeb()
        {
            Instantiate(spiderWeb, _transform.position, Quaternion.identity);
        }
    }
}