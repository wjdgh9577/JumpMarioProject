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
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.RegistNextSection(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.ChangeSection(this);
        }
    }

    public void SetActiveTileMap(bool active)
    {
        _tileMapGrid.SetActive(active);
    }
}
