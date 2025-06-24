using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeReference] private Animator anim;
    private Vector2 target;
    private float moveSpeed;
    private float maxY;
    private float distanceToTargetToDestroyBomb = 1f;

    private Vector3 trajectoryStartPoint;
    private float totalDistance;
    private float timeToTarget;
    private float elapsedTime;
    private bool hasReachTarget = false;

    public AudioManager audio;

    private void Start()
    {
        anim = GetComponent<Animator>();
        trajectoryStartPoint = transform.position;
        totalDistance = Vector3.Distance(trajectoryStartPoint, target);
        timeToTarget = totalDistance / moveSpeed;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (audio == null)
        {
            audio = GameObject.Find("GamePlay").GetComponent<AudioManager>();
        }
        if (target != null && !hasReachTarget)
        {
            elapsedTime += Time.deltaTime;
            UpdateBombPosition();
        }
        if (Vector2.Distance(transform.position, target) < distanceToTargetToDestroyBomb)
        {
            hasReachTarget = true;
            anim.Play("Explotion");
        }
    }

    public void Init(Vector2 target, float moveSpeed, float maxY)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
        float xDistanceTarget = target.x - transform.position.x;
        this.maxY = Mathf.Abs(xDistanceTarget * maxY);
    }

    private void UpdateBombPosition()
    {
        float progress = elapsedTime / timeToTarget;
        Vector2 newPosition = Vector2.Lerp(trajectoryStartPoint, target, progress);

        // Apply parabolic height
        float height = maxY * (1 - Mathf.Pow((progress * 2 - 1), 2));
        newPosition.y += height;

        // Ensure newPosition is valid
        if (!float.IsNaN(newPosition.x) && !float.IsNaN(newPosition.y))
        {
            transform.position = newPosition;
        }
        else
        {
            Debug.LogError($"Invalid position calculated: newPosition = {newPosition}");
        }
    }

    public void MusicBomb()
    {
        audio.PlaySFX(audio.bomb);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
