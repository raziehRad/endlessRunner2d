using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace DefaultNamespace
{
    public class GroundSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool groundPool;
        [SerializeField] private ItemSpawner _itemSpawner;
        [SerializeField] private BackgroundManager _background;

        [SerializeField] private int initialGround = 5;
        [SerializeField] private Vector2 xSpacing;
        [SerializeField] private Vector2 yRange;

        private readonly List<GameObject> grounds = new();
        public IReadOnlyList<GameObject> Grounds => grounds;

        public void Initialize()
        {
            float spawnX = 0f;
            for (int i = 0; i < initialGround; i++)
            {
                Spawn(spawnX, Random.Range(yRange.x, yRange.y), true);
                spawnX += GetWidth(grounds[^1]) + Random.Range(xSpacing.x, xSpacing.y);
            }
        }

        public GameObject Spawn(float x, float y, bool safeSpawn)
        {
            var ground = groundPool.GetFromPool();
            if (ground == null) return null;
            ground.transform.position = new Vector3(x, y, 0);
            ground.SetActive(true);
            grounds.Add(ground);

            _background.Spawn(x, y);
            _itemSpawner.SpawnItems(ground, safeSpawn);
            return ground;
        }

        public void Recycle(GameObject ground)
        {
            ground.SetActive(false);
            grounds.Remove(ground);
        }

        float GetWidth(GameObject obj)
        {
            var g = obj.GetComponent<Ground>();
            return g != null ? g.Data.width : 10f;
        }
    }
}