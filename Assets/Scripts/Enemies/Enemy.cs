using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Enemy : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent(out PlayerStats playerStats);
            if (playerStats)
                playerStats.Kill();
        }
    }
}