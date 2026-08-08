
    using UnityEngine;

    public class ItemGather : MonoBehaviour
    {

        public void ReleaseItems()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameEvents.OnReleaseItem?.Invoke(transform.GetChild(i).gameObject);
            }
        }
    }
