using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createEnemyHeli : MonoBehaviour
{
    private GameObject player;
    public GameObject heli,suru;
    [SerializeField]
    private float delay,x;
    private RaycastHit hit;
    public Survival m;
    // Start is called before the first frame update
    void Start()
    {
        if (delay!=null)
        x=delay;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
       // x=Random.Range(-50,-40);
        //z=Random.Range(-50,-40);
        player=GameObject.FindWithTag("Player");
        if(player.transform.position.y>5)
        {
        if(!Physics.Raycast(player.transform.position,new Vector3(0,-1,0),out hit,1))
        {
        if( delay<=0 && FindObjectOfType<GameManager>().outside==false)
        {
            //MF_AutoPool.Spawn(heli,new Vector3(player.transform.position.x,0,player.transform.position.z)+new Vector3(5*x,20,5*z),Quaternion.Euler(0,180,0));
            if(suru.transform.position.y>0)
            {
            MF_AutoPool.Spawn(heli,new Vector3(suru.transform.position.x,20,suru.transform.position.z),Quaternion.Euler(0,180,0));
            }
            /*else
            {
                suru.transform.parent=null;
                suru.gameObject.tag="3";
                Debug.Log(suru.transform.position);
            }*/
            if(x==0)
            {
            delay=10;
            }
            else
            {
                delay=x;

            }
            if(m!=null)
        {
            m.a();
        }
        }
        else
        {
            delay-=Time.deltaTime;
        }
        }
        }
        
    }
}
