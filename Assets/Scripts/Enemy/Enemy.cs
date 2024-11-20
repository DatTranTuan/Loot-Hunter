using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    // stunned info
    public float stunDiraction;
    public Vector2 stunDirection;
    public bool CanBeStuned;
    //[SerializeField] protected GameObject counterImage;
    // Move info
    public float moveSpeed;
    public float idleTime;
    public float BattleTime;
    private float defaultMoveSpeed;


    // attack info
    [Header ("Attack distance")]
    public float attackDistance;
    // thoi gian hoi chieu
    public float attackCooldown;


    [HideInInspector] public float lastTimeAttacked;
    public EnemyStateMachine stateMachine { get; private set; }

    public string lastAnimBoolName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
     //   Debug.Log(IsPlayerDetected().collider.gameObject.name + "I SEE");
    }

    public virtual void AssignLastAnimName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }

    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }
    protected virtual IEnumerator FreezeTimerFor(float _seconds)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_seconds);
        FreezeTime(false);
    }
    #region Counter  Attack Window
    public virtual void OpenCounterAttackWindow()
    {
        CanBeStuned = true;
    //    counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        CanBeStuned = false;
     //   counterImage.SetActive(false);
    }
    public virtual bool CanbeStunned()
    {
        if (CanBeStuned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    #endregion

    // cap nhat trang thai tan cong
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    // 1 : vi trí , 2: khoảng cách , 3 Check cái gì
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);
    public virtual RaycastHit2D IsPlayerDetectedWM() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 20, whatIsPlayer);
    public virtual RaycastHit2D IsPlayerDetectedFL() => Physics2D.Raycast(wallCheck.position, Vector2.left * facingDir, 1, whatIsPlayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));

    }
}
