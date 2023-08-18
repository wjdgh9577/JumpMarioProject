using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runningboy.Entity
{
    /// <summary>
    /// 플레이어, 몬스터 공용
    /// 움직임 제어
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class Entity : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        protected Rigidbody2D _rigidbody;
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;
        [SerializeField]
        protected Animator _animator;

        protected virtual void Reset()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        #region Animator

        protected virtual void ResetAllTrigger()
        {
            var parameters = _animator.parameters;

            foreach (var parameter in parameters)
            {
                if (parameter.type != AnimatorControllerParameterType.Trigger)
                    continue;

                var name = parameter.name;
                _animator.ResetTrigger(name);
            }
        }

        protected virtual void SetTrigger(string name)
        {
            ResetAllTrigger();
            _animator.SetTrigger(name);
        }

        #endregion
    }
}