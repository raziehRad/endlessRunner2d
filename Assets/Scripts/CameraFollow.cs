using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_player.transform.position.y>-4)
        {
            transform.position =new Vector3( _player.transform.position.x+offset, _player.transform.position.y,-10);
        }

    }
}
