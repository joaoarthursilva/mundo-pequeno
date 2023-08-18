using System;
using UnityEngine;

namespace Enemies
{
    public class CaterpillarHeadAnimation : MonoBehaviour
    {
        [SerializeField] private Sprite rightHead;
        [SerializeField] private Sprite leftHead;
        [SerializeField] private Sprite downHead;
        [SerializeField] private Sprite upHead;
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
                    _spriteRenderer.sprite = downHead;
                    break;
                case Caterpillar.Direction.Up:
                    _spriteRenderer.sprite = upHead;
                    break;
                case Caterpillar.Direction.Right:
                    _spriteRenderer.sprite = rightHead;
                    break;
                case Caterpillar.Direction.Left:
                    _spriteRenderer.sprite = leftHead;
                    break;
            }
        }
    }
}