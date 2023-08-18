using UnityEngine;

namespace Enemies
{
    public class AntAnimation : MonoBehaviour
    {
        private Animator _animator;
        private Ant _ant;
        private SpriteRenderer _spriteRenderer;
        private static readonly int HorizontalSpeed = Animator.StringToHash("horizontalSpeed");
        private static readonly int Vertical = Animator.StringToHash("vertical");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _ant = GetComponent<Ant>();
        }

        private void Update()
        {
            var targetAnthillX = _ant.GetTargetAnthillPosition().x;
            var targetAnthillY = _ant.GetTargetAnthillPosition().y;


            if (targetAnthillX > gameObject.transform.position.x)
            { // pra direita
                _spriteRenderer.flipX = true;
                _animator.SetFloat(HorizontalSpeed, 1);
                _animator.SetFloat(Vertical, 0);
            }
            else if (targetAnthillX < gameObject.transform.position.x)
            { // pra esquerda
                _spriteRenderer.flipX = false;
                _animator.SetFloat(HorizontalSpeed, 1);
                _animator.SetFloat(Vertical, 0);
            }
            else if (targetAnthillY > gameObject.transform.position.y)
            { // pra cima
                _spriteRenderer.flipX = false;
                _animator.SetFloat(HorizontalSpeed, 0);
                _animator.SetFloat(Vertical, 1);
            }
            else if (targetAnthillY < gameObject.transform.position.y)
            { // pra baixo
                _spriteRenderer.flipX = false;
                _animator.SetFloat(HorizontalSpeed, 0);
                _animator.SetFloat(Vertical, -1);
            }
        }
    }
}