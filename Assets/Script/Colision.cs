using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    public float distance,aStore,timer;
    private RaycastHit hit,hit0;
    public Transform camHolder,pivot;
    private Camera cam;
    public GameObject player;
    private Vector3 rayDirection,rayDirection0,length,a;
    private GameManager manager;
    public LayerMask plr;
    private float rayDist=7;
    void Awake()
    {
        manager=FindObjectOfType<GameManager>();
        cam=Camera.main;
    }
    void Start()
    {
        a=pivot.transform.position-transform.position;
        aStore=a.magnitude;
    }
    void OnTriggerStay(Collider a)
    {
        if(a.gameObject.tag!="Player" && length.magnitude<=distance)
        {
             cam.transform.position+= cam.transform.forward*.3F;



        }
    }
    
    
    void FixedUpdate()
    {
        //player=GameObject.FindWithTag("Player");
        a=pivot.transform.position-transform.position;

        length=cam.transform.position-camHolder.transform.position;
        if(manager.outside==true)
        {
            rayDist=6.8F;
        }
        if(manager.outside==false)
        {
            rayDist=30;
        }
        if(manager.scoped==false)
        {
        if( manager.follow==true)
        {
        //player=GameObject.FindWithTag("Player");
        transform.position=cam.transform.position;
        rayDirection=player.transform.position+new Vector3(0,1.8F,0)-transform.position;
        rayDirection0=player.transform.position+new Vector3(0,1.8F,0)-camHolder.transform.position;
        if(Physics.Raycast(camHolder.position-transform.forward*2.5F,rayDirection0,out hit0,rayDist,~plr))
        {
        if(hit0.transform.gameObject.tag!="Player")
        {
            if(manager.outside==false)
            {
               cam.transform.position=pivot.transform.position-pivot.transform.forward*4.5F;
            }

        if(Physics.Raycast(transform.position-transform.forward*2.5F,rayDirection,out hit,rayDist,~plr))
        {
        
        if(hit.transform.gameObject.tag!="Player" )
        {
            if(length.magnitude<distance)
            {
                if(manager.outside==true)
            {
                if(timer<=0)
                {
            cam.transform.position+= cam.transform.forward*.3F;
                }
                else
                {
                    timer-=Time.fixedDeltaTime;
                }
            //if(hit.transform.gameObject.tag=="Wall")
            //cam.nearClipPlane=.7F;
            }
            }
        }
        
        }
        
        }
        
        else
        { if(length.magnitude>0 && a.magnitude<aStore)
        {
            cam.transform.position-=cam.transform.forward*.3F;
            timer=.5F;
            //cam.nearClipPlane=.15F;
        }
        }
        
        }

        else
        {
            cam.transform.position=camHolder.transform.position;
            timer=.5F;
            //cam.nearClipPlane=.15F;
            //if(length.magnitude>0)
        //{
          //  cam.transform.position-=cam.transform.forward*.3F;
        //}
        }
        }
        }
        if(manager.outside==true)
        {
        if(manager.scoped==true)
        {
            cam.transform.position=camHolder.transform.position+camHolder.transform.forward*5F;
            cam.farClipPlane=250F;
        }      
        if(manager.scoped==false)
        {
            cam.farClipPlane=100F;
        }
        }
    }
    
    
}