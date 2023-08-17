using UnityEngine;

namespace Enemies
{
    public class Ant : Enemy
    {
        private Rigidbody2D _rb1;
        private Rigidbody2D _rb2;
        private Vector2 _targetAnthillPosition;
        private Vector2 _startingAnthillPosition;
        private Transform _transform;
        private bool _canMove;
        private bool _canEnterAnthill;
        [SerializeField] private float timeToRespawn = .5f;
        [SerializeField] private float movementDelay = 1f;
        [SerializeField] private GameObject antSpriteFront;
        [SerializeField] private GameObject antSpriteBack;

        private void Start()
        {
            _rb1 = antSpriteFront.GetComponent<Rigidbody2D>();
            _rb2 = antSpriteBack.GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
            Spawn();
            InvokeRepeating(nameof(Move), 0f, movementDelay);
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

        private bool _toggleMovementDirection = true;

        private void Move()
        {
            if (!_canMove) return;
            var position = _transform.position;

            if (_targetAnthillPosition.x > position.x && _targetAnthillPosition.y > position.y)
            {
                if (_toggleMovementDirection)
                {
                    _rb1.MovePosition(new Vector2(position.x + 1, position.y));
                    _rb2.MovePosition(new Vector2(position.x + 1, position.y));
                    _toggleMovementDirection = false;
                }
                else
                {
                    _rb1.MovePosition(new Vector2(position.x, position.y + 1));
                    _rb2.MovePosition(new Vector2(position.x, position.y + 1));
                    _toggleMovementDirection = true;
                }
            }
            else if (_targetAnthillPosition.x > position.x)
            {
                _rb1.MovePosition(new Vector2(position.x + 1, position.y));
                _rb2.MovePosition(new Vector2(position.x + 1, position.y));
            }
            else if (_targetAnthillPosition.y > position.y)
            {
                _rb1.MovePosition(new Vector2(position.x, position.y + 1));
                _rb2.MovePosition(new Vector2(position.x, position.y + 1));
            }
            else if (_targetAnthillPosition.x < position.x)
            {
                _rb1.MovePosition(new Vector2(position.x - 1, position.y));
                _rb2.MovePosition(new Vector2(position.x - 1, position.y));
            }
            else if (_targetAnthillPosition.y < position.y)
            {
                _rb1.MovePosition(new Vector2(position.x, position.y - 1));
                _rb2.MovePosition(new Vector2(position.x, position.y - 1));
            }
        }
    }
}