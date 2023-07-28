using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject playerCharacter;
    public CameraMovement caracterObj;

    public void Respawn()
    {
        if (Vector3.Distance(transform.position, spawnPoint.transform.position) < 30)
        {
            GameObject newPlayer = Instantiate(gameObject, spawnPoint.transform.position, Quaternion.identity);
            caracterObj.playerPos = newPlayer.transform;
            //newPlayer.GetComponentInChildren<Animator>().gameObject.transform.position = new Vector3(0, 0, 0.6f);
            newPlayer.GetComponent<PlayerController>().enabled = true;
            newPlayer.transform.parent = null;

            Destroy(gameObject);
            //Component[] allComponents = gameObject.GetComponentsInChildren(typeof(MonoBehaviour));
            //foreach (Component gameObjectComponent in allComponents)
            //    Destroy((MonoBehaviour)gameObjectComponent);

            //caracterObj.playerPos = gameObject.transform;
            //gameObject.GetComponent<PlayerController>().enabled = true;
        }
    }
}
