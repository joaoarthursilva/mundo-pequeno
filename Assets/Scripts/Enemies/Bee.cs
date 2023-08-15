using Pathfinding;
using UnityEngine;

namespace Enemies
{
    public class Bee : Enemy
    {
        private Transform _target;
        public float speed = 300f;
        public float nextWaypointDistance = 3f;
        private Path _path;
        private int _currentWaypoint;
        private Seeker _seeker;
        private Rigidbody2D _rb;
        public float repeatRate = .5f;
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

        private void Movement()
        {
            if (_path == null) return;
            if (_currentWaypoint >= _path.vectorPath.Count)
            {
                return;
            }

            var direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
            var force = direction * (speed * Time.deltaTime);

            _rb.AddForce(force);

            var distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                _currentWaypoint++;
            }
        }
    }
}