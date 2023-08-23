using UnityEngine;

namespace Enemies
{
    public class CaterpillarBackAnimation : MonoBehaviour
    {
        private Caterpillar.Direction _direction;
        private Caterpillar _caterpillar;
        private static readonly int Direction = Animator.StringToHash("direction");
        private Animator _animator;

        private void Start()
        {
            _caterpillar = GetComponent<Caterpillar>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _direction = _caterpillar.GetDirection();
            switch (_direction)
            {
                case Caterpillar.Direction.Down:
                    _animator.SetInteger(Direction, 3);
                    break;
                case Caterpillar.Direction.Up:
                    _animator.SetInteger(Direction, 2);
                    break;
                case Caterpillar.Direction.Right:
                    _animator.SetInteger(Direction, 0);
                    break;
                case Caterpillar.Direction.Left:
                    _animator.SetInteger(Direction, 1);
                    break;
            }
        }
    }
}