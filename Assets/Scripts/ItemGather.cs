
    using UnityEngine;

    public class ItemGather : MonoBehaviour
    {

        public void ReleaseItems()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameManager.Instance.GroundManager.Spawner.ItemSpawner.ReleaseItem(transform.GetChild(i).gameObject);
            }
        }
    }
