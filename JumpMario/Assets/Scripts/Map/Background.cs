using Runningboy.Manager;
using Runningboy.Data;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Map
{
    /*
     * 스크립트 실행 순서를 카메라 스크립트 이후로 조절했음.
     * 작성시 참고할 것.
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

                if (screenRatio > backgroundRatio) // 가로에 맞추기
                {
                    float scale = _camera.orthographicSize * 2 * screenRatio / sprite.bounds.size.x;
                    transform.localScale = new Vector2(scale, scale);
                }
                else // 세로에 맞추기
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
            // TODO: 섹터 이동 이벤트, 쉐이더 그래프 사용 고려
        }
    }
}