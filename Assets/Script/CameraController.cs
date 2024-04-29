using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float offsetx;
    [SerializeField] private float offsety;
    [SerializeField] private float offsetz;

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(offsetx, offsety, -offsetz);
    }
}
