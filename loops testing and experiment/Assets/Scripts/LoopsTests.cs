using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopsTests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {        
        int highest = -1;        
        for(int randNumberCount = 0; randNumberCount < 10 ; randNumberCount++ )
        {            
            int randomNumber = Random.Range(0, 101);
            
            if (randomNumber > highest)
            {
                highest = randomNumber;
            }
        }
        Debug.Log(highest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
