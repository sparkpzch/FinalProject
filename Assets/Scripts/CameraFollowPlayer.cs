using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float smoothSpeed = 0.125f;
    public float zoomSpeed = 0.05f;
    public float targetOrthographicSize = 1.6f;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Camera camera = GetComponent<Camera>();
            if (camera != null)
            {
                camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetOrthographicSize, zoomSpeed);
            }
        }
    }
}
