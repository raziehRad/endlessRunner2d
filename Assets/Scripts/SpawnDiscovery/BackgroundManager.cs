using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Manages background spawning, movement, and recycling
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private ObjectPool backPool;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private Vector2 yOffsetRange = new Vector2(3, 8);
        [Header("X Spawn")] private Vector2 Xspawning = new Vector2(1, 3);
        [Header("Y Spawn")] private Vector2 Yspawning = new Vector2(-1, 1);
        private readonly List<GameObject> backs = new();
        
        private GroundSpawner _spawner;

        private void Start()
        {
            _spawner = GetComponent<GroundSpawner>();
        }
        // Spawn a background object at the specified position
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
        // Move active background objects and recycle them when needed
        public void Tick()
        {
            foreach (var back in backs)
                back.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            RecycleIfNeeded();
        }
        // Recycle the oldest background object and spawn a new one
        private void RecycleIfNeeded()
        {
            if (backs.Count == 0) return;

            var first = backs[0];
            var last = backs[^1];

            float rightEdge = Camera.main.transform.position.x +
                              Camera.main.orthographicSize * Camera.main.aspect;

            if (backPool != null && last.transform.position.x+ _spawner.GetWidth(last)/2 
                <rightEdge+ _spawner.GetWidth(backPool.gameObject))
            {
                float spacing = Random.Range(Xspawning.x, Xspawning.y);
                float spawnX = last.transform.position.x +_spawner. GetWidth(last) + spacing;
                float yPos = Random.Range(Yspawning.x, Yspawning.y);
                
                first.SetActive(false);
                backs.RemoveAt(0);
                Spawn(spawnX, yPos);
            }
        }

        public void SetSpeed(float speed)
        {
            moveSpeed = speed;
        }
    }
