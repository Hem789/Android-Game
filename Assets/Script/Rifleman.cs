using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifleman : MonoBehaviour
{
    public GameObject handgun,gun,backgun;//,effect;//,ragpiv;
    public Animator anim;
    private float gunDelay=0.1F,reload=0,bulletCount;
    public float a,swim;
    private GameObject Player;
    public GameObject Right, Left,bullet;
    private Vector3 playerDistance;

   // private Vector3 shootDirection;
    private Collider colli;  
    private enemyWalk walk;
    public AudioSource reloader,shoot;
    public AudioClip reloading;
    private GameManager manager;
    //private bool seen;  // Start is called before the first frame update
    

void Awake()
{
    walk=GetComponent<enemyWalk>();
    Player=GameObject.FindWithTag("Player");
    handgun.SetActive(false);
    gun.SetActive(false);
    backgun.SetActive(true);
    //effect.SetActive(false);
    bulletCount=a;
    manager=FindObjectOfType<GameManager>();
    shoot.transform.parent=gun.transform;
}

    void Update()
    {
        //if(!Player)
        //{
            Player=walk.player;
        //}
        //if(Player)
        //{
       playerDistance=Player.transform.position-transform.position;
        //}
        if(manager.Pause==true)
       {
           reloader.enabled=false;
            shoot.enabled=false;
            
       }
       
    }
   
    void OnAnimatorIK()
    {
        if (manager.Pause==false)
        {
       //Player=GameObject.FindWithTag("Player");
        if((walk.seen==false && walk.dead==false) || manager.follow==false) 
        {
            backgun.SetActive(true);
            handgun.SetActive(false);
            gun.SetActive(false);
        }
        if(walk.seen==true && walk.dead==false) 
        {
         if(transform.position.y<=swim+.2F)
        {
            backgun.SetActive(true);
            handgun.SetActive(false);
            gun.SetActive(false);
        }
        
        anim.SetBool("reload",false);
        
        if(reload>0 && transform.position.y>swim+.2F && manager.follow==true && walk.seen==true)
        {
            anim.SetBool("reload",true);
        }
        

if(playerDistance.magnitude<=10)
{

        shoot.enabled=true;
    
        reloader.enabled=true;
        //if(manager.Pause==true || manager.follow==false)
        //{
            
        //}
}
else
{
    shoot.enabled=false;
    reloader.enabled=false;
}       
        if( transform.position.y>swim+.2F)
        {
        
        if(walk.follow==true)
        {
            //if(transform.position.y>swim+.2F)
            //{
            anim.SetBool("shoot",false);
            handgun.SetActive(true);
            gun.SetActive(false);
            backgun.SetActive(false);
            //}

        }
        
        if(walk.shoot==true)
        {
            

            if(reload<=0)
            {
            anim.SetBool("shoot",true);
            
            
            
            if(manager.croach==true)
            {
            gun.transform.LookAt(Player.transform.position+new Vector3(0,.75F,0));
            }
            if(manager.croach==false)
            {
            gun.transform.LookAt(Player.transform.position+new Vector3(0,1.4F,0));
            }
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            anim.SetIKPosition(AvatarIKGoal.RightHand,Right.transform.position);
           
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand,Left.transform.position);
            
            }
            if(reload<=0)
            {
               // shoot.enabled=true;
            }
            if(gunDelay<=0 && reload<=0)
            {
            
            anim.SetBool("reload",false);
            MF_AutoPool.Spawn(bullet,gun.transform.position+gun.transform.forward*1F,gun.transform.rotation);
            //shoot.enabled=true;

            if(transform.position.y>swim+.2F)
            {
            gun.SetActive(true);
            handgun.SetActive(false);
            backgun.SetActive(false);
            }
            //effect.SetActive(true);
            gunDelay=0.1F;
            bulletCount--;
            }
            //effect.SetActive(false);
            if(bulletCount<=0)
            {
                reload=2;
                reloader.enabled=true;
                reloader.PlayOneShot(reloading,1);
                ///shoot.enabled=false; 
                
            }
            if(gunDelay>0)
            {
                gunDelay-=Time.deltaTime;
            }
            if(reload>0)
            {
                //shoot.enabled=false;
                if(transform.position.y>swim+.2F)
            {
                gun.SetActive(false);
            handgun.SetActive(true);
            backgun.SetActive(false);
            }
                reload-=Time.deltaTime;
                bulletCount=a;
            }
        }
        if(walk.shoot==false )
        {
            if(transform.position.y>swim+.2F)
            {
            //shoot.enabled=false;
            gun.SetActive(false);
            handgun.SetActive(true);
            backgun.SetActive(false);
            }
        }
        }
        else
        {
            if(transform.position.y<swim+.2F)
            {
            anim.SetBool("shoot",false);
            //shoot.enabled=false;
            gun.SetActive(false);
            handgun.SetActive(false);
            backgun.SetActive(true);
            }
        }
        }
        }
    }
}
