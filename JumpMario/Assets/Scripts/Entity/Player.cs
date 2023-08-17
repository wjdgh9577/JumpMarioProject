using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runningboy.Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Player : CustomBehaviour
    {
        [SerializeField]
        GameObject _arrow;
    }
}