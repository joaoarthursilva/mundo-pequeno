using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class Ant : Enemy
    {
        private Rigidbody2D _rb;
        private Vector2 _targetAnthillPosition;
        private Vector2 _startingAnthillPosition;
        private Transform _transform;
        private bool _canMove;
        private bool _canEnterAnthill;
        [SerializeField] private float timeToRespawn = .5f;
        [SerializeField] private float movementDelay = 1f;
        [SerializeField] private ManageAnts manageAnts;
        [SerializeField] private bool isFront;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
            if (isFront)
            {
                Spawn();
                InvokeRepeating(nameof(Move), 0f, movementDelay);
            }
            else
            {
                Invoke(nameof(Spawn), movementDelay);
                InvokeRepeating(nameof(Move), movementDelay, movementDelay);
            }
        }

        private void Spawn()
        {
            _canEnterAnthill = false;
            _startingAnthillPosition = manageAnts.GetSpawn();
            Debug.Log(_startingAnthillPosition);
            _targetAnthillPosition = manageAnts.GetCurrentTarget();
            Debug.Log(_targetAnthillPosition);
            _transform.position = _startingAnthillPosition;
            _canMove = true;
        }


        private void Despawn()
        {
            _canMove = false;
            _transform.position = new Vector3(1000, 1000, 1000);
            Invoke(nameof(Spawn), timeToRespawn);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out PlayerStats playerStats);
            if (playerStats)
                playerStats.Kill();
            if (!_canEnterAnthill) return;
            col.TryGetComponent(out Anthill anthill);
            if (!anthill) return;
            if (_canEnterAnthill)
                Despawn();
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            col.TryGetComponent(out Anthill anthill);
            if (!anthill) return;
            _canEnterAnthill = true;
        }

        private bool _toggleMovementDirection = true;

        private void Move()
        {
            if (!_canMove) return;
            var position = _transform.position;

            if (_targetAnthillPosition.x > position.x && _targetAnthillPosition.y > position.y)
            {
                if (_toggleMovementDirection)
                {
                    _rb.MovePosition(new Vector2(position.x + 1, position.y));
                    _toggleMovementDirection = false;
                }
                else
                {
                    _rb.MovePosition(new Vector2(position.x, position.y + 1));
                    _toggleMovementDirection = true;
                }
            }
            else if (_targetAnthillPosition.x > position.x)
            {
                _rb.MovePosition(new Vector2(position.x + 1, position.y));
            }
            else if (_targetAnthillPosition.y > position.y)
            {
                _rb.MovePosition(new Vector2(position.x, position.y + 1));
            }
            else if (_targetAnthillPosition.x < position.x)
            {
                _rb.MovePosition(new Vector2(position.x - 1, position.y));
            }
            else if (_targetAnthillPosition.y < position.y)
            {
                _rb.MovePosition(new Vector2(position.x, position.y - 1));
            }

            // _rb.MovePosition(Vector2.MoveTowards(_transform.position, _targetAnthillPosition, .1f));
        }
    }
}