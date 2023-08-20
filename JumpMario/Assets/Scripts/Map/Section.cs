using Runningboy.Collection;
using Runningboy.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Map
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Section : MonoBehaviour
    {
        [Header("Components")]
        public PolygonCollider2D polygonCollider2D;

        [Header("")]
        public int sectorNumber;
        public int sectionNumber;

        [SerializeField]
        GameObject _tileMapGrid;
        [SerializeField]
        GameObject _checkPoint;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                MapManager.instance.RegistNextSection(this);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                MapManager.instance.ChangeSection(this);
            }
        }

        public void SetActiveTileMap(bool active)
        {
            _tileMapGrid.SetActive(active);
        }
    }
}