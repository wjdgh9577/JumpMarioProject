using System;
using UnityEngine;
using Runningboy.Collection;
using Runningboy.Manager;
using Sirenix.OdinInspector;

namespace Runningboy.Entity
{
    public class Player : Entity, IPlayable
    {
        [Header("Components")]
        [SerializeField]
        LineRenderer _arrow;

        [Header("Control Values")]
        [SerializeField, ReadOnly]
        EntityStatus _status = EntityStatus.Idle;
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

        private void OnEnable()
        {
            GameManager instance = GameManager.instance;
            instance.onBeginDrag += OnBeginDrag;
            instance.onDuringDrag += OnDuringDrag;
            instance.onEndDrag += OnEndDrag;

            tag = "Player";
        }

        private void OnDisable()
        {
            GameManager instance = GameManager.instance;
            instance.onBeginDrag -= OnBeginDrag;
            instance.onDuringDrag -= OnDuringDrag;
            instance.onEndDrag -= OnEndDrag;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    foreach (var contact in  collision.contacts)
                    {
                        if (contact.normal.y > 0)
                            goto _loop;
                    }
                    break;
                _loop:
                    SetTrigger("Land");
                    break;
                default:
                    break;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    if ((_status & CanJump) != 0)
                    {
                        SetTrigger("Fall");
                    }
                    break;
                default:
                    break;
            }
        }

        #region Drag Callbacks

        private void OnBeginDrag(object sender, EventArgs callback)
        {
            if (callback is DragCallbackArgs args)
            {
                if ((_status & EntityStatus.Idle) != 0)
                {
                    SetTrigger("Crouch");
                }
            }
        }

        private void OnDuringDrag(object sender, EventArgs callback)
        {
            if (callback is DragCallbackArgs args)
            {
                if ((_status & EntityStatus.Idle) != 0)
                {
                    SetTrigger("Crouch");
                }

                Vector3 start = GUIManager.instance.ScreenToWorldPoint(args.startScreenPosition);
                Vector3 currnet = GUIManager.instance.ScreenToWorldPoint(args.currentScreenPosition);

                RenderArrow(args.reverse ? currnet - start : start - currnet);
            }
        }

        private void OnEndDrag(object sender, EventArgs callback)
        {
            _arrow.enabled = false;

            if ((_status & CannotJump) != 0)
                return;

            if (callback is DragCallbackArgs args)
            {
                Vector3 start = GUIManager.instance.ScreenToWorldPoint(args.startScreenPosition);
                Vector3 currnet = GUIManager.instance.ScreenToWorldPoint(args.currentScreenPosition);

                Vector2 newVector = args.reverse ? currnet - start : start - currnet;
                Vector2 normalized = newVector.normalized;
                float range = newVector.sqrMagnitude;
                
                switch (_status)
                {
                    case EntityStatus.Idle:
                    case EntityStatus.Crouch:
                        if (range >= _minRange)
                        {
                            if (normalized.y > 0)
                            {
                                Debug.Log("Jump");

                                range = Mathf.Clamp(newVector.sqrMagnitude, _minRange, _maxRange);

                                Jump(normalized, range * _forceRatio);
                            }
                            else
                            {
                                Debug.Log("Slide");

                                Slide(normalized, range * _forceRatio);
                            }
                        }
                        else
                        {
                            Debug.Log("Return to Idle");

                            SetTrigger("Cancel");
                        }
                        break;
                    case EntityStatus.Jump:
                        if (range >= _minRange && Mathf.Abs(_rigidbody.velocity.y) <= _superJumpAllowable)
                        {
                            Debug.Log("Super Jump");

                            range = Mathf.Clamp(newVector.sqrMagnitude, _minRange, _maxRange);

                            SuperJump(normalized, range * _forceRatio);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Control

        private readonly EntityStatus CannotJump = EntityStatus.SuperJump | EntityStatus.Die | EntityStatus.Fall;
        private readonly EntityStatus CanJump = EntityStatus.Idle | EntityStatus.Crouch;

        private void Jump(Vector2 dir, float force)
        {
            _spriteRenderer.flipX = dir.x < 0;
            _rigidbody.velocity = dir * Mathf.Sqrt(force);

            SetTrigger("Jump");
        }

        private void SuperJump(Vector2 dir, float force)
        {
            _spriteRenderer.flipX = dir.x < 0;
            _rigidbody.velocity = dir * Mathf.Sqrt(force);

            SetTrigger("SuperJump");
        }

        private void Slide(Vector2 dir, float force)
        {
            _spriteRenderer.flipX = dir.x < 0;
            _rigidbody.velocity = dir * Mathf.Sqrt(force);

            SetTrigger("Land");
        }

        #endregion

        private void RenderArrow(Vector3 vector)
        {
            _arrow.enabled = true;

            float range = Mathf.Clamp(vector.sqrMagnitude, 0, _maxRange);
            Vector3 normalized = vector.normalized;

            _arrow.endColor = range >= _minRange ? Color.green : Color.red;

            _arrow.SetPosition(1, normalized * range);
        }

        protected override void SetTrigger(string name)
        {
            base.SetTrigger(name);

            switch (name)
            {
                case "Land":
                case "Cancel":
                    _status = EntityStatus.Idle;
                    break;
                case "Crouch":
                    _status = EntityStatus.Crouch;
                    break;
                case "Fall":
                    _status = EntityStatus.Fall;
                    break;
                case "Jump":
                    _status = EntityStatus.Jump;
                    break;
                case "SuperJump":
                    _status = EntityStatus.SuperJump;
                    break;
                case "Die":
                    _status = EntityStatus.Die;
                    break;
                default:
                    break;
            }
        }
    }
}