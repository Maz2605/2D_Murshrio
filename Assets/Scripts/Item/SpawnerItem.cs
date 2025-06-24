using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> poolItem;
    private bool check = false;

    private void Start()
    {
        check = false;
    }


    private void Update()
    {
        if (gameObject.GetComponent<JumpDamage>().lifes <= 0 && !check)
        {
            if (poolItem.Count > 1)
            {
                GameObject objItem = PoolingManager.Instance.Spawn(poolItem[Random.Range(0, poolItem.Count)], transform.position, quaternion.identity);
                objItem.SetActive(true);
                check = true;
            }
            else
            {
                GameObject objItem = PoolingManager.Instance.Spawn(poolItem[0], transform.position, quaternion.identity);
                objItem.SetActive(true);
                Debug.Log("OKOK");
                check = true;
            }
        }
    }
}
