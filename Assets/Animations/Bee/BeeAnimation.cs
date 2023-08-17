using UnityEngine;

namespace Animations.Bee
{
    public class BeeAnimation : MonoBehaviour
    {
        private Enemies.Bee _beeScript;
        [SerializeField] private Animator beeAnimator;
        private SpriteRenderer _beeSprite;
        private static readonly int HorizontalSpeed = Animator.StringToHash("horizontalSpeed");
        private static readonly int VerticalSpeed = Animator.StringToHash("verticalSpeed");

        private void Start()
        {
            _beeScript = GetComponent<Enemies.Bee>();
            _beeSprite = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            ManageDirection();
        }

        private void ManageDirection()
        {
            beeAnimator.SetFloat(HorizontalSpeed, Mathf.Abs(_beeScript.GetHorizontalSpeed()));
            beeAnimator.SetFloat(VerticalSpeed, _beeScript.GetVerticalSpeed());
            _beeSprite.flipX = _beeScript.IsMovingRight();
        }
    }
}