using UnityEngine;

namespace Enemies
{
    public class Ant : Enemy
    {
        public Transform destination;

        private void Start()
        {
            Vector2.MoveTowards(gameObject.transform.position, destination.position, .1f);
        }

        private void Update()
        {
        }
    }
}