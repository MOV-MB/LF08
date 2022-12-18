using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public float z = -5f;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1, z);
    }
}