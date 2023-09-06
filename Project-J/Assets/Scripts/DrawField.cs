using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class DrawField : PoolObject
{
    [SerializeField]
    LineRenderer _lineRenderer;
    [SerializeField]
    EdgeCollider2D _edgeCollider;

    [SerializeField]
    int _maxDrawCount = 5;
    [SerializeField]
    float _selfDespawnTime = 5f;

    List<Vector2> points = new List<Vector2>();

    static Queue<DrawField> drawFields = new Queue<DrawField>();

    public override void OnSpawn()
    {
        base.OnSpawn();
        ResetPoints();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void Despawn<T>()
    {
        StopAllCoroutines();
        if (drawFields.Peek() == this)
        {
            drawFields.Dequeue();
        }
        base.Despawn<T>();
    }

    public void DrawOnScreen(in Vector3 point)
    {
        points.Add(point);
        SetRenderPoints(point);
    }

    public void CreateField()
    {
        SetColliderPoints();
        drawFields.Enqueue(this);
        if (drawFields.Count > _maxDrawCount)
        {
            drawFields.Dequeue().Despawn<DrawField>();
        }
        StartCoroutine(DespawnTimer());
    }

    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(_selfDespawnTime);
        Despawn<DrawField>();
    }

    private void ResetPoints()
    {
        _edgeCollider.enabled = false;
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = 0;
        points.Clear();
    }

    private void SetRenderPoints(in Vector3 point)
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, point);
    }

    private void SetColliderPoints()
    {
        _edgeCollider.enabled = true;
        _edgeCollider.SetPoints(points);
    }
}
