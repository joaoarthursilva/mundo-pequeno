using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ant : Enemy
    {
        private Rigidbody2D _rb;
        private Vector2 _targetAnthillPosition;
        private Vector2 _startingAnthillPosition;
        private Transform _transform;
        private bool _canMove;
        private bool _canEnterAnthill;
        [SerializeField] private float timeToRespawn = .5f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
            Spawn();
        }

        private void Spawn()
        {
            _canEnterAnthill = false;
            _startingAnthillPosition = ManageAnthills.GetRandomAnthill().transform.position;
            do
            {
                _targetAnthillPosition = ManageAnthills.GetRandomAnthill().transform.position;
            } while (_targetAnthillPosition == _startingAnthillPosition);

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

        private void FixedUpdate()
        {
            if (_canMove) Move();
        }

        private void Move()
        {
            _rb.MovePosition(Vector2.MoveTowards(_transform.position, _targetAnthillPosition, .1f));
        }
    }
}