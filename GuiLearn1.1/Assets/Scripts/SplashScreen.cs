using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public float disableTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > disableTime)
        {
            gameObject.SetActive(false);
        }
           
    }
}
