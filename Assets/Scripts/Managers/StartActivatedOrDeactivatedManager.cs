using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class StartActivatedOrDeactivatedManager : MonoBehaviour
    {
        public List<GameObject> objectsToStartActivated;
        public List<GameObject> objectsToStartDeactivated;
        private void Awake()
        {
            foreach (var obj in objectsToStartActivated)
            {
                obj.SetActive(true);
            }
            foreach (var obj in objectsToStartDeactivated)
            {
                obj.SetActive(false);
            }
        }

    }
}