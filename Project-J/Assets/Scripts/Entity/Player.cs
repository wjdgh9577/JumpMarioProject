using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class Player : Entity
{
    [Header("Components")]
    [SerializeField]
    LineRenderer _arrow;

    [Header("Control Values")]
    [SerializeField]
    float _minRange = 0.1f;
    [SerializeField]
    float _maxRange = 5f;
    [SerializeField]
    float _forceRatio = 100;
    [SerializeField]
    float _superJumpAllowable = 1f;

    protected override void Reset()
    {
        base.Reset();
        _arrow = GetComponent<LineRenderer>();

        _status = EntityStatus.Idle;
        _minRange = 0.1f;
        _maxRange = 5f;
        _forceRatio = 100;
        _superJumpAllowable = 1f;
    }

    private void Start()
    {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.1f)
                    goto _loop;
            }
            return;
        _loop:
            SetStatus(EntityStatus.Idle);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if ((_status & CanJump) != 0)
            {
                SetStatus(EntityStatus.Jump);
            }
        }
    }

    #region Drag Callbacks

    public class DragCallbackArgs : EventArgs
    {
        public Vector2 startScreenPosition;
        public Vector2 currentScreenPosition;
        public bool reverse;
    }

    private void OnBeginDrag()
    {
        if ((_status & EntityStatus.Idle) != 0)
        {
            SetStatus(EntityStatus.Crouch);
        }
    }

    private void OnDrag(Vector2 start, Vector2 current)
    {
        if ((_status & EntityStatus.Idle) != 0)
        {
            SetStatus(EntityStatus.Crouch);
        }

        RenderArrow(current - start);
    }

    private void OnEndDrag(Vector2 start, Vector2 current)
    {
        _arrow.enabled = false;

        if ((_status & CannotJump) != 0)
            return;

        Vector2 newVector = current - start;
        Vector2 normalized = newVector.normalized;
        float range = MathF.Sqrt(newVector.sqrMagnitude);

        switch (_status)
        {
            case EntityStatus.Idle:
            case EntityStatus.Crouch:
                if (range >= _minRange) // Jump or Slide
                {
                    range = Mathf.Min(range, _maxRange);

                    SetVelocity(normalized, range * _forceRatio);
                    SetStatus(EntityStatus.Idle);
                }
                else // Cancel
                {
                    SetStatus(EntityStatus.Idle);
                }
                break;
            case EntityStatus.Jump:
                if (range >= _minRange/* && Mathf.Abs(_rigidbody.velocity.y) <= _superJumpAllowable*/) // Super Jump
                {
                    range = Mathf.Min(range, _maxRange);

                    SetVelocity(normalized, range * _forceRatio);
                    SetStatus(EntityStatus.SuperJump);
                }
                break;
            default:
                break;
        }
    }

    #endregion

    private void RenderArrow(Vector3 vector)
    {
        _arrow.enabled = true;

        float range = Mathf.Clamp(Mathf.Sqrt(vector.sqrMagnitude), 0, _maxRange);
        Vector3 normalized = vector.normalized;

        _arrow.endColor = range >= _minRange ? Color.green : Color.red;

        _arrow.SetPosition(1, normalized * range);
    }
}