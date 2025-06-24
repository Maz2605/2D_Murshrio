using UnityEngine;

public class NPCFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float followDistance = 0.5f;
    [SerializeField] private bool noFly = true; 

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector2 targetPos = player.position;
        Vector2 currentPos = transform.position;

        if (noFly)
        {
            targetPos.y = currentPos.y;
        }

        Vector2 direction = targetPos - currentPos;
        float distance = direction.magnitude;

        if (distance > followDistance)
        {
            Vector2 moveDir = direction.normalized;
            transform.position += (Vector3)(moveDir * moveSpeed * Time.deltaTime);

            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        if (direction.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (direction.x < -0.01f)
            spriteRenderer.flipX = true;
    }
}
