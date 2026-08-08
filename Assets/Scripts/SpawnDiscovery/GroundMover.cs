using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GroundMover : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private GroundSpawner _spawner;

        private void Awake()
        {
            _spawner = GetComponent<GroundSpawner>();
        }

        private void OnEnable()
        {
            GameEvents.OnSpeedChanged += SetSpeed;
        }
        private void OnDisable()
        {
            GameEvents.OnSpeedChanged -= SetSpeed;
        }
        public void Tick()
        {
            foreach (var g in _spawner.Grounds)
            {
                g.transform.Translate(Vector3.left*speed*Time.deltaTime);
            }
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }
}