using Runningboy.Collection;
using Runningboy.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Section : MonoBehaviour
{
    public PolygonCollider2D polygonCollider2D;

    [SerializeField]
    GameObject _tileMapGrid;
    [SerializeField]
    GameObject _checkPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                GameManager.instance.RegistNextSection(this);
                Debug.Log($"{this} Enter");
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                GameManager.instance.ChangeSection(this);
                Debug.Log($"{this} Exit");
                break;
            default:
                break;
        }
    }
}
