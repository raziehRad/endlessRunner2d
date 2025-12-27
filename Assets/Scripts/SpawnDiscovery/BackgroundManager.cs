using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private ObjectPool backPool;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private Vector2 yOffsetRange = new Vector2(3, 8);

        private readonly List<GameObject> backs = new();

        public IReadOnlyList<GameObject> Backs => backs;

        public void Spawn(float x, float groundY)
        {
            var back = backPool.GetFromPool();
            if (back == null) return;

            back.transform.position = new Vector3(
                x,
                groundY + Random.Range(yOffsetRange.x, yOffsetRange.y),
                0
            );

            back.SetActive(true);
            backs.Add(back);
        }

        public void Tick()
        {
            foreach (var back in backs)
                back.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            RecycleIfNeeded();
        }

        private void RecycleIfNeeded()
        {
            if (backs.Count == 0) return;

            var first = backs[0];
            var last = backs[^1];

            float rightEdge = Camera.main.transform.position.x +
                              Camera.main.orthographicSize * Camera.main.aspect;

            if (last.transform.position.x < rightEdge)
            {
                first.SetActive(false);
                backs.RemoveAt(0);
                Spawn(last.transform.position.x + 10f, last.transform.position.y);
            }
        }

        public void SetSpeed(float speed)
        {
            moveSpeed = speed;
        }
    }
}