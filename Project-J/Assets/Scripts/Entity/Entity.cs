using UnityEngine;
using Sirenix.OdinInspector;

public enum EntityStatus
{
    Idle = 1,
    Die = 2,
    Crouch = 4,
    Jump = 8,
    SuperJump = 16,
}

/// <summary>
/// 플레이어, 몬스터 공용
/// 움직임 제어
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField, ReadOnly]
    protected EntityStatus _status = EntityStatus.Idle;

    [Header("Base Components")]
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

    #region Status

    protected readonly EntityStatus CannotJump = EntityStatus.SuperJump | EntityStatus.Die;
    protected readonly EntityStatus CanJump = EntityStatus.Idle | EntityStatus.Crouch;

    protected void SetStatus(EntityStatus status)
    {
        _status = status;

        string trigger;
        switch (status)
        {
            case EntityStatus.Idle:
                trigger = "Idle";
                break;
            case EntityStatus.Crouch:
                trigger = "Crouch";
                break;
            case EntityStatus.Jump:
                trigger = "Jump";
                break;
            case EntityStatus.SuperJump:
                trigger = "SuperJump";
                break;
            case EntityStatus.Die:
                trigger = "Die";
                break;
            default:
                trigger = "Default";
                break;
        }

        //SetTrigger(trigger);
    }

    #endregion

    protected void SetVelocity(Vector2 dir, float force)
    {
        _spriteRenderer.flipX = dir.x < 0;
        _rigidbody.velocity = dir * Mathf.Sqrt(force);
    }
}