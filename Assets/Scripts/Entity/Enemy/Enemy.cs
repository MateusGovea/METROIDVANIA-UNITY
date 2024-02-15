using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;

    [Header("Combat")]
    public float battleTime;
    public float agroDistance;

    [Header("Stun")]
    public float stunDuration;
    public Vector2 stunDirection;

    [Header("Detection Info")]
    public float detectionRange;
    public float BehindDetectionRange;
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    #region Collision

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, detectionRange, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

    #endregion
}
