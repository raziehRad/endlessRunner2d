using UnityEngine;

namespace DefaultNamespace
{
    public class GroundMover : MonoBehaviour
    {
        [SerializeField] private GroundSpawner _spawner;
        [SerializeField] private float speed = 5f;
        public float MoveSpeed => speed;

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