using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputLayer : UIView, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    GameObject _drawFieldPrefab;

    Vector2 startScreenPosition;
    Vector2 currentScreenPosition;

    DrawField _drawField;
    MainCamera _mainCamera;

    public static event Action onBeginDrag;
    public static event Action<Vector2, Vector2> onDrag;
    public static event Action<Vector2, Vector2> onEndDrag;

    private void Start()
    {
        _mainCamera = Camera.main.GetComponent<MainCamera>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startScreenPosition = eventData.pointerCurrentRaycast.screenPosition;
        _drawField = ObjectPoolManager.instance.Spawn<DrawField>(_drawFieldPrefab, _mainCamera.Focus);
        _drawField.transform.localPosition = Vector3.zero;
        ScreenToLocal(startScreenPosition);
        onBeginDrag?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentScreenPosition = eventData.pointerCurrentRaycast.screenPosition;
        ScreenToLocal(currentScreenPosition);
        onDrag?.Invoke(startScreenPosition, currentScreenPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _drawField.transform.SetParent(null);
        _drawField.CreateField();
        onEndDrag?.Invoke(startScreenPosition, currentScreenPosition);
    }

    private void ScreenToLocal(in Vector2 screenPosition)
    {
        Vector3 worldPosition = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _mainCamera.Camera.focusDistance));
        Vector3 localPosition = _mainCamera.Focus.InverseTransformPoint(worldPosition);
        _drawField.DrawOnScreen(localPosition);
    }
}