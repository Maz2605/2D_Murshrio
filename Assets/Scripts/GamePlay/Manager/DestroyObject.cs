using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public string nameObject = "Player";
    public bool isReceived;
    private void OnEnable()
    {
        isReceived = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(nameObject))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.coin);
            gameObject.SetActive(false);
            isReceived = true;
        }
    }
}
