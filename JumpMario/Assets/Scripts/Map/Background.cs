using Runningboy.Manager;
using Runningboy.Data;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Map
{
    /*
     * ��ũ��Ʈ ���� ������ ī�޶� ��ũ��Ʈ ���ķ� ��������.
     * �ۼ��� ������ ��.
     */
    public class Background : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer _spriteRenderer;
        [SerializeField]
        BackgroundData _backgroundData;

        Camera _camera;

        private void Start()
        {
            _camera = GameManager.instance.IOModule.mainCamera;
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, 0);
        }

        public void SetBackground(byte sectorNumber)
        {
            if (_backgroundData.TryGetValue(sectorNumber, out Sprite sprite))
            {
                _spriteRenderer.sprite = sprite;

                float screenRatio = (float)Screen.width / Screen.height;
                float backgroundRatio = sprite.bounds.size.x / sprite.bounds.size.y;

                if (screenRatio > backgroundRatio) // ���ο� ���߱�
                {
                    float scale = _camera.orthographicSize * 2 * screenRatio / sprite.bounds.size.x;
                    transform.localScale = new Vector2(scale, scale);
                }
                else // ���ο� ���߱�
                {
                    float scale = _camera.orthographicSize * 2 / sprite.bounds.size.y;
                    transform.localScale = new Vector2(scale, scale);
                }

                return;
            }

            Debug.LogError($"Sector {sectorNumber} background sprite not found.");;
        }

        public void SetBackground(byte sectorNumberFrom, byte sectorNumberTo)
        {
            // TODO: ���� �̵� �̺�Ʈ, ���̴� �׷��� ��� ���
        }
    }
}