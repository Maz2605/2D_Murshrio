using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform target;

    [SerializeField] private float shootRate;
    [SerializeField] private float bombSpeed;
    [SerializeField] private float maxY;

    [SerializeField] private float positionX;
    [SerializeField] private float positionY;
    

    public void SpawnBomb()
    {
        Vector3 bounus = new Vector3(positionX, positionY, 0f);
        Vector2 targetPosition = target.position + bounus;
        Bomb bomb = PoolingManager.Instance.Spawn(_bombPrefab, transform.position, quaternion.identity).GetComponent<Bomb>();
        bomb.Init(targetPosition, bombSpeed, maxY);
    }
}