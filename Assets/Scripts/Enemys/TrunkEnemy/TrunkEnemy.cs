using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrunkEnemy : AI_Enemy
{
    [SerializeField] private List<Vector3> pointMove;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Sprite srBullet;
    [SerializeField] private Transform posSpawn;
    public float fadeTime;
    public bool canRun = true;
    private float timeElapsed = 0f;
    private GameObject body;
    private Vector3 target;
    private int targetIndex;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitDamage = GetComponent<JumpDamage>();
        target = pointMove[0];
        if (bullet != null)
        {
            bullet.GetComponent<SpriteRenderer>().sprite = srBullet;   
        }
        body = transform.GetChild(1).gameObject;
        CanRun = canRun;
    }

    private bool checkDead = false;
    public override void Running()
    {
        if (!canRun)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (Dead())
            {
                if (!checkDead)
                {
                    rb.velocity = new Vector2(Random.Range(-1f, 1f), 4f);
                    body.GetComponent<BoxCollider2D>().enabled = false;
                    transform.GetComponent<BoxCollider2D>().enabled = false;
                    checkDead = true;
                }
                else
                {
                    timeElapsed += Time.deltaTime;
                    if (timeElapsed > fadeTime)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
        else
        {
            if (Vector2.Distance(rb.position, target) > 0.5f && CanRun && !Dead())
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else if(Vector2.Distance(rb.position, target) < 0.5f && CanRun && !Dead())
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                StartCoroutine(DelayFlip());
            }else if (!CanRun && !Dead())
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                if (!checkDead)
                {
                    rb.velocity = new Vector2(Random.Range(-1f, 1f), 4f);
                    body.GetComponent<BoxCollider2D>().enabled = false;
                    transform.GetComponent<BoxCollider2D>().enabled = false;
                    checkDead = true;
                }
                else
                {
                    timeElapsed += Time.deltaTime;
                    if (timeElapsed > fadeTime)
                    {
                        Destroy(gameObject);
                    }
                }
            }   
        }
    }
    
   private bool hitPlayer = false;
   private bool isBlock = false;
   private bool isAttacking = false;
   public override void Attack()
   {
       Vector2 current = transform.position;
       RaycastHit2D[] ray2D;

       if (speed > 0)
       {
           ray2D = Physics2D.RaycastAll(current, Vector2.right, arrangeAttack);
           Debug.DrawRay(current, Vector2.right * arrangeAttack, Color.red);
       }
       else
       {
           ray2D = Physics2D.RaycastAll(current, Vector2.left, arrangeAttack);
           Debug.DrawRay(current, Vector2.left * arrangeAttack, Color.red);
       }

       hitPlayer = false;
       Vector2 player = default;

       foreach (var item in ray2D)
       {
           if (item.collider.gameObject.CompareTag("Player"))
           {
               hitPlayer = true;
               player = item.point;
           }

           if (item.collider.gameObject.CompareTag("Ground") && player != null)
           {
               Vector2 hitPoint = item.point;
               if (IsBetween(player, hitPoint, transform.position))
               {
                   isBlock = true;
                   break;
               }
               else
               {
                   isBlock = false;
                   break;
               }
           }
       }

       // bat dau tan cong
       if (!isAttacking && hitPlayer && !isBlock)
       {
           anim.SetBool("Attack", true);
           isAttacking = true;
           CanRun = false;
           
       }
       // kiem tra va dung tan cong
       else if (isAttacking && (!hitPlayer || isBlock))
       {
           StartCoroutine(StopAttack());
       }
   }
   private IEnumerator StopAttack()
   {
       yield return new WaitForSeconds(0.5f); // Thời gian delay
       anim.SetBool("Attack", false);
       isAttacking = false; // rest trang thai 
       hitPlayer = false;
       CanRun = true;
   }

   private bool IsBetween(Vector2 pos1, Vector2 pos2, Vector2 pos3)
   {
       return (pos2.x > pos1.x && pos2.x < pos3.x) || (pos2.x < pos1.x && pos2.x > pos3.x);
   }

   [SerializeField] private float speedBullet = -5f;
    public void SpawnBullet()
    {
        GameObject newBullet = PoolingManager.Instance.Spawn(bullet, posSpawn.position, quaternion.identity);
        if (speed > 0)
        {
            newBullet.GetComponent<Bullet>().speedShoot = -speedBullet;
            newBullet.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            newBullet.GetComponent<Bullet>().speedShoot = speedBullet;
            newBullet.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void UpdateTarget()
    {
        targetIndex = (targetIndex + 1) % pointMove.Count;
        target = pointMove[targetIndex];
    }

    private IEnumerator DelayFlip()
    {
        yield return new WaitForSeconds(1f);
        if (Vector2.Distance(rb.position, target) <= 0.5f)
        {
            if (!isAttacking)
            {
                UpdateTarget();
                CanRun = true;
                SetFlip(true);
                Flip();   
            }
        }
    }

}
