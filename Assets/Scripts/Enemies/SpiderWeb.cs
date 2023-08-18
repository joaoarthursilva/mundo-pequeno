using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpiderWeb : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy;
        [SerializeField] private float slowDownDuration;
        private void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            Invoke(nameof(DestroyObject), timeToDestroy);
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out PlayerController playerController);
            if (!playerController) return;
            playerController.SlowDown(slowDownDuration);
        }
    }
}