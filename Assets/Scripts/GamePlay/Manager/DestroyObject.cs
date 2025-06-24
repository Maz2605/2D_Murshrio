using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public string nameObject = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(nameObject))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.coin);
            gameObject.SetActive(false);
        }
    }
}
