using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public GameObject player;
    public float offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 v3offset;
    public Vector3 minValues, maxValue;

    private Vector3 playerPosition;

    private void Awake()
    {
        syncCamera(0f);
    }

    // Update is called once per frame
    void Update()
    {
        syncCamera(smoothFactor);
    }

    private void syncCamera(float offsetSmoothing)
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        if (player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(playerPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(playerPosition.y, minValues.y, maxValue.y),
            playerPosition.z);

        transform.position = Vector3.Lerp(transform.position, boundPosition, offsetSmoothing * Time.deltaTime);
    }
}
