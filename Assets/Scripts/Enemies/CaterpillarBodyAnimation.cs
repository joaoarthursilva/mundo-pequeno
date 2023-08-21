using UnityEngine;

namespace Enemies
{
    public class CaterpillarBodyAnimation : MonoBehaviour
    {
        [SerializeField] private Sprite leftNormalBody;
        [SerializeField] private Sprite rightNormalBody;
        [SerializeField] private Sprite upNormalBody;
        [SerializeField] private Sprite downNormalBody;

        [SerializeField] private Sprite leftToDownCurveBody;
        [SerializeField] private Sprite rightToDownCurveBody;
        [SerializeField] private Sprite upToRightCurveBody;
        [SerializeField] private Sprite downToRightCurveBody;
        [SerializeField] private Sprite downToLeftCurveBody;

        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;
        private SpriteRenderer _spriteRenderer;
        private Caterpillar.Direction _currentDirection;
        private Caterpillar.Direction _previousDirection;
        private bool _isHorizontalMovement;
        private Caterpillar _caterpillar;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _caterpillar = GetComponent<Caterpillar>();
        }

        private void Update()
        {
            _isHorizontalMovement = _caterpillar.IsHorizontalMovement();
            _currentDirection = _caterpillar.GetDirection();
            _previousDirection = _caterpillar.GetPreviousDirection();
            if (_isHorizontalMovement)
            {
                switch (_currentDirection)
                {
                    case Caterpillar.Direction.Down:
                        _spriteRenderer.sprite = _previousDirection == Caterpillar.Direction.Left
                            ? leftToDownCurveBody
                            : rightToDownCurveBody;
                        break;
                    case Caterpillar.Direction.Up:
                        _spriteRenderer.sprite = upNormalBody;
                        break;
                    case Caterpillar.Direction.Right:
                        _spriteRenderer.sprite = _previousDirection == Caterpillar.Direction.Down
                            ? downToRightCurveBody
                            : rightNormalBody;
                        break;
                    case Caterpillar.Direction.Left:
                        _spriteRenderer.sprite = _previousDirection == Caterpillar.Direction.Down
                            ? downToLeftCurveBody
                            : leftNormalBody;
                        break;
                }
            }
            else
            {
                switch (_currentDirection)
                {
                    case Caterpillar.Direction.Down:
                        _spriteRenderer.sprite = downNormalBody;
                        break;
                    case Caterpillar.Direction.Up:
                        _spriteRenderer.sprite = upNormalBody;
                        break;
                    case Caterpillar.Direction.Right:
                        _spriteRenderer.sprite = rightNormalBody;
                        break;
                    case Caterpillar.Direction.Left:
                        _spriteRenderer.sprite = leftNormalBody;
                        break;
                }
            }
        }
    }
}