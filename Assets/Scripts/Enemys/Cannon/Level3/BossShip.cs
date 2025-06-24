using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BossShip : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject nextLevel;
    public bool isAdvent = false;
    public GameObject camera;
    public GameObject rock;
    
    

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.GetComponent<MoveShip>().isMove)
        {
            if (gameObject.GetComponent<JumpDamage>().lifes > 0)
            {
                AdventEnemy();   
            }
            else
            {
                nextLevel.SetActive(true);
                gameObject.GetComponent<JumpDamage>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                camera.GetComponent<CameraFollowPlayer>().enabled = false;
                rock.SetActive(false);
                return;
            }
        }
    }

    void AdventEnemy()
    {
        if (playerTransform.position.x >= 80f && !isAdvent)
        {
            isAdvent = true;
            Debug.Log(isAdvent);
        }
        
        if (isAdvent && transform.position.x - playerTransform.position.x <= 14.8f)
        {
            Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>(true);

            // Duyệt qua tất cả các Transform
            foreach (var childTransform in childTransforms)
            {
                // Bỏ qua đối tượng cha
                if (childTransform == gameObject.transform) continue;

                // Kích hoạt đối tượng con
                childTransform.gameObject.SetActive(true);
            }
            // Move the enemy forward
            transform.Translate(new Vector2(4f * Time.deltaTime, 0f));
        }
        else if (!isAdvent)
        {
            // Move the enemy forward if not in advent state
            transform.Translate(new Vector2(4f * Time.deltaTime, 0f));
        }
    }
}