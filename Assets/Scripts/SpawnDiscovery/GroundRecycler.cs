using UnityEngine;

namespace DefaultNamespace
{
    public class GroundRecycler : MonoBehaviour
    {
        [SerializeField] private GroundSpawner _spawner;
        [SerializeField] private Vector2 xSpacing;
        [SerializeField] private Vector2 yRange;

        public void Tick()
        {
            if (_spawner.Grounds.Count==0)return;

            var first = _spawner.Grounds[0];
            var last = _spawner.Grounds[^1];

            float rightEdge = Camera.main.transform.position.x +
                              Camera.main.orthographicSize * Camera.main.aspect;
            if (last.transform.position.x+ _spawner.GetWidth(last)/2 <rightEdge+ _spawner.GetWidth(_spawner.GroundPool.gameObject))
            {
                float spacing = Random.Range(xSpacing.x, xSpacing.y);
                float spawnX = last.transform.position.x + _spawner.GetWidth(last) + spacing;
                float y = Random.Range(yRange.x, yRange.y);
                
                _spawner.Recycle(first);
                _spawner.Spawn(spawnX, y, false);
            }
        }
    }
}