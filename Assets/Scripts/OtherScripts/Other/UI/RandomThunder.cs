using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomThunder : MonoBehaviour
{
    [SerializeField] private GameObject allthunder; 
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 5f;
    [SerializeField] private float thunderDuration = 0.2f;

    private List<GameObject> thunders = new List<GameObject>();

    private void OnEnable()
    {
        foreach (Transform child in allthunder.transform)
        {
            child.gameObject.SetActive(false);
            thunders.Add(child.gameObject);
        }

        StartCoroutine(ThunderRoutine());
    }

    private IEnumerator ThunderRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            int randomIndex = Random.Range(0, thunders.Count);
            GameObject thunder = thunders[randomIndex];

            thunder.SetActive(true);
            yield return new WaitForSeconds(thunderDuration);
            thunder.SetActive(false);
        }
    }
}
