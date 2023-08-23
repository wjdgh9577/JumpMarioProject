using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Runningboy.Manager;
using Runningboy.Utility;

namespace Runningboy.GUI
{
    public class InputLayer : PanelBase, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private RectTransform _touchPin;
        [SerializeField]
        private RectTransform _touchScroll;

        #region Drag Event

        Vector2 startScreenPosition;
        Vector2 currentScreenPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            startScreenPosition = eventData.pointerCurrentRaycast.screenPosition;
            SetActivePin(true);
            SetPinObjectPosition(_touchPin, startScreenPosition);
            GameManager.instance.IOModule.OnBeginDrag(this, startScreenPosition, startScreenPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            currentScreenPosition = eventData.pointerCurrentRaycast.screenPosition;
            SetPinObjectPosition(_touchScroll, currentScreenPosition);
            GameManager.instance.IOModule.OnDuringDrag(this, startScreenPosition, currentScreenPosition);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetActivePin(false);
            GameManager.instance.IOModule.OnEndDrag(this, startScreenPosition, currentScreenPosition);
        }

        #endregion

        #region Pin UI

        private void SetActivePin(bool active)
        {
            _touchPin.gameObject.SetActive(active);
            _touchScroll.gameObject.SetActive(active);
        }

        private void SetPinObjectPosition(RectTransform obj, Vector3 position)
        {
            obj.position = position;
        }

        #endregion
    }
}