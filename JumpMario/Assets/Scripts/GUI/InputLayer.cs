using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Runningboy.Manager;

namespace Runningboy.GUI
{
    public class InputLayer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        Camera mainCam;

        private void Start()
        {
            mainCam = Camera.main;
        }

        #region Drag

        Vector3 startPos;
        Vector3 currentPos;
        bool isDrag = false;

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDrag = true;
            startPos = ScreenToWorldPoint(eventData.pointerCurrentRaycast.screenPosition);
            GameManager.instance.OnBeginDrag(startPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            currentPos = ScreenToWorldPoint(eventData.pointerCurrentRaycast.screenPosition);
            GameManager.instance.OnDuringDrag(currentPos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDrag = false;
            GameManager.instance.OnEndDrag(startPos - currentPos);
        }

        private Vector3 ScreenToWorldPoint(in Vector2 screenPosition)
        {
            Vector3 screenPosition3 = new Vector3(screenPosition.x, screenPosition.y, 0);
            Vector3 returnPosition = mainCam.ScreenToWorldPoint(screenPosition3);

            return returnPosition;
        }

        private void OnDrawGizmos()
        {
            if (isDrag)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(startPos, currentPos);
            }
        }

        #endregion
    }
}