using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _virtualCamera;

    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.Follow = FindObjectOfType<Player>().transform;
    }
}
