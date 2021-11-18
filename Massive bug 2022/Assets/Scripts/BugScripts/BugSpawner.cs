using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public static BugSpawner instance;

    [HideInInspector]
    public List<Transform> bugTrans = new List<Transform>();

    [SerializeField] GameObject bugPrefab;
    [SerializeField] float spawnCooldown;
    [SerializeField] float spawnRadius;

    private float cooldownCounter;



    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownCounter <= 0)
        {
            Instantiate(bugPrefab, RandomSpawnPosition(), Quaternion.identity);
            cooldownCounter = spawnCooldown;
        }
        else cooldownCounter -= Time.deltaTime;
    }

    private Vector3 RandomSpawnPosition()
    {
        Vector2 spawnDir = Caculator.NormalizedDirection2D(Random.Range(0, 360));

        return spawnDir * spawnRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Vector3.zero, spawnRadius);
    }

    public void AddBugTrans(Transform bug)
    {
        bugTrans.Add(bug);
    }
    public void RemoveBugTrans(Transform bug)
    {
        bugTrans.Remove(bug);
    }
}
