using UnityEngine;

namespace Enemies
{
    public class CaterpillarBackAnimation : MonoBehaviour
    {
        [SerializeField] private Sprite rightBack;
        [SerializeField] private Sprite leftBack;
        [SerializeField] private Sprite downBack;
        [SerializeField] private Sprite upBack;
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;
        private SpriteRenderer _spriteRenderer;
        private Caterpillar.Direction _direction;
        private Caterpillar _caterpillar;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _caterpillar = GetComponent<Caterpillar>();
        }

        private void Update()
        {
            _direction = _caterpillar.GetDirection();
            switch (_direction)
            {
                case Caterpillar.Direction.Down:
                    _spriteRenderer.sprite = downBack;
                    break;
                case Caterpillar.Direction.Up:
                    _spriteRenderer.sprite = upBack;
                    break;
                case Caterpillar.Direction.Right:
                    _spriteRenderer.sprite = rightBack;
                    break;
                case Caterpillar.Direction.Left:
                    _spriteRenderer.sprite = leftBack;
                    break;
            }
        }
    }
}