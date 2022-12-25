using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
   
public float a;
private float b;
void OnEnable()
{
    b=a;
}
void Awake()
{
    b=a;
}
    // Update is called once per frame
    void Update()
    {
        if(b<=0)
        {
        //    this.gameObject.SetActive(false);
        MF_AutoPool.Despawn(gameObject);
        }
        else
        {
            b-=Time.deltaTime;
        }
    }
}
