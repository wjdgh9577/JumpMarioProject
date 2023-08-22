using Runningboy.Collection;
using Runningboy.Manager;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Map
{
    [Serializable]
    public struct SectionData
    {
        [HorizontalGroup("")]
        public int sectorNumber;
        [HorizontalGroup("")]
        public int sectionNumber;
    }

    public class Section : MonoBehaviour
    {
        [Header("Components")]
        public PolygonCollider2D polygonCollider2D;
        [SerializeField]
        SpriteRenderer _marker;
        [SerializeField]
        GameObject _tileMapGrid;
        [SerializeField]
        GameObject _checkPoint;

        [Header("Sector/Section Data"), HideLabel, ReadOnly]
        public SectionData sectionData;

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

        public void Init()
        {
            var data =  gameObject.name.Split('_');

            if (data.Length != 2 )
            {
                Debug.LogError($"Invalid section object name: {gameObject.name}");
                return;
            }

            if (int.TryParse(data[0], out int sector))
            {
                sectionData.sectorNumber = sector;
            }
            else
            {
                Debug.LogError($"Invalid section object name: {gameObject.name}");
                return;
            }
            if (int.TryParse(data[1], out int section))
            {
                sectionData.sectionNumber = section;
            }
            else
            {
                Debug.LogError($"Invalid section object name: {gameObject.name}");
                return;
            }

            Vector2 size = _marker.bounds.size;

            polygonCollider2D.pathCount = 0;
            polygonCollider2D.SetPath(0, new Vector2[]
            {
                new Vector2(-size.x / 2, -size.y / 2),
                new Vector2(-size.x / 2, size.y / 2),
                new Vector2(size.x / 2, size.y / 2),
                new Vector2(size.x / 2, -size.y / 2)
            });
            polygonCollider2D.offset = _marker.transform.localPosition;
        }

        public void SetActiveTileMap(bool active)
        {
            _tileMapGrid.SetActive(active);
        }
    }
}