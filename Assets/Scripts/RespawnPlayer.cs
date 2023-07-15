using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject spawnPoint;

    public void Respawn()
    {
        if (Vector3.Distance(spawnPoint.transform.position, gameObject.transform.position) < 30)
        {
            gameObject.transform.position = spawnPoint.transform.position;
        }
    }
}
