using System;
using UnityEngine;

public class ParallaxBackgroundRunner : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float parallaxMultiplier;
    private Vector3 lastGroundPosition;

    private void Start()
    {
        lastGroundPosition = _target.position;
    }

    private void Update()
    {
        transform.position = new Vector3( _target.position.x, 0, 0);
    }

    
}
