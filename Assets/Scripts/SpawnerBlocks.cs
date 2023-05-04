using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBlocks : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Plane[] panelPrefabs;
    [SerializeField] private Plane firstPrefab;

    private List<Plane> spawnChunks = new List<Plane>();
        
    void Start()
    {
        spawnChunks.Add(firstPrefab);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (player.transform.position.z > spawnChunks[spawnChunks.Count - 1].end.position.z - 80)
        {
            SpawnChunk();
        }
    }
    public void SpawnChunk()
    {
        Plane newPlanes = Instantiate(panelPrefabs[Random.Range(0, panelPrefabs.Length)]);
        newPlanes.transform.position = spawnChunks[spawnChunks.Count - 1].end.position - newPlanes.begin.localPosition;
        spawnChunks.Add(newPlanes);
        Debug.Log(newPlanes.name);

        if (spawnChunks.Count > 8)
        {
            Destroy(spawnChunks[0].gameObject);
            spawnChunks.RemoveAt(0);
        }
    }
}
