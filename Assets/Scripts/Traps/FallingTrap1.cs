using System.Collections;
using UnityEngine;

public class FallingTrap1 : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool checkPlayer = false;
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (checkPlayer)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5f);
        }
        if (Vector3.Distance(originalPosition, transform.position) > 20f)
        {
            StartCoroutine(MoveBackToOriginalPosition());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!checkPlayer && anim.GetBool("On")){anim.SetBool("On", false);}
            StartCoroutine(PlayerCheck());
        }
    }

    IEnumerator PlayerCheck()
    {
        yield return new WaitForSeconds(0.5f);
        checkPlayer = true;
    }

    IEnumerator MoveBackToOriginalPosition()
    {
        checkPlayer = false;
        anim.SetBool("On", true);
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, Time.deltaTime * 3f);
            yield return null;
        }
        transform.position = originalPosition;
    }
}