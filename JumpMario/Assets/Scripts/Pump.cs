using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pump : MonoBehaviour
{
    [SerializeField, Range(0, 2000)]
    int force = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var center = transform.position;
        var dir = (collision.transform.position - center) / Vector2.SqrMagnitude(collision.transform.position - center);
        collision.attachedRigidbody.velocity = dir * force;
        Debug.LogWarning($"collision: {collision.transform.position}, transform: {center}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning("Enter");
    }
}
