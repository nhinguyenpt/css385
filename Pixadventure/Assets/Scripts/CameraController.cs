using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [Range(1, 10)]
    [SerializeField] public float smoothFactor;
    [SerializeField] public Vector2 offset2D;
    [SerializeField] public Vector3 minValues, maxValue;

    private Vector3 _playerPosition;

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
        _playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        if (player.transform.localScale.x > 0f)
        {
            _playerPosition = new Vector3(_playerPosition.x + offset2D.x, _playerPosition.y + offset2D.y, _playerPosition.z);
        }
        else
        {
            _playerPosition = new Vector3(_playerPosition.x - offset2D.x, _playerPosition.y + offset2D.y, _playerPosition.z);
        }

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(_playerPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(_playerPosition.y, minValues.y, maxValue.y),
            _playerPosition.z);

        transform.position = Vector3.Lerp(transform.position, boundPosition, offsetSmoothing * Time.deltaTime);
    }
}
