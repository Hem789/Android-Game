using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftActivator : MonoBehaviour
{
    private Lift lift;
    float counter=20;
    public AudioSource voice;
    void Start()
    {
        lift=GetComponent<Lift>();
        lift.enabled=false;
    } 
    void OnTriggerEnter(Collider a)
    {
        if(a.gameObject.tag=="Player" && FindObjectOfType<GameManager>().scoped==false && FindObjectOfType<GameManager>().outside==true)
        {
            lift.enabled=true;
            counter=20;
        }
    }
    void OnTriggerStay(Collider a)
    {
        if(a.gameObject.tag=="Player" && FindObjectOfType<GameManager>().scoped==false && FindObjectOfType<GameManager>().outside==true)
        {
            lift.enabled=true;
            counter=20;
        }
    }
    //void OnTriggerExit(Collider a)
    //{
      //  if(a.gameObject.tag=="Player")
        //{
        //    lift.enabled=false;
        //}
    //}
    void FixedUpdate()
    {
        if(counter<=0)
        {
            lift.enabled=false;
            voice.enabled=false;
        }
        else
        {
            counter-=Time.deltaTime;
        }

    }
}
