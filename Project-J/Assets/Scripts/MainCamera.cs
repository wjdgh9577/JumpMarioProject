using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    [SerializeField]
    Camera _camera;
    [SerializeField]
    Transform _focusTM;

    public Camera Camera { get { return _camera; } }
    public Transform Focus { get { return _focusTM; } }
}
