using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Transform pivot,piv;
    public Rigidbody rb;
	public GameObject frontPropeller, backPropeller,left,right,sphere,bullet,cam;
    private bool fly=false,firebool;
    private Vector3 direction,rot,lrot,rrot;
    private float yStore, bulletCount=50, fireGap;
    //private GetIn1 enter;
    public Joystick joystick1,joystick2,firestick;
    public FixedButton fire;
    private RaycastHit hit;
    public AudioSource firing;
    private GameManager manager;
    public Transform hcam;
    public LayerMask mask;
    public float fireDelay=.1F;
    
    void Awake()
    {
       
        //fireDelay=0.1F;
        manager=FindObjectOfType<GameManager>();
       
    }
    // Start is called before the first frame update
    void OnTriggerStay (Collider a)
    {
        if(a.gameObject.tag=="Ground")
        {
        fly=false;
        left.transform.rotation=transform.rotation;
        right.transform.rotation=transform.rotation;
        rb.useGravity=true;   
            
        }

        
    }
    void OnCollisionExit(Collision b)
    {
        if(b.gameObject.tag=="Ground")
        {
            fly=true;
           
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Camera.main.transform.position=hcam.transform.position;
        Camera.main.transform.rotation=hcam.transform.rotation;
        if(manager.Pause==true)
        {
            firing.enabled=false;
        }
       if(manager.Pause==false)
       {
        if(fire.Pressed==false)
        {  
            
            firebool=false;
        }
        if(fire.Pressed==true || firestick.Horizontal!=0 || firestick.Vertical!=0)
        {
            firebool=true;
            //firing.enabled=true;
        }
        
        pivot.transform.parent=null;
        pivot.transform.position=transform.position;  
       

        if(transform.position.y>=-9)
        {
        frontPropeller.transform.Rotate (0, 50, 0);
		backPropeller.transform.Rotate (35, 0, 0);
        
        
        if(transform.position.y<=25)
        {
            //transform.position+=new Vector3(0,.15F*Input.GetAxis("Vertical"),0);
            transform.position+=new Vector3(0,.15F*joystick2.Vertical,0);

            
        }
       
        if(transform.position.y>=25)
        {
        transform.position=new Vector3(transform.position.x,25,transform.position.z);
        }
        frontPropeller.transform.localEulerAngles += new Vector3 (0, joystick2.Vertical * 3, 0);
        //frontPropeller.transform.localEulerAngles += new Vector3 (0, Input.GetAxis ("Vertical") * 3, 0);

		
        if(fly==true)
        {
            if (joystick2.Vertical == 0) 
            //if (Input.GetAxis("Vertical") == 0) 
        {
            transform.position+=new Vector3(0,-.005F,0);
        }
            rb.useGravity=false;
            //if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("heliForward")!=0 || Input.GetAxis("heliRight")!=0)
          //  if(joystick1.Horizontal!=0 || joystick1.Vertical!=0 || joystick2.Horizontal!=0)
        //{
            transform.Rotate(0,.7F*joystick2.Horizontal,0);
            yStore=direction.y;
            //direction=transform.forward*Input.GetAxis("heliForward")+transform.right*Input.GetAxis("Horizontal");
            //if(joystick1.Vertical!=0)
            //{
            direction=(transform.forward*joystick1.Vertical*3/*+transform.forward*1*/)+transform.right*joystick1.Horizontal*2;
            //}
            //else
            //{
              //  direction=transform.right*joystick1.Horizontal;
            //}
            direction=direction.normalized*5F;
            direction.y=yStore;
            rb.velocity=direction*3;
       
		//float rotx = Input.GetAxis ("heliForward");
		//float rotz = -Input.GetAxis ("Horizontal");
        float rotx = joystick1.Vertical;
		float rotz = -joystick1.Horizontal;
		transform.localEulerAngles = new Vector3 (rotx*15,transform.localEulerAngles.y,rotz* 15);
		//transform.localEulerAngles += new Vector3 (0, Input.GetAxis ("heliRight") * .5F, 0);
       transform.localEulerAngles += new Vector3 (0, joystick2.Horizontal * .5F, 0);
      
        //}
        //if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("heliForward")!=0 || Input.GetAxis("heliRight")!=0)
        //if(joystick1.Horizontal!=0 || joystick1.Vertical!=0 || joystick2.Horizontal!=0)
        //{
          //  transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,transform.rotation.eulerAngles.y,0),3);
        //}
        }
        if(firebool==false)
        {
            firing.enabled=false;
        pivot.transform.rotation=Quaternion.Slerp(pivot.transform.rotation,Quaternion.Euler(0,0,0)/*transform.rotation*/,0.4F*Time.deltaTime);
        }
        }
        //firing.enabled=false;
        if(firebool==true || Input.GetKey(KeyCode.M))
        {
            if(fly==true)
            {
            if(Physics.Raycast(sphere.transform.position,cam.transform.forward,out hit,50000,~mask))
            {
                rot=hit.point-transform.position;
                lrot=hit.point-left.transform.position;
                rrot=hit.point-right.transform.position;

                
                Quaternion turn=Quaternion.LookRotation(new Vector3(rot.x,rot.y,rot.z)); 
                //if(turn.eulerAngles.x<-15)
                //{
                    turn=Quaternion.Euler(0,turn.eulerAngles.y,0);
                //}
                //if(turn.eulerAngles.x>15)
                //{
                //    turn=Quaternion.Euler(15,turn.eulerAngles.y,0);
                //}
                Quaternion turnl=Quaternion.LookRotation(new Vector3(lrot.x,lrot.y,lrot.z));
                Quaternion turnr=Quaternion.LookRotation(new Vector3(rrot.x,rrot.y,rrot.z));
                transform.rotation=Quaternion.Slerp(transform.rotation,turn,.2F);
                left.transform.rotation=Quaternion.Slerp(left.transform.rotation,turnl,5F);
                right.transform.rotation=Quaternion.Slerp(right.transform.rotation,turnr,5F);
                
                
                
               
            }
            else
            {
                
                Quaternion a=Quaternion.Euler(0,piv.transform.rotation.eulerAngles.y,0);
                transform.rotation=Quaternion.Slerp(transform.rotation,a,.1F);
                left.transform.rotation=transform.rotation;
                right.transform.rotation=transform.rotation;
               
            }
            

            }
            if(fireDelay<=0)
            {
            MF_AutoPool.Spawn(bullet,left.transform.position,left.transform.rotation);
            MF_AutoPool.Spawn(bullet,right.transform.position,right.transform.rotation);
            if(Physics.Raycast(sphere.transform.position,cam.transform.forward,out hit,500))
            {
                if(transform.position.y>3)
                {
                MF_AutoPool.Spawn(bullet,hit.point-transform.forward*3,left.transform.rotation);
                MF_AutoPool.Spawn(bullet,hit.point-sphere.transform.forward*3,sphere.transform.rotation);
                }
            }
            firing.enabled=true;
            //MF_AutoPool.Spawn(effect,left.transform.position-transform.forward*0.1F,left.transform.rotation);
            //MF_AutoPool.Spawn(effect,right.transform.position-transform.forward*0.1F,right.transform.rotation);
            fireDelay=0.1F;
            bulletCount--;
            }
            
            else
            {
                fireDelay-=Time.deltaTime;
            }
            if(bulletCount<=0)
            {
                
                fireGap=5;
                bulletCount=50;
            }
            if(fireGap>0)
            {
                //firing.enabled=false;
                fireGap-=Time.deltaTime;
            }
        }
        if( joystick1.Horizontal!=0|| joystick1.Vertical!=0 )
        piv.transform.rotation=Quaternion.Slerp(piv.transform.rotation,/*Quaternion.Euler(piv.transform.eulerAngles.x,transform.rotation.eulerAngles.y,piv.rotation.eulerAngles.z)*/transform.rotation,.5F*Time.deltaTime);
       }   
    }
}
