
    using UnityEngine;
    using UnityEngine.Serialization;

    public class Ground : MonoBehaviour
    {
        [SerializeField] private GroundData data;
         public GroundData Data=>data;
    }
