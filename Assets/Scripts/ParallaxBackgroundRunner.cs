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
        Vector3 delta = _target.position - lastGroundPosition;
        transform.position += new Vector3(delta.x * parallaxMultiplier, 0, 0);
        lastGroundPosition = _target.position;
    }
}