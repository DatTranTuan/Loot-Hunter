using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

public class BotControl_dattt : Singleton<BotControl_dattt>
{
    [SerializeField] private BotAnimation anim;

    private StateMachine stateMachine;

    [SerializeField] private float attackRange;
    [SerializeField] private float rangeAttackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private BotType botType;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EdgeCollider2D edgeCollider;

    [SerializeField] private BotAttackArea attackArea;
    [SerializeField] private GameObject rangeBulletPrefab;
    [SerializeField] private Transform rangeBulletContain;
    [SerializeField] private LaserBeam laserPrefab;
    [SerializeField] private Transform laserContain;

    private Transform startBPosition;

    private float currentHealth;
    private float raycastRange;

    private float damageTaken;

    public Transform startPosition;
    private bool isRight = true;
    private bool isTarget = false;
    private bool isTakeDmg = false;
    private bool isImune = false;
    private bool isDeath = false;

    private float flyHeight = 5f;
    private float flightDuration = 2f;
    private float loopRadius = 3f;
    private int loopSegments = 36;
    public float minX = 389f;
    public float maxX = 446f;

    private bool isFlying = false;

    public BotAnimation Anim { get => anim; set => anim = value; }
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
    public bool IsTarget { get => isTarget; set => isTarget = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsTakeDmg { get => isTakeDmg; set => isTakeDmg = value; }
    public float DamageTaken { get => damageTaken; set => damageTaken = value; }
    public BotType BotType { get => botType; set => botType = value; }
    public bool IsDeath { get => isDeath; set => isDeath = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public bool IsImune { get => isImune; set => isImune = value; }
    public Transform StartPosition { get => startPosition; set => startPosition = value; }
    public bool IsFlying { get => isFlying; set => isFlying = value; }

    private void Start()
    {
        StateInit();

        currentHealth = DataManager.Instance.GetBotData(BotType).maxHealth;
    }

    private void Update()
    {
        stateMachine.Update();

        IsTarget = CheckPlayer();

        if (isDeath)
        {
            isDeath = true;
            StopMoving();
            ChangeDeath();
        }
    }

    private void StateInit()
    {
        StateMachine = new StateMachine(this);
        S_Wait s_Idle = new S_Wait(this);
        StateMachine.AddState(s_Idle);
        s_Idle.Enter();
    }

    public void ChangePatrol()
    {
        if (stateMachine.GetState(typeof(S_Patrol)) == null)
        {
            stateMachine.AddState(new S_Patrol(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Patrol)));
    }

    public void ChangeIdle()
    {
        if (stateMachine.GetState(typeof(S_Wait)) == null)
        {
            stateMachine.AddState(new S_Wait(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Wait)));
    }

    public void ChangeAttack()
    {
        if (stateMachine.GetState(typeof(S_Attack)) == null)
        {
            stateMachine.AddState(new S_Attack(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Attack)));
    }

    public void ChangeRangeAttack()
    {
        if (stateMachine.GetState(typeof(S_RangeAttack)) == null)
        {
            stateMachine.AddState(new S_RangeAttack(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_RangeAttack)));
    }

    public void ChangeLaserAttack()
    {
        if (stateMachine.GetState(typeof(S_Laser)) == null)
        {
            stateMachine.AddState(new S_Laser(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Laser)));
    }

    public void ChangeShield()
    {
        if (stateMachine.GetState(typeof(S_Shield)) == null)
        {
            stateMachine.AddState(new S_Shield(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Shield)));
    }

    public void ChangeTakeHit()
    {
        if (!isDeath && !IsImune)
        {
            if (stateMachine.ActiveStates.Count > 0)
            {
                IStateNormal currentState = stateMachine.ActiveStates[stateMachine.ActiveStates.Count - 1];
                stateMachine.RemoveState(currentState);
            }

            if (stateMachine.GetState(typeof(S_TakeHit)) == null)
            {
                stateMachine.AddState(new S_TakeHit(this));
            }

            stateMachine.ChangeState(stateMachine.GetState(typeof(S_TakeHit)));
        }
    }

    public void ChangeGuard()
    {
        if (stateMachine.GetState(typeof(S_Guard)) == null)
        {
            stateMachine.AddState(new S_Guard(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Guard)));
    }

    public void ChangeTele()
    {
        if (stateMachine.GetState(typeof(S_Tele)) == null)
        {
            stateMachine.AddState(new S_Tele(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Tele)));
    }

    public void ChangeFly()
    {
        if (stateMachine.GetState(typeof(S_Fly)) == null)
        {
            stateMachine.AddState(new S_Fly(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Fly)));
    }

    public void ChangeDeath()
    {
        if (stateMachine.GetState(typeof(S_Death)) == null)
        {
            stateMachine.AddState(new S_Death(this));
        }

        stateMachine.ChangeState(stateMachine.GetState(typeof(S_Death)));
    }

    public void Moving()
    {
        Rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        Rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        //edgeCollider.enabled = true;

        ActiveAttack();
        Debug.Log("UnLeash Sword");
        //StartCoroutine(DelayActiveBox());
        //Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void RangeAttack()
    {
        GameObject bulletSpawn = Instantiate(rangeBulletPrefab, rangeBulletContain.transform.position, Quaternion.identity);
        bulletSpawn.transform.SetParent(rangeBulletContain);
        Destroy(bulletSpawn.gameObject, 2f);
    }

    public void LaserAttack()
    {
        LaserBeam laserSpawn = Instantiate(laserPrefab, laserContain.transform.position, Quaternion.identity);
        laserSpawn.transform.SetParent(laserContain);
    }

    public void ActiveLastBoss()
    {
        GameManager.Instance.NightBone.SetActive(false);
        GameManager.Instance.Cthulu.SetActive(true);
    }

    public bool IsTargetInAttackRange()
    {
        if (IsTarget && Vector2.Distance(PlayerControl.Instance.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsTargetInRangeAttackRange()
    {
        if (IsTarget && Vector2.Distance(PlayerControl.Instance.transform.position, transform.position) <= rangeAttackRange)
        {
            Debug.Log("Rangeeeeee");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    private void ActiveAttack()
    {
        //attackArea.PLAYER?.PlayerTakeDmg(DataManager.Instance.GetBotData(BotControl_dattt.Instance.BotType).dmgDeal);
        attackArea.BotDealDmg(DataManager.Instance.GetBotData(botType).handDmgDeal);
        //edgeCollider.enabled = true;
        //attackArea.SetActive(true);
        //indexAttack++;
        //Debug.LogError($"Time On: {Time.frameCount}, index {indexAttack}");
    }

    public void StartFly()
    {
        StartCoroutine(FlyAndReturn());
        SpawnCthuluBalls();
    }

    public void SpawnCthuluBalls()
    {
        for (int i = 0; i < 40; i++)
        {
            float randomX = Random.Range(minX, maxX);

            Vector3 spawnPosition = new Vector3(randomX, -105, 0);

            GameObject spawnCthuBall = Instantiate(rangeBulletPrefab, spawnPosition, Quaternion.Euler(0, 0, 90));
        }
    }

    private IEnumerator FlyAndReturn()
    {
        if (IsFlying) yield break;
        IsFlying = true;

        Vector3 originalPosition = transform.position;

        Vector3 upPosition = originalPosition + Vector3.up * flyHeight;
        yield return MoveToPosition(upPosition, flightDuration);

        Vector3[] loopPath = GenerateLoopPath(upPosition, loopRadius, loopSegments);
        foreach (Vector3 targetPoint in loopPath)
        {
            yield return MoveToPosition(targetPoint, flightDuration / loopSegments);
        }

        yield return MoveToPosition(originalPosition, flightDuration);

        IsFlying = false;
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private Vector3[] GenerateLoopPath(Vector3 center, float radius, int segments)
    {
        Vector3[] path = new Vector3[segments];
        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            float x = center.x + Mathf.Cos(angle) * radius;
            float z = center.z + Mathf.Sin(angle) * radius;
            path[i] = new Vector3(x, center.y, z);
        }

        return path;
    }

    private void DeActiveAttack()
    {
        if (botType == BotType.NightBone)
        {
            ChangeIdle();
        }
        else
        {
            ChangePatrol();
        }
        //edgeCollider.enabled = false;

        //attackArea.SetActive(false);
        //Debug.LogError($"Time OFF: {Time.frameCount},index {indexAttack}");
    }

    private void DeActiveGuard()
    {
        isImune = false;
        StateMachine.Exit(StateMachine.GetState(typeof(S_Guard)));
        ChangeAttack();
    }

    private void DeActiveShield()
    {
        isImune = false;
        StateMachine.Exit(StateMachine.GetState(typeof(S_Shield)));
        ChangePatrol();
    }

    private void DeActiveTele()
    {
        StateMachine.Exit(StateMachine.GetState(typeof(S_Tele)));
        ChangeAttack();
    }

    public void ReSpawn()
    {
        if (gameObject.activeInHierarchy)
        {
            isDeath = false;
            currentHealth = DataManager.Instance.GetBotData(botType).maxHealth;
            if (stateMachine.ActiveStates != null && stateMachine.GetState(typeof(S_Death)) != null)
            {
                StateMachine.Exit(StateMachine.GetState(typeof(S_Death)));
            }

            stateMachine.ActiveStates.Clear();
            ChangeIdle();
        }
    }

    public bool CheckPlayer()
    {
        if (botType == BotType.GolemBoss || botType == BotType.NightBone || botType == BotType.DeathBringer)
        {
            raycastRange = 9f;
        }
        else
        {
            raycastRange = 3.5f;
        }

        Vector2 raycastStartPos = new Vector2(transform.position.x, transform.position.y + 1f);

        if (botType == BotType.FlyingEye)
        {
            raycastStartPos = new Vector2(transform.position.x, transform.position.y);
        }

        RaycastHit2D hitRight = Physics2D.Raycast(raycastStartPos, Vector2.right, raycastRange, playerLayer);
        Debug.DrawRay(raycastStartPos, Vector2.right * raycastRange, Color.green);

        RaycastHit2D hitLeft = Physics2D.Raycast(raycastStartPos, Vector2.left, raycastRange, playerLayer);
        Debug.DrawRay(raycastStartPos, Vector2.left * raycastRange, Color.red);

        return hitRight.collider != null || hitLeft.collider != null;
    }
}
