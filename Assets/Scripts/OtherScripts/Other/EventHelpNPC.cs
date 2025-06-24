using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventHelpNPC : MonoBehaviour
{
    [SerializeField] private GameObject objectLong;
    [SerializeField] private GameObject particle;
    [SerializeField] private MonoBehaviour[] enemyBehaviours;
    [SerializeField] private Event1 eventDialogue;
    public NPCFollower NPCFollower;

    private IsEnemy[] aI_Enemies;
    private bool show = false;

    private void Awake()
    {
        aI_Enemies = enemyBehaviours
            .Select(e => e as IsEnemy)
            .Where(e => e != null)
            .ToArray();

        objectLong.SetActive(true);
    }

    private void Update()
    {
        if (!show && CheckDieAllEnemy())
        {
            show = true;
            objectLong.SetActive(false);
            particle.SetActive(true);
            eventDialogue.ShowEvent();
            NPCFollower.enabled = true;
        }
    }

    private bool CheckDieAllEnemy()
    {
        return aI_Enemies == null
            || aI_Enemies.Length == 0
            || aI_Enemies.All(enemy => enemy == null || enemy.Dead());
    }

}
