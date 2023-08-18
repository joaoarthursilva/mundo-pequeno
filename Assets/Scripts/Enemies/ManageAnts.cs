using System;
using UnityEngine;

namespace Enemies
{
    public class ManageAnts : MonoBehaviour
    {
        private Vector2 _spawn;
        private Vector2 _currentTarget;

        private void Start()
        {
            GetNewTarget();
            do
            {
                GetNewSpawn();
            } while (_spawn == _currentTarget);
        }

        public Vector2 GetCurrentTarget()
        {
            return _currentTarget;
        }

        public Vector2 GetNewTarget()
        {
            _currentTarget = ManageAnthills.GetRandomAnthill().transform.position;
            return _currentTarget;
        }

        public Vector2 GetSpawn()
        {
            return _spawn;
        }

        public Vector2 GetNewSpawn()
        {
            _spawn = ManageAnthills.GetRandomAnthill().transform.position;
            return _spawn;
        }
    }
}