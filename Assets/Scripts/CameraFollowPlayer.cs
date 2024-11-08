using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject background;
    public float smoothSpeed = 0.125f;
    public float zoomSpeed = 0.05f;
    public float targetOrthographicSize = 1.6f;
    public float parallaxEffectMultiplier = 0.5f;

    private Vector2 backgroundStartPosition;

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
        if (background == null)
        {
            GameObject backgroundObj = GameObject.Find("BGL1");
            if (backgroundObj != null)
            {
                background = backgroundObj;
                backgroundStartPosition = background.transform.position;
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
                float desiredOrthographicSize = Mathf.Lerp(camera.orthographicSize, targetOrthographicSize, zoomSpeed);
                camera.orthographicSize = desiredOrthographicSize;
            }

            if (background != null)
            {
                Vector3 backgroundDesiredPosition = new Vector3(player.position.x * parallaxEffectMultiplier, player.position.y * parallaxEffectMultiplier, background.transform.position.z);
                Vector3 backgroundSmoothedPosition = Vector3.Lerp(background.transform.position, backgroundDesiredPosition, smoothSpeed);
                background.transform.position = backgroundSmoothedPosition;
            }
        }
    }
}
