using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject shoot;

    [SerializeField]
    private SystemController system;
    [SerializeField]
    private float speedFollow = 2f;

    // Update is called once per frame
    void Update()
    {        
        FollowPlayerOrShoot();        
    }

    void FollowPlayerOrShoot()
    {
        if (system.currentPlayer)
        {
            GameObject player = system.currentPlayer;
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }                              
    }
}
