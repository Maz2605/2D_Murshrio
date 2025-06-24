using UnityEngine;

public class MoveShip : MonoBehaviour
{
    [SerializeField] private Animator sailAnimator;
    
    public bool isMove = false;
    public Transform ship;
    private Transform playerTransform;
    public float speed;
    
    // Update is called once per frame
    void Update()
    {
        if (isMove) Move();
    }

    private void Move()
    {
        float inputVertical = Input.GetAxisRaw("Vertical");
        if (inputVertical != 0)
        {
            if (transform.position.y > -1.5f)
            {
                transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
            }
            else if (transform.position.y < -10f)
            {
                transform.position = new Vector3(transform.position.x, -10f, transform.position.z);
            } 
            transform.Translate(new Vector2(0f, 5f * inputVertical * Time.deltaTime));   
        }
        transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isMove = true;
            playerTransform = other.gameObject.transform;
            other.gameObject.GetComponent<Player_Controller>().enabled = false;
            playerTransform.SetParent(ship);
            sailAnimator.SetBool("Wind",true);
            sailAnimator.Play("Tranfer to Wind");
        }
    }

     private void OnCollisionExit2D(Collision2D other)
     {
         if (other.gameObject.CompareTag("Player"))
         {
             isMove = false;
             playerTransform.GetComponent<PlayerRespawn>().PointCheckPoint(transform.position.x,transform.position.y+0.5f);
             playerTransform = null;
             sailAnimator.SetBool("Wind",false);
         }
     }
}