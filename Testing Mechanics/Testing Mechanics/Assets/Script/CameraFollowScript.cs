using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject playerRef;
    public float heightDifference;

    private Vector2 selfPos;
    private Vector2 playerPosRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        selfPos = new Vector2 (transform.position.x, transform.position.y);
        playerPosRef = new Vector2 (playerRef.transform.position.x, playerRef.transform.position.y + heightDifference);
        if(selfPos != playerPosRef)
        {
            selfPos = playerPosRef;
            transform.position = new Vector3(selfPos.x, selfPos.y, transform.position.z);
        }
    }
}
