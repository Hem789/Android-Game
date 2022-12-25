using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    public Transform pivot;
    private float time,reload;
    public float swim;
    public GameObject Right, Left,sphere,shoulder,effect1,pos1,pos2,bullet,bullet1,origin;
    public Animator anim;
    private RaycastHit hit;
    private Vector3 Shoot,Shoot2;
    public GameObject Gun,handGun,backGun,handGun1, backGun1;
    public Joystick joy,firestick;
    public FixedButton fire1bool;
    public AudioSource Sniper, rifle;
    public AudioClip snipeering;
    private GameManager manager;
    public LayerMask mask;
   // public Joybutton but;


    // Start is called before the first frame update
    void Start()
    {
        manager=FindObjectOfType<GameManager>();
        //joy=FindObjectOfType<Joystick>();
        Gun.SetActive(false);
        handGun.SetActive(true);
        backGun.SetActive(false);
        handGun1.SetActive(false);
        backGun1.SetActive(true);
        //but=FindObjectOfType<Joybutton>();
    }
    void OnAnimatorIK()
    {
        if(manager.Pause==true || manager.outside==false || manager.follow==false || transform.position.y<=swim)
        {
            rifle.enabled=false;
        }
        Gun.transform.position=shoulder.transform.position;
        if( transform.position.y>swim+.2F)
        {
           // backGun.SetActive(false);
        //if(Input.GetAxis("Fire1")>0  && Input.GetAxis("Horizontal")==0 && Input.GetAxis("Vertical")>=0)//
         if((Input.GetKey(KeyCode.M)|| fire1bool.Pressed==true || firestick.Horizontal!=0 || firestick.Vertical!=0)) //&& joy.Vertical>=0 && joy.Horizontal>=-.6F &&joy.Horizontal<=.6F)
        {
            fire1();
            
            

        }
        else
        {
            rifle.enabled=false;
            Gun.SetActive(false);
            if(manager.zoom==false)
            {
                handGun.SetActive(true);
                backGun1.SetActive(true);
                handGun1.SetActive(false);
                backGun.SetActive(false);
            }
            else
            {
                handGun1.SetActive(true);
                backGun.SetActive(true);
                handGun.SetActive(false);
                backGun1.SetActive(false);
            }
        }
        }
    else
    {
        backGun.SetActive(true);
        handGun.SetActive(false);
        Gun.SetActive(false);
        backGun1.SetActive(true);
        handGun1.SetActive(false);
    }
        
    }
    void fire1()
    {
        if(joy.Vertical>0)
        {
        anim.SetBool("back",false);
        }
        if(joy.Vertical<0  && ((manager.zoom==true && manager.sniper1>0) || (manager.zoom==false && manager.gun1>0)))
        {
            anim.SetBool("back",true);
        }
        //if(joy.Horizontal<=-.6F ||joy.Horizontal>=.6F || joy.Vertical<0)
        //{
        //transform.rotation=Quaternion.Euler(0,pivot.rotation.eulerAngles.y,0);
        //}

        if(manager.zoom==false)
        {
            
         if(transform.position.y>swim+.2F)//* joy.Vertical>=0 && */ && joy.Horizontal>=-.6F &&joy.Horizontal<=.6F)
        {
            if(manager.gun1>0)
            {
               // reload=false;
            MF_AutoPool.Spawn(effect1,pos1.transform.position,pos1.transform.rotation);
            //MF_AutoPool.Spawn(effect2,pos2.transform.position,pos2.transform.rotation);
            if(time<=0)
            {
            MF_AutoPool.Spawn(bullet,origin.transform.position,Gun.transform.rotation);
            rifle.enabled=true;
            manager.shoot();
            
            time=.1F;
            }
            else
            {
                time-=Time.deltaTime;
            }
            
            handGun.SetActive(false);
            Gun.SetActive(true);
            handGun1.SetActive(false);
            backGun.SetActive(false);
            backGun1.SetActive(true);

        
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            anim.SetIKPosition(AvatarIKGoal.RightHand,Right.transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand,Left.transform.position);
            transform.rotation=Quaternion.Euler(0,pivot.rotation.eulerAngles.y,0);
            
            if(Physics.Raycast(sphere.transform.position,sphere.transform.forward,out hit,75,~mask))
            {
                //Shoot=hit.point-Gun.transform.position;
                //Quaternion gunlook=Quaternion.LookRotation(new Vector3(Shoot.x,Shoot.y,Shoot.z));
                //Gun.transform.rotation=Quaternion.Slerp(Gun.transform.rotation,gunlook,.5F);
                Gun.transform.LookAt(hit.point);
                origin.transform.position=Gun.transform.position+Gun.transform.forward*1.093F;
                origin.transform.rotation=Gun.transform.rotation;
            }
            else
            {
                
                Gun.transform.rotation=pivot.transform.rotation;
            }
            }
            else 
            {
                Gun.SetActive(false);
                handGun.SetActive(true);
                rifle.enabled=false;
            }
        }
        //if(transform.position.y>swim+.2F)// && (joy.Vertical<0 || joy.Horizontal<-.6F ||joy.Horizontal>=.6F))
        //{
          //  rifle.enabled=false;
           // Gun.SetActive(false);
            //handGun.SetActive(true);
        //}
        }
        if(manager.zoom==true)
        {
            
         if(transform.position.y>swim+.2F)//* && joy.Vertical>=0 */&& joy.Horizontal>=-.6F &&joy.Horizontal<=.6F)
        {
            if(manager.sniper1>0)
            {
            handGun1.SetActive(false);
            handGun.SetActive(false);
            Gun.SetActive(true);
            backGun1.SetActive(false);
            backGun.SetActive(true);
        
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            anim.SetIKPosition(AvatarIKGoal.RightHand,Right.transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand,Left.transform.position);
            transform.rotation=Quaternion.Euler(0,pivot.rotation.eulerAngles.y,0);
            
            if(Physics.Raycast(sphere.transform.position,sphere.transform.forward,out hit,260,~mask))
            {
                //Shoot=hit.point-Gun.transform.position;
                //Quaternion gunlook=Quaternion.LookRotation(new Vector3(Shoot.x,Shoot.y,Shoot.z));
                //Gun.transform.rotation=Quaternion.Slerp(Gun.transform.rotation,gunlook,1F);
                Gun.transform.LookAt(hit.point);
                origin.transform.position=Gun.transform.position+Gun.transform.forward*1.093F;
                origin.transform.rotation=Gun.transform.rotation;
                //Shoot2=hit.point-origin.transform.position;
                //Quaternion gunlook2=Quaternion.LookRotation(new Vector3(Shoot2.x,Shoot2.y,Shoot2.z));
                //origin.transform.rotation=Quaternion.Slerp(Gun.transform.rotation,gunlook,1F);

            }
            else
            {
                
                Gun.transform.rotation=pivot.transform.rotation;
            }
            if(/*time<=0 && */ manager.sniperShoot)
            {
            if(manager.scoped==false)
            {
            MF_AutoPool.Spawn(bullet1,origin.transform.position,origin.transform.rotation);
            Sniper.PlayOneShot(snipeering,1);
            }
            if(manager.scoped==true)
            {
                MF_AutoPool.Spawn(bullet1,hit.point-sphere.transform.forward*1F,sphere.transform.rotation);
                MF_AutoPool.Spawn(bullet1,origin.transform.position,origin.transform.rotation);
                Sniper.PlayOneShot(snipeering,1);
            }
            manager.shoot();
            }
            }
        }
        
        }
    
    }
    
    // Update is called once per frame
    
    
}
