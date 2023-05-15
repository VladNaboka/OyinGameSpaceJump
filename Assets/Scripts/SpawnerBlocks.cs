using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBlocks : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Plane[] panelPrefabs;
    [SerializeField] private Plane firstPrefab;
    [SerializeField] private float spawnDistance = 100;
    private List<Plane> spawnChunks = new List<Plane>();
    void Start()
    {
        spawnChunks.Add(firstPrefab);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (player.transform.position.z > spawnChunks[spawnChunks.Count - 1].end.position.z - spawnDistance)
        {
            SpawnChunk();
        }
    }
    public void SpawnChunk()
    {
        Plane newPlane = Instantiate(panelPrefabs[Random.Range(0, panelPrefabs.Length)]);
        newPlane.transform.position = spawnChunks[spawnChunks.Count - 1].end.position - newPlane.begin.localPosition;
        spawnChunks.Add(newPlane);

        if (spawnChunks.Count > 6)
        {
            Destroy(spawnChunks[0].gameObject);
            spawnChunks.RemoveAt(0);
        }
    }
}
