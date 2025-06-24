using System.Collections;
using UnityEngine;

public class BaseEnemies : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    private Collider2D Coll { get; set; }

    [Header("Stats")]
    public int faceDirection = -1;
    public int maxHealth = 3;
    public float moveSpeed = 2f;
    public float patrolRange = 3f;
    public float detectRange = 5f;
    public float destroyAfter = 1f;

    [Header("Detection & Attack")]
    public LayerMask groundMask;
    public LayerMask playerMask;
    protected float AttackTimer = 0f;
    protected int CurrentHealth;

    protected Vector2 StartPos;
    protected Transform Target;
    protected int chaseDirection = 0;
    protected Coroutine _loseTargetCoroutine;

    public enum State { Patrol, Chasing, Dead }
    protected State CurrentState = State.Patrol;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        CurrentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        StartPos = transform.position;
    }

    protected virtual void Update()
    {
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        if (CurrentState == State.Dead) return;
        HandleState();
    }

    private void HandleState()
    {
        switch (CurrentState)
        {
            case State.Patrol:
                Anim.SetBool("Attack", false);
                Anim.SetBool("Patrol", true);
                Patrol();
                if (_loseTargetCoroutine != null)
                {
                    StopCoroutine(_loseTargetCoroutine);
                    _loseTargetCoroutine = null;
                }
                break;

            case State.Chasing:
                Anim.SetBool("Patrol", false);
                Attack();
                break;
        }
    }

    protected void Flip()
    {
        faceDirection *= -1;
        Rb.transform.Rotate(0f, 180f, 0f);
    }

    protected void CheckIfShouldFlip()
    {
        if ((faceDirection == -1 && transform.position.x <= StartPos.x - patrolRange) ||
            (faceDirection == 1 && transform.position.x >= StartPos.x + patrolRange))
        {
            Flip();
        }

        if (IsWallAhead())
        {
            Flip();
        }
    }

    protected bool IsWallAhead()
    {
        Vector2 wallCheckOrigin = transform.position + Vector3.right * faceDirection * 0.5f;
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheckOrigin, Vector2.right * faceDirection, 0.3f, groundMask);
        return wallHit.collider != null;
    }

    protected virtual void Patrol()
    {
        Rb.velocity = new Vector2(faceDirection * moveSpeed, Rb.velocity.y);
        CheckIfShouldFlip();
    }

    protected virtual void Attack()
    {
        Anim.SetBool("Attack", true);

        float chaseDistance = Mathf.Abs(transform.position.x - StartPos.x);
        if (chaseDistance >= patrolRange)
        {
            CurrentState = State.Patrol;
            return;
        }

        if (chaseDirection != 0)
        {
            Rb.velocity = new Vector2(chaseDirection * moveSpeed, Rb.velocity.y);
            if (faceDirection != chaseDirection)
                Flip();
        }

        if (IsWallAhead())
        {
            CurrentState = State.Patrol;
        }
    }

    protected virtual void DetectPlayer()
    {
        AttackTimer -= Time.deltaTime;

        Vector2 origin = transform.position;
        Vector2 direction = faceDirection == -1 ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, detectRange, playerMask);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            chaseDirection = faceDirection;
            if (faceDirection != chaseDirection)
                Flip();

            CurrentState = State.Chasing;

            if (_loseTargetCoroutine != null)
            {
                StopCoroutine(_loseTargetCoroutine);
                _loseTargetCoroutine = null;
            }
        }
        else if (CurrentState == State.Chasing && _loseTargetCoroutine == null)
        {
            _loseTargetCoroutine = StartCoroutine(DelayToReturnToPatrol());
        }

        Debug.DrawRay(origin, direction * detectRange, Color.red);
    }

    protected IEnumerator DelayToReturnToPatrol()
    {
        yield return new WaitForSeconds(2f);
        CurrentState = State.Patrol;
        _loseTargetCoroutine = null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CurrentState == State.Dead) return;

        if (other.gameObject.CompareTag("Player") && AttackTimer <= 0f)
        {
            AttackEffect(other);
        }
    }

    protected virtual void AttackEffect(Collision2D other)
    {
        // Viết logic cắn Player ở đây nếu cần
        Debug.Log("Bit player");
    }

    public virtual void Dead()
    {
        if (CurrentState == State.Dead) return;

        CurrentState = State.Dead;
        Coll.enabled = false;
        Rb.velocity = Vector2.zero;
        Anim.SetTrigger("Dead");

        Destroy(gameObject, destroyAfter);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, maxHealth);
        Debug.Log("Enemy Health: " + CurrentHealth);
        if (CurrentHealth == 0)
            Dead();
    }

    
}
