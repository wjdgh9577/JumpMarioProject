using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Indicator : MonoBehaviour
{
    [SerializeField]
    LineRenderer _lineRenderer;

    Transform _playerTM;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _playerTM =  FindObjectOfType<Player>().transform;

        InputLayer.onBeginDrag += OnBeginDrag;
        InputLayer.onDrag += OnDrag;
        InputLayer.onEndDrag += OnEndDrag;
    }

    private void OnDestroy()
    {
        InputLayer.onBeginDrag -= OnBeginDrag;
        InputLayer.onDrag -= OnDrag;
        InputLayer.onEndDrag -= OnEndDrag;
    }

    private void LateUpdate()
    {
        transform.position = _playerTM.position;
    }

    private void OnBeginDrag()
    {
        _lineRenderer.enabled = true;
    }

    private void OnDrag(Vector2 start, Vector2 current)
    {
        Vector2 vector = current - start;
        float range = Mathf.Sqrt(vector.sqrMagnitude);
        Vector3 normalized = vector.normalized;

        _lineRenderer.SetPosition(1, normalized * range);
    }

    private void OnEndDrag(Vector2 start, Vector2 current)
    {
        _lineRenderer.enabled = false;
    }
}
