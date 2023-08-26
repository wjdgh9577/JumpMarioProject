using Runningboy.Collection;
using Runningboy.Manager;
using Runningboy.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Runningboy.Map
{
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

        List<Trigger> triggers;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            MapManager.instance.EnterSection(this);
        }

        public SectionData? Init()
        {
            gameObject.layer = LayerMask.NameToLayer("Sector");

            var data =  gameObject.name.Split('_');

            if (data.Length != 2 )
            {
                Debug.LogError($"Invalid section object name: {gameObject.name}");
                return null;
            }

            if (byte.TryParse(data[0], out byte sector))
            {
                sectionData.sectorNumber = sector;
            }
            else
            {
                Debug.LogError($"Invalid section object name: {gameObject.name}");
                return null;
            }
            if (byte.TryParse(data[1], out byte section))
            {
                sectionData.sectionNumber = section;
            }
            else
            {
                Debug.LogError($"Invalid section object name: {gameObject.name}");
                return null;
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

            if (triggers == null)
            {
                triggers = GetComponentsInChildren<Trigger>().ToList();
            }
            foreach (Trigger trigger in triggers)
            {
                trigger.gameObject.SetActive(true);

            }

            SetActiveSection(false);

            return sectionData;
        }

        public void SetActiveSection(bool active)
        {
            SetActiveTileMap(active);
            SetActiveMarker(active);
        }

        private void SetActiveTileMap(bool active)
        {
            _tileMapGrid.SetActive(active);
        }

        private void SetActiveMarker(bool active)
        {
            _marker.gameObject.SetActive(PlayerData.instance.isVisitedSection(sectionData));
            _marker.color = active ? Color.yellow : Color.white;
        }

        public void SetPlayerToCheckPoint(Transform tm)
        {
            tm.position = _checkPoint.transform.position;
        }
    }
}