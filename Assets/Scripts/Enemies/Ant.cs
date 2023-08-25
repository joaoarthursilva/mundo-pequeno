using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ant : Enemy
    {
        private Rigidbody2D _rb;
        private Transform _transform;
        private Vector2 _targetAnthillPosition;
        private Vector2 _startingAnthillPosition;
        private bool _canMove;
        private bool _canEnterAnthill;
        [SerializeField] private float timeToRespawn = .5f;
        [SerializeField] private float movementDelay = .3f;
        private Vector2 _spawn;
        private Vector2 _currentTarget;
        private bool _toggleMovementDirection = true;
        private Vector2 _prevPos;

        private Animator _animator;
        private static readonly int Direction = Animator.StringToHash("direction");
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _transform = gameObject.transform;
            Spawn();
            InvokeRepeating(nameof(Move), movementDelay, movementDelay);
        }

        private Vector2 GetNewTarget()
        {
            do
            {
                _currentTarget = ManageAnthills.GetRandomAnthill().transform.position;
            } while (_currentTarget == _spawn);

            return _currentTarget;
        }

        private Vector2 GetNewSpawn()
        {
            _spawn = ManageAnthills.GetRandomAnthill().transform.position;
            return _spawn;
        }

        private void Spawn()
        {
            _canEnterAnthill = false;
            _startingAnthillPosition = GetNewSpawn();
            _targetAnthillPosition = GetNewTarget();
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

        private void Move()
        {
            if (!_canMove) return;
            var position = _transform.position;

            if (_targetAnthillPosition.x > position.x && _targetAnthillPosition.y > position.y)
            {
                // diagonal pra cima direita
                if (_toggleMovementDirection)
                {
                    MoveRight();
                    _toggleMovementDirection = false;
                }
                else
                {
                    MoveUp();
                    _toggleMovementDirection = true;
                }
            }
            else if (_targetAnthillPosition.x < position.x && _targetAnthillPosition.y > position.y)
            {
                // diagonal pra cima esquerda
                if (_toggleMovementDirection)
                {
                    MoveLeft();
                    _toggleMovementDirection = false;
                }
                else
                {
                    MoveUp();
                    _toggleMovementDirection = true;
                }
            }
            else if (_targetAnthillPosition.x < position.x && _targetAnthillPosition.y < position.y)
            {
                // diagonal pra baixo esquerda
                if (_toggleMovementDirection)
                {
                    MoveLeft();
                    _toggleMovementDirection = false;
                }
                else
                {
                    MoveDown();
                    _toggleMovementDirection = true;
                }
            }
            else if (_targetAnthillPosition.x > position.x && _targetAnthillPosition.y < position.y)
            {
                // diagonal pra baixo direita
                if (_toggleMovementDirection)
                {
                    MoveRight();
                    _toggleMovementDirection = false;
                }
                else
                {
                    MoveDown();
                    _toggleMovementDirection = true;
                }
            }
            else if (_targetAnthillPosition.x > position.x)
            {
                MoveRight();
            }
            else if (_targetAnthillPosition.y > position.y)
            {
                MoveUp();
            }
            else if (_targetAnthillPosition.x < position.x)
            {
                MoveLeft();
            }
            else if (_targetAnthillPosition.y < position.y)
            {
                MoveDown();
            }
        }

        private void MoveUp()
        {
            var position = _transform.position;
            _rb.MovePosition(new Vector2(position.x, position.y + 1));
            _animator.SetInteger(Direction, 1);
            _spriteRenderer.flipX = false;
        }

        private void MoveDown()
        {
            var position = _transform.position;
            _rb.MovePosition(new Vector2(position.x, position.y - 1));
            _animator.SetInteger(Direction, 0);
            _spriteRenderer.flipX = false;
        }

        private void MoveRight()
        {
            var position = _transform.position;
            _rb.MovePosition(new Vector2(position.x + 1, position.y));
            _animator.SetInteger(Direction, 2);
            _spriteRenderer.flipX = true;
        }

        private void MoveLeft()
        {
            var position = _transform.position;
            _rb.MovePosition(new Vector2(position.x - 1, position.y));
            _animator.SetInteger(Direction, 2);
            _spriteRenderer.flipX = false;
        }
    }
}