using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    [SerializeField] PoolManager spherePool;

    private void Start()
    {
        //Instantiate
        for(int x = 0; x < 10; x++)
        {
            GameObject theThingSpawned = spherePool.Spawn();
            //when spawning, we need to reset the values of the gameobjesct
            theThingSpawned.transform.position = Vector3.zero;
        }

        StartCoroutine(despawnSpheres());
        StartCoroutine(spawnSpheres());
    }


    IEnumerator despawnSpheres()
    {
        while(spherePool.spawnResource.Count > 0)
        {
            yield return new WaitForSeconds(1f);

            spherePool.Despawn(spherePool.spawnResource[0]);
        }
    }

    IEnumerator spawnSpheres()
    {
        while (spherePool.spawnResource.Count > 0)
        {
            yield return new WaitForSeconds(2f);

            GameObject theThingISpawned = spherePool.Spawn();
            //when spawning, we need to reset the values of the gameobjesct
            theThingISpawned.transform.position = Vector3.zero;
        }
    }




}
