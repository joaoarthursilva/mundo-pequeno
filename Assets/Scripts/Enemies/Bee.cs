using Pathfinding;
using UnityEngine;

namespace Enemies
{
    public class Bee : Enemy
    {
        private Transform _target;
        public float speed = 600f;
        public float nextWaypointDistance = .5f;
        private Path _path;
        private int _currentWaypoint;
        private Seeker _seeker;
        private Rigidbody2D _rb;
        public float repeatRate = .1f;
        private bool _canMove;

        private void Start()
        {
            _target = FindObjectOfType<PlayerController>().transform;
            _canMove = true;
            _seeker = GetComponent<Seeker>();
            _rb = GetComponent<Rigidbody2D>();
            InvokeRepeating(nameof(UpdatePath), 0f, repeatRate);
        }

        private void UpdatePath()
        {
            if (_seeker.IsDone())
                _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
        }

        private void OnPathComplete(Path p)
        {
            if (p.error) return;

            _path = p;
            _currentWaypoint = 0;
        }

        private void FixedUpdate()
        {
            if (_canMove) Movement();
        }

        private Vector2 _direction;
        private void Movement()
        {
            if (_path == null) return;
            if (_currentWaypoint >= _path.vectorPath.Count)
            {
                return;
            }

            _direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
            var force = _direction * (speed * Time.deltaTime);
            
            _rb.AddForce(force);

            var distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                _currentWaypoint++;
            }
        }
        
        public float GetHorizontalSpeed()
        {
            return _direction.x;
        }
        public float GetVerticalSpeed()
        {
            return _direction.y;
        }

        public bool IsMovingRight()
        {
            return _direction.x >= 0;
        }
    }
}