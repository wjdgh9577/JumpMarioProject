using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Runningboy.Map
{
    public enum TriggerBehaviour
    {
        FadeoutTileMap,
    }

    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        TriggerBehaviour _triggerBehaviour;
        [SerializeField]
        GameObject[] _targets;
        [SerializeField]
        bool _recycle = false;
        [SerializeField, ReadOnly]
        bool _used = false;

        private void OnEnable()
        {
            _used = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_used && collision.CompareTag("Player"))
            {
                if (!_recycle)
                    _used = true;

                TurnOnTrigger();
            }
        }

        private void TurnOnTrigger()
        {
            switch (_triggerBehaviour)
            {
                case TriggerBehaviour.FadeoutTileMap:
                    FadeOutTileMap();
                    break;
            }
        }

        Tilemap[] tilemaps = null;
        private void FadeOutTileMap()
        {
            StartCoroutine(FadeOutTileMapCoroutine());
        }

        private IEnumerator FadeOutTileMapCoroutine()
        {
            if (tilemaps == null)
            {
                tilemaps = new Tilemap[_targets.Length];
                for (int i = 0; i < _targets.Length; i++)
                {
                    tilemaps[i] = _targets[i].GetComponent<Tilemap>();
                }
            }

            while (true)
            {
                foreach (var tilemap in tilemaps)
                {
                    var color = tilemap.color;

                    if (color.a <= 0)
                        goto loop;
                    
                    color = new Color(color.r, color.g, color.b, Mathf.Clamp01(color.a - Time.deltaTime * 2));
                    tilemap.color = color;
                }
                yield return null;
            }
        loop:
            foreach (var tilemap in tilemaps)
            {
                var color = tilemap.color;
                color = new Color(color.r, color.g, color.b, 1);
                tilemap.color = color;
                tilemap.gameObject.SetActive(false);
            }
        }
    }
}