
using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class GroundSpawner : MonoBehaviour
    {
        [SerializeField] private int initialGround = 5;
        [SerializeField] private Vector2 xSpacing;
        [SerializeField] private Vector2 yRange;
        private BackgroundManager _background;
        private ObjectPool groundPool;
        private ItemSpawner _itemSpawner;
        private readonly List<GameObject> grounds = new();
        public IReadOnlyList<GameObject> Grounds => grounds;
        public ObjectPool GroundPool => groundPool;

        private void Awake()
        {
            _background = GetComponent<BackgroundManager>();
            groundPool = GetComponent<ObjectPool>();
            _itemSpawner = GetComponent<ItemSpawner>();
        }

        public void Initialize()
        {
            float spawnX = 0f;
            for (int i = 0; i < initialGround; i++)
            {
                var go = Spawn(spawnX, Random.Range(yRange.x, yRange.y), true);
                spawnX += GetWidth(go) + Random.Range(xSpacing.x, xSpacing.y);
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
            DeChildItems(ground);
            _itemSpawner.SpawnItems(ground, safeSpawn);
            return ground;
        }

        private static void DeChildItems(GameObject ground)
        {
            for (int i = 0; i < ground.transform.childCount; i++)
            {
                if ( ground.transform.GetChild(i).CompareTag("GroundItem")) return;
                ground.transform.GetChild(i).gameObject.SetActive(false);
                ground.transform.GetChild(i).SetParent(null);
            }
        }

        public void Recycle(GameObject ground)
        {
            ground.SetActive(false);
            grounds.Remove(ground);
        }

       public float GetWidth(GameObject obj)
        {
            var g = obj.GetComponent<Ground>();
            return g != null ? g.Data.width : 10f;
        }
    }
}