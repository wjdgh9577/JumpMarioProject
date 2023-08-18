using System;
using UnityEngine;
using Runningboy.Collection;
using Runningboy.Manager;
using Sirenix.OdinInspector;

namespace Runningboy.Entity
{
    public class Player : Entity, IPlayable
    {
        //[SerializeField]
        //GameObject _arrow;

        [Header("Control Values")]
        [SerializeField]
        float _minRange = 0.1f;
        [SerializeField]
        float _maxRange = 5f;
        [SerializeField]
        float _forceRatio = 100;
        [SerializeField, ReadOnly]
        EntityStatus _status = EntityStatus.Idle;

        protected override void Reset()
        {
            base.Reset();
            //_arrow = GetComponentInChildren<LineRenderer>().gameObject;

            _minRange = 0.1f;
            _maxRange = 5f;
            _forceRatio = 100;
            _status = EntityStatus.Idle;
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
                    _status = EntityStatus.Idle;
                    SetTrigger("Land");
                    break;
                default:
                    break;
            }
        }

        #region Drag Event

        public void OnBeginDrag(object sender, EventArgs callback)
        {
            if (callback is BeginDragCallbackArgs args)
            {
                SetTrigger("Crouch");
            }
        }

        public void OnDuringDrag(object sender, EventArgs callback)
        {
            if (callback is DuringDragCallbackArgs args)
            {
                
            }
        }

        public void OnEndDrag(object sender, EventArgs callback)
        {
            if (callback is EndDragCallbackArgs args)
            {
                Vector2 newVector = args.dir;
                Vector2 normalized = newVector.normalized;
                float range = newVector.sqrMagnitude;
                
                if (range >= _minRange)
                {
                    Debug.Log("Jump");
                    range = Mathf.Clamp(newVector.sqrMagnitude, _minRange, _maxRange);

                    Jump(normalized, range * _forceRatio);
                }
                else
                {
                    Debug.Log("Return to Idle");
                    SetTrigger("Cancel");
                }
            }
        }

        #endregion

        private void Jump(Vector2 dir, float force)
        {
            _spriteRenderer.flipX = dir.x < 0;
            _rigidbody.velocity = dir * Mathf.Sqrt(force);
            SetTrigger("Jump");
            _status = EntityStatus.Jump;
        }
    }
}