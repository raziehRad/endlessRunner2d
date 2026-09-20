
    using UnityEngine;

    public class ItemRotation :  MonoBehaviour
    {
        [SerializeField] private float speed = 180f;

        private void Update()
        {
            transform.Rotate(0f, speed * Time.deltaTime, 0f);
        }
    }
