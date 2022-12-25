using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask mask;
    private RaycastHit ground,ground1;
    private BoxCollider colli;
    //private Collider[] ragdoll;
    public Rigidbody rb;

    private Vector3 Direction;
    public float moveSpeed,swim,swimf,height;
    private float time=8,reloadTime;
    public Joystick joystick,firestick;
    public Animator anime;
   
    public Transform pivot,piv,cam;
    public FixedButton jumpbool,firebool;//croachbool;
    private bool ragdollcontrol=false,falling=false;
    public bool croach=false;
    public AudioSource Swimming,reloader;
    public AudioClip reloading;
    private GameManager manager;
    private IK shoot;
    public Collision col;
    void OnTriggerEnter(Collider a)
    {
        if(falling==false )
        {
        if(a.gameObject.tag=="Bullet0")
        {
            manager.hit(1);
        }
        if(a.gameObject.tag=="Sniper" || a.gameObject.tag=="BigBullet2")
        {
            manager.hit(20);
        }
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        manager=FindObjectOfType<GameManager>();
        colli=GetComponent<BoxCollider>();
        rb=GetComponent<Rigidbody>();
        //ragdoll=GetComponentsInChildren<rb>(true);
        shoot=GetComponent<IK>();
        doll(ragdollcontrol);
        Swimming.enabled=false;
    }
    void LateUpdate()
    {
        if(manager.healthCount>0)
        {
        if(Physics.Raycast(transform.position+new Vector3(0,1F,0),new Vector3(0,-1,0),out ground,50, ~mask))
        {
            if(transform.position.y>swim)
            {
                rb.AddForce(transform.up*(-5000));
            }
            if(ground.distance>height )
           {
                anime.Play("Falling");
                //rb.AddForce(transform.up*(-15));
           }
            if(ground.transform.gameObject.tag=="Ground")
           {

                if(ground.distance>height )
           {
           falling=true;
           rb.AddForce(new Vector3(0,-180,0));
           colli.center=new Vector3(colli.center.x,0F,colli.center.z);
           colli.size=new Vector3(colli.size.x,.4F,colli.size.z);
           }
            }
            if(ground.distance<=1.8F && falling==true)
            {
                anime.SetBool("hitGround",true);
                transform.position=new Vector3(transform.position.x,ground.point.y,transform.position.z);
               /* if(transform.position.y>-1.6F)
                {
                rb.AddForce(new Vector3(0,-180F,0));
                }*/
                

                manager.healthCount=0;
                ragdollcontrol=true;
                falling=false;
                doll(true);
            
            }
        }
        }
       
    }
    void FixedUpdate()
    {
        if(manager.croach==true)
        {
            if((Input.GetKey(KeyCode.M)|| firebool.Pressed==true || firestick.Horizontal!=0 || firestick.Vertical!=0))// && (joystick.Vertical>=0 ||Input.GetAxis("Vertical")>0) && joystick.Horizontal>=-.6F &&joystick.Horizontal<=.6F)
        {
                if(joystick.Vertical>=0)
                {
                anime.SetFloat("croach",1);
                }
                if(joystick.Vertical<0)
                {
                    anime.SetFloat("croach",-1);
                }
            }
            else
            {
                anime.SetFloat("croach",1);
            }
        }
        if(transform.position.y<=swim+.2F && transform.position.y>=swimf-0.2F && ragdollcontrol==false)
        {
            anime.SetBool("Swim",true);
            falling=false;
            colli.enabled=true;
            //if(Input.GetAxis("Vertical")==0||Input.GetAxis("Horizontal")==0)
            if(joystick.Vertical==0 && joystick.Horizontal==0)
            {
                anime.Play("Swimming Idle");
            transform.position=new Vector3(transform.position.x,swimf,transform.position.z);
            //transform.rotation=Quaternion.Euler(0,cam.transform.rotation.y,0);
            //transform.LookAt(new Vector3(cam.transform.position.x,transform.position.y,cam.transform.position.z));
            Swimming.enabled=false;
            }
            //if(Input.GetAxis("Vertical")!=0||Input.GetAxis("Horizontal")!=0)
            if(joystick.Vertical!=0 || joystick.Horizontal!=0)
            {
                anime.Play("Swim Forward");
            transform.position=new Vector3(transform.position.x,swim,transform.position.z);
            transform.position+=transform.forward*0.1F;
            //transform.LookAt(null);
            Swimming.enabled=true;
            }
            rb.useGravity=false;
            
        }
        if(transform.position.y<swimf-0.2 && ragdollcontrol==false)
        {
            anime.SetBool("Swim",true);
            transform.position+=new Vector3(0,0.03F,0);
            rb.useGravity=false;
            falling=false;
            colli.enabled=true;
        }
        if(transform.position.y>swim+0.2 && ragdollcontrol==false)
        {
            rb.useGravity=true;
            Swimming.enabled=false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.Pause==true || manager.outside==false || manager.follow==false )
        {
            Swimming.enabled=false;
            reloader.enabled=false;
        }
        if(manager.Pause==false)
        {
        reloader.enabled=true;
        pivot.transform.parent=null;
        pivot.transform.position=transform.position;
        if(ragdollcontrol==true)
        {
            manager.over();
            Swimming.enabled=false;
        if(time<=0)
            {
                manager.gameOver();
            }
            else
            {
                time-=Time.deltaTime;
            } 
        }
        if(ragdollcontrol==false)
        {
        if(/*manager.level==2 &&*/ manager.healthCount==0)
        {
            ragdollcontrol=true;
            doll(true);
            //rb.constraints=RigidbodyConstraints.FreezePositionY;
        }
        float yStr=Direction.y;
        Direction=piv.transform.forward*joystick.Vertical+piv.transform.right*joystick.Horizontal;
        Direction=Direction.normalized * moveSpeed;
        Direction.y=yStr;
        anime.SetBool("run",false);
        anime.SetBool("ufri",false);
        anime.SetBool("Fire",false);
        anime.SetBool("Swim",false);
        if(manager.canChange==true)
        {
        anime.SetBool("reload",false);
        }
        
        if(joystick.Horizontal!=0 || joystick.Vertical!=0)
        { 
            anime.SetBool("run",true);
               if(Input.GetKey(KeyCode.M)||shoot.fire1bool.Pressed==true || firestick.Horizontal!=0 || firestick.Vertical!=0)
        {
            anime.SetBool("run",true);
            if(joystick.Vertical>0)
            {
            rb.AddForce(transform.forward*20000);

            if(joystick.Horizontal>0)
            {
            rb.AddForce(transform.right*10000);
            }
            if(joystick.Horizontal<0)
            {
            rb.AddForce(-transform.right*10000);
            }
            }
            
             if(joystick.Vertical<0)
            {
            rb.AddForce(-transform.forward*10000);
            if(joystick.Horizontal>0)
            {
            rb.AddForce(transform.right*1000);
            }
            if(joystick.Horizontal<0)
            {
            rb.AddForce(-transform.right*100);
            }
            }
           
        }
         else
            {
                rb.AddForce(transform.forward*20000);
            }



            
            
           
            Quaternion turn=Quaternion.LookRotation(new Vector3(Direction.x,0,Direction.z));
            if(firebool.Pressed==false && firestick.Horizontal==0 && firestick.Vertical==0 && Input.GetKey(KeyCode.M)==false)
            {

            transform.rotation=Quaternion.Slerp(transform.rotation,turn,2F);
            }
            else
            {
                if((manager.zoom==true && (manager.sniper1>0 || manager.sniper>5)) || (manager.zoom==false && (manager.gun1>0 || manager.gun>30)))
                {
                if(joystick.Vertical<0)
                transform.position+=transform.right*joystick.Horizontal*.035F;
                //rb.AddForce(transform.right*joystick.Horizontal*100);
                if(joystick.Vertical>=0)
                //rb.AddForce(transform.right*joystick.Horizontal*1000);
                transform.position+=transform.right*joystick.Horizontal*.07F;
                
                }
                else
                {
                    transform.rotation=Quaternion.Slerp(transform.rotation,turn,.5F);
                }
            }

        }
       
        if(Input.GetButtonDown("Jump") || jumpbool.Pressed)
        {
            if(Physics.Raycast(transform.position,new Vector3(0,-1,0),out ground1,0.1F))
            {
                if(ground1.transform.gameObject.tag=="Ground")
                {
           jump();
                }
            }
        }
        if((Input.GetKey(KeyCode.M)|| firebool.Pressed==true || firestick.Horizontal!=0 || firestick.Vertical!=0))// && (joystick.Vertical>=0 ||Input.GetAxis("Vertical")>0) && joystick.Horizontal>=-.6F &&joystick.Horizontal<=.6F)
        {
           fire();
            
        }
        else
        {
            anime.SetBool("back",false);
        }
    
        
        }
        }
    }
    void jump()
    {
        //anime.SetBool("ufri",true);
        anime.Play("Jump");
        rb.AddForce(transform.up*500);
    }
    void fire()
    {
        if(manager.gun1!=0 && manager.sniper1!=0)
        {
            if(joystick.Vertical>=0 )
            {
                anime.SetBool("Fire",true);
                reloadTime=0;
            }
        }
        else
        {
            if((manager.sniper1<=0 && manager.zoom==true && manager.sniper>0) || (manager.gun1<=0 && manager.zoom==false&& manager.gun>0))
            { 
            anime.SetBool("reload",true);
            if(reloadTime==0)
            {
            reloader.PlayOneShot(reloading,1);
            reloadTime++;
            }
            }
        }
    }
    public void reload()
    {
        if(manager.canChange==true)
        {
            if((manager.zoom==true &&manager.sniper>0)||(manager.zoom==false &&manager.gun>0))
            {
        reloader.PlayOneShot(reloading,1);
        anime.SetBool("reload",true);
        manager.reloadPressed();
            }
        }
    }

    
    void doll(bool ragdollcontrol)
    {
        //foreach(var col in ragdoll)
        //{
          //  col.enabled=ragdollcontrol;
        //}
        shoot.Gun.SetActive(false);
        shoot.rifle.enabled=false;
        if(falling==false)
        {
        rb.useGravity=!ragdollcontrol;
        colli.enabled=!ragdollcontrol;
        anime.enabled=!ragdollcontrol;
        }
    }    
    void sitting(bool sit)
    {
        if(sit==true)
        {
            colli.size=new Vector3(0.5F,1.2F,1F);
            colli.center=new Vector3(0,0.6F,0.2F);
            anime.SetBool("sit",true);
            manager.sit();
        }
        if(sit==false)
        {
            colli.size=new Vector3(0.7F,1.843569F,0.7F);
            colli.center=new Vector3(0,0.9F,0);
            anime.SetBool("sit",false);
            manager.stand();
        }

    }
    public void sit()
    {
        croach=!croach;
        sitting(croach);
    }
}