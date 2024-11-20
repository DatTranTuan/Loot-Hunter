using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player :Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public bool isBusy { get; private set; }


    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Charge Fire")]
    public GameObject bulletFire;
    public Transform firePos;

    [Header("Dash info")]
    [SerializeField] private float dashCoolDown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    public float SwordReturnInpact;


    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; }
    public PlayerLightBallState lightBallState { get; private set; }
    public PlayerDeathState deathState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");

        moveState = new PlayerMoveState(this, stateMachine, "Move");

        airState = new PlayerAirState(this, stateMachine, "Jumping");

        dashState = new PlayerDashState(this, stateMachine, "Dash");

        jumpState = new PlayerJumpState(this, stateMachine, "Jumping");

        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        lightBallState = new PlayerLightBallState(this, stateMachine, "LightBall");
        deathState = new PlayerDeathState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTriger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.L) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
        StartCoroutine(LoadSceneAfterDelay(2f));
    }
    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }
}
