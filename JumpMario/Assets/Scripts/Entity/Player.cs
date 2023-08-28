using System;
using UnityEngine;
using Runningboy.Collection;
using Runningboy.Manager;
using Sirenix.OdinInspector;
using System.Collections;

namespace Runningboy.Entity
{
    public class Player : Entity, IPlayable
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
            GameManager instance = GameManager.instance;
            instance.IOModule.onBeginDrag += OnBeginDrag;
            instance.IOModule.onDuringDrag += OnDuringDrag;
            instance.IOModule.onEndDrag += OnEndDrag;

            tag = "Player";
            correctCoroutine = CorrectCoroutine();
            Correct();
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

        private void OnBeginDrag(object sender, EventArgs callback)
        {
            if (callback is DragCallbackArgs args)
            {
                if ((_status & EntityStatus.Idle) != 0)
                {
                    SetStatus(EntityStatus.Crouch);
                }
            }
        }

        private void OnDuringDrag(object sender, EventArgs callback)
        {
            if (callback is DragCallbackArgs args)
            {
                if ((_status & EntityStatus.Idle) != 0)
                {
                    SetStatus(EntityStatus.Crouch);
                }

                Vector3 start = GameManager.instance.IOModule.ScreenToWorldPoint(args.startScreenPosition);
                Vector3 currnet = GameManager.instance.IOModule.ScreenToWorldPoint(args.currentScreenPosition);

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
                Vector3 start = GameManager.instance.IOModule.ScreenToWorldPoint(args.startScreenPosition);
                Vector3 currnet = GameManager.instance.IOModule.ScreenToWorldPoint(args.currentScreenPosition);

                Vector2 newVector = args.reverse ? currnet - start : start - currnet;
                Vector2 normalized = newVector.normalized;
                float range = MathF.Sqrt(newVector.sqrMagnitude);
                
                switch (_status)
                {
                    case EntityStatus.Idle:
                    case EntityStatus.Crouch:
                        if (range >= _minRange) // Jump or Slide
                        {
                            range = Mathf.Min(range, _maxRange);

                            AddForce(normalized, range * _forceRatio);
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

                            AddForce(normalized, range * _forceRatio);
                            SetStatus(EntityStatus.SuperJump);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Correction

        private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        private IEnumerator correctCoroutine;

        public void Correct()
        {
            StopCoroutine(correctCoroutine);
            StartCoroutine(correctCoroutine);
        }

        private IEnumerator CorrectCoroutine()
        {
            Vector2 beforeVelocity = _rigidbody.velocity;
            while (true)
            {
                yield return waitForFixedUpdate;
                Vector2 afterVelocity = _rigidbody.velocity;
                if (beforeVelocity == afterVelocity && (_status & CanJump) == 0)
                {
                    SetStatus(EntityStatus.Idle);
                }
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
}