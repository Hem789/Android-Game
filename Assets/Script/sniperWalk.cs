using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperWalk : MonoBehaviour
{
    public Transform t1,t2,t3,t4,t5,t6,target,gunpoint;
    public GameObject gun,handgun,backGun,coin;
    private GameObject player;
    private RaycastHit hit;
    public float destroy,health=5,seenTime=5;
    private Vector3 moveDirection,distance;//,shootdirection,gunAngle;
    public Animator anime;
    public bool follow,shoot,dead=false;
    private bool seen=false,sit;
    private Collider mainCollider;
    public Rigidbody Rb;
    private GameManager manager;
    // Start is called before the first frame update
    void OnTriggerStay(Collider a)
    {
        if(a.gameObject.tag=="Player" && manager.outside==false)
        {
        manager.enemy();
        ragdoll(true);
        }
    }
    void OnTriggerEnter(Collider a)
    {
        if(a.gameObject.tag=="Bullet" || a.gameObject.tag=="Bullet0")
        {
            health--;
            seen=true;

        }
        if(a.gameObject.tag=="BigBullet2")
        {
            health=0;
        }
        if(a.gameObject.tag=="BigBullet")
        {
            health=0;
        }
        if(a.gameObject.tag=="Sniper")
        {
            health=0;
        }
    }
    void Start()
    {
        mainCollider=GetComponent<Collider>();
        manager=FindObjectOfType<GameManager>();
        ragdoll(false);
        
        
        target.position=t1.position;
        t1.transform.parent=null;
        t2.transform.parent=null;
        t3.transform.parent=null;
        t4.transform.parent=null;
        t5.transform.parent=null;
        t6.transform.parent=null;
        target.transform.parent=null;
    }
    
       

    // Update is called once per frame
    void LateUpdate()
    {
        if(health<=0 && dead==false)
        {
            ragdoll(true);
            dead=true;
            manager.enemy();
            MF_AutoPool.Spawn(coin,transform.position+new Vector3(0,1,0),transform.rotation);
        }
    }
    void FixedUpdate()
    {
        sit=manager.croach;
        player=GameObject.FindWithTag("Player");
    
        if(dead==true)
        {
            t1.transform.parent=transform;
            t2.transform.parent=transform;
            t3.transform.parent=transform;
            t4.transform.parent=transform;
            t5.transform.parent=transform;
            t6.transform.parent=transform;
            Destroy(gameObject,10);
        }
        if(dead==false)
        {
            if(manager.level==2)
            {
                Destroy(gameObject,180);
            }
        follow=false;
        anime.SetBool("move",false);
        anime.SetBool("run",false);
        //if(sit==true)
        //{
            distance=transform.position-player.transform.position;
            //if(manager.outside==false || manager.scoped==true)
            //{
        if(distance.magnitude>=250)
        {
            anime.enabled=false;
        }
        if(distance.magnitude<250)
        {
            anime.enabled=true;
        }
            //}
           
            gunpoint.transform.LookAt(player.transform.position+new Vector3(0,.8F,0));
        //}
        //if(sit==false)
        //{
          //  gunpoint.transform.LookAt(player.transform.position+new Vector3(0,1.5F,0));
        //}
            if(Physics.Raycast(gunpoint.position,gunpoint.transform.forward,out hit,200))
            {
                if(hit.transform.gameObject.tag=="Player" )
                {
                    seen=true;
                    seenTime=5;
                    if( moveDirection.magnitude<49)
                    {
                    //gun.transform.LookAt(hit.point);
                    shoot=true;
                   
                    }
                    
                }
                if(hit.transform.gameObject.tag=="Player"  && seenTime>-1)
                {
                    seenTime-=Time.deltaTime;
                    if(seenTime<=0)
                    {
                    seen=false;
                    shoot=false;
                    }
                }
            }
        
        
        
         if(seen==false)
        {
        if(target.position==t6.position && moveDirection.magnitude<=0.2F) 
        {
            target.position=t1.position;
        }
         if(target.position==t5.position && moveDirection.magnitude<=0.2F)
        {
            target.position=t6.position;
        }
         if(target.position==t4.position && moveDirection.magnitude<=0.2F)
        {
            target.position=t5.position;
        }
         if(target.position==t3.position && moveDirection.magnitude<=0.2F)
        {
            target.position=t4.position;
        }
         if(target.position==t2.position && moveDirection.magnitude<=0.2F)
        {
            target.position=t3.position;
        }
         if(target.position==t1.position && moveDirection.magnitude<=0.2F)
        {
            target.position=t2.position;
        }
        
       
        
        moveDirection=target.position-transform.position;
        Quaternion lookway=Quaternion.LookRotation(new Vector3(moveDirection.x,0,moveDirection.z));
        transform.rotation=Quaternion.Slerp(transform.rotation,lookway,0.5F);
        anime.SetBool("move",true);
        }
        if(seen==true)
        {
           transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));

        
        if(moveDirection.magnitude>50)
        {
        follow=true;
        anime.SetBool("move",false);
        }
        }
       
        } 
    }
    void ragdoll(bool a)
    {
    gun.SetActive(false);
    handgun.SetActive(false);
    backGun.SetActive(false);
    mainCollider.enabled=!a;
    anime.enabled=!a;
    
            
    }
}
