using UnityEngine;
using UnityEngine.SceneManagement;

// Keeps the camera following the player while limiting its movement
// based on the player's vertical position.
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float offset;
 
    void LateUpdate()
    {
        if (_player.transform.position.y>-4)
        {
            transform.position =new Vector3( _player.transform.position.x+offset, _player.transform.position.y,-10);
        }

    }
}
