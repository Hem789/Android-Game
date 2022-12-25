using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIn1 : MonoBehaviour
{
    Helicopter heli;
    public Player x;
    public GameObject player, helicam,playerControl,heliControl,enter,fan,backfan,fire,heleecam;
    public GameObject camu;
    private bool inside,playerout,dead=false,playSound=false;//,fireSet=false;
    private RaycastHit rey;
    public float health;
    public bool fly=false;
    private Camera cam;
    private float d,time=5,black,drawntime=10,boomtime;
    public float swim;
    public FixedButton getin,exit;
    public Renderer rando;
    public AudioSource flying,explosion;
    public AudioClip explode;
    private Rigidbody Rig;
    private GameManager manager;
    public LayerMask mask;
    void Awake()
    {
        heleecam.transform.localRotation=Quaternion.Euler(0,0,0);
        heleecam.SetActive(false);
        manager=FindObjectOfType<GameManager>();
        cam=Camera.main;
        x=FindObjectOfType<Player>();
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag=="Bullet" || c.gameObject.tag=="Bullet0")
        {
            health--;
        }
        if(c.gameObject.tag=="BigBullet2"|| c.gameObject.tag=="BigBullet")
        {
            health-=4;
        }
        if(c.gameObject.tag=="sniper")
        {
            health-=4;
        }
    }

    // Start is called before the first frame update
    void OnTriggerStay(Collider a)
    {
        if(a.gameObject.tag=="Player" && manager.scoped==false)
        {
            enter.SetActive(true);
            if(inside==false && (Input.GetKey(KeyCode.F)||getin.Pressed==true))
            {
            heleecam.SetActive(true);
            heleecam.transform.parent=null;
            heli.enabled=true;
            a.gameObject.transform.parent=transform;
            cam.transform.parent=helicam.transform;
            cam.transform.position=helicam.transform.position;
            cam.transform.rotation=helicam.transform.rotation;
            cam.farClipPlane=250F;
            player.transform.localScale=new Vector3(1,1,1);
            player.SetActive(false);
            inside=true;
            manager.vehicleEnter();
            //sphere.SetActive(true);
            playerout=false;
            //xirbool=false;
            playerControl.SetActive(false);
            heliControl.SetActive(true);
            enter.SetActive(false);
            getin.Pressed=false;
            //sphere.SetActive(false);
            transform.gameObject.tag="Player";
            playSound=true;
            if(x.croach==true)
            {
                x.sit();
            }
            }
        }
        
    }
     void OnTriggerExit(Collider a)
    {
        if(a.gameObject.tag=="Player")
        {
        enter.SetActive(false);
        }
    }
    void Start()
    {
        heli=GetComponent<Helicopter>();
        heli.enabled=false;
        if(player==null)
        player=GameObject.FindWithTag("Player");
        inside=false;
        playerout=false;
        //sphere.SetActive(false);
        fire.SetActive(false);
        Rig=GetComponent<Rigidbody>();
//        c=1;
        d=3;
        black=30;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.position.y<-20)
        {
         Destroy(gameObject);   
        }
        if(transform.position.y>.2F)
        {
            fly=true;
        }
        if(manager.Pause==true)
        {
            flying.enabled=false;
        }
        if(manager.Pause==false)
        {
        flying.enabled=playSound;
        if(health<=0)
        {
            if(dead==false && transform.position.y>swim)
            {
                fire.SetActive(true);
                fire.transform.rotation=Quaternion.Euler(0,0,0);
                
            }
            if(transform.position.y<=swim)
            {
                fire.SetActive(false);
                heli.enabled=false;
            }
            if(black<=0)
            {
            dead=true;
            }
            else
            {
                black-=Time.deltaTime;
            }
        }
        if(dead==false)
        {

        if(playerout==true)
        {
            if(Physics.Raycast(transform.position,new Vector3(0,-1,0),out rey,40,~mask))
            {
            if(transform.position.y>swim)
            {
                Rig.useGravity=true;
                
                if(rey.distance>1 || rey.transform.gameObject.tag=="Water")
                {
                fly=true;
                transform.position+=transform.forward*.2F;//+new Vector3(0,-.2F,0)-transform.up*.1F;
                transform.Rotate(-1,2.5F,0);
            
                fan.transform.Rotate(0,10,0);
                backfan.transform.Rotate(10,0,0);
                }
                if(transform.position.y<=1 || rey.distance<=1)
                {
                    fly=false;
                //Rig.useGravity=true;
                if(d<=0)
            {
                d=15;
                playerout=false;
                playSound=false;
                
                
               
            }
            else
            {
                fan.transform.Rotate(0,5,0);
                backfan.transform.Rotate(3,0,0);
                d-=Time.deltaTime;
                
            }
                }
            }
            if(transform.position.y<=swim)
            {
                //Rig.constraints=RigidbodyConstraints.FreezePositionX;  
            //Rig.constraints=RigidbodyConstraints.FreezeRotationZ;  
            
            //Rig.constraints=RigidbodyConstraints.FreezePositionZ;
            //Rig.constraints=RigidbodyConstraints.FreezeRotationX;
            //Rig.constraints=RigidbodyConstraints.FreezeRotationY;
              //  Rig.constraints=RigidbodyConstraints.FreezePositionY;
                flying.enabled=false;
                Destroy(gameObject,10);
             
            }
            }
            
        }
        if(inside==true && (Input.GetKey(KeyCode.X)|| exit.Pressed==true))
        {
            player.SetActive(true);
            
            player.transform.localScale=new Vector3(1F,1F,1F);
            player.transform.parent=null;
            player.transform.rotation=Quaternion.Euler(0,player.transform.rotation.eulerAngles.y,0);
            cam.transform.parent=camu.transform;
            cam.transform.position=camu.transform.position;
            cam.transform.rotation=camu.transform.rotation;
            cam.farClipPlane=100F;
            heleecam.transform.parent=transform;
            heli.enabled=false;
            inside=false;
            manager.vehicleExit();
            playerControl.SetActive(true);
            heliControl.SetActive(false);
            playerout=true;
            exit.Pressed=false;
            //sphere.SetActive(true);
            transform.gameObject.tag="Vehicle";
            //playSound=false;
            heleecam.SetActive(false);
           
        }
        if(transform.position.y<=swim+1 && inside==true)
        {
            playSound=false;
            flying.enabled=false;
           // heli.pivot.transform.parent=transform;
            //heli.pivot.transform.position=transform.position;
            cam.transform.position=helicam.transform.position;
            cam.transform.rotation=helicam.transform.rotation;
            heli.enabled=false;
            if(drawntime<=0 && inside==true)
            {
                manager.gameOver();
            }
            if(drawntime>0)
            {
                drawntime-=Time.deltaTime;
            }
             if(drawntime<=0 && inside==false)
            {
                Destroy(gameObject,10);
            }

            
        }
        if(transform.position.y<=swim+1 && inside==false)
        {
            playSound=false;
            heli.enabled=false;
        }
        }
        else
        {
            if(boomtime==0)
            {
            explosion.PlayOneShot(explode,1);
            heli.fireDelay=50f;
            heli.firing.enabled=false;
            boomtime++;
            }
            rando.material.color=Color.black;
            playSound=false;
            
            fire.SetActive(false);
            heliControl.SetActive(false); 
            Destroy(gameObject,10);
            //Physics.Raycast(transform.position,-transform.up,out rey,1);
            if(transform.position.y>1)
            {
            
            //fan.transform.Rotate(0,10,0);
            //backfan.transform.Rotate(10,0,0);
            transform.position+=transform.forward* .1F-transform.up*0.2F;
//            transform.Rotate(-1,2.5F,0F);
            }
            else
            {
               /* if(d<=0)
                {
            //fan.transform.Rotate(0,10,0);
            //backfan.transform.Rotate(10,0,0);
//            transform.Rotate(0,0,0);
                }
                if(d>0)
                {
                    d-=Time.deltaTime;
                }*/
                if(inside==true)
                {
                    //game over
                    manager.gameOver();
                }
                else
                {
                 if(time<=0)
                {
                   
                    Destroy(gameObject,10);
                }
                if(d<=0 && time>0)
                {
                    time-=Time.deltaTime;
                }
                }
            }
        }
        }
    }
   
    
}
