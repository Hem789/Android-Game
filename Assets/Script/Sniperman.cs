using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniperman : MonoBehaviour
{
    public GameObject handgun,gun,backgun;
    public Animator anim;
    private float gunDelay=0.1F,reload=0,bulletCount;
    public float a;
    private GameObject Player;
    public GameObject Right, Left,bullet;
    private RaycastHit hit;
    private Vector3 shootDirection;
    private Collider colli;  
    private sniperWalk walk;
    public AudioSource firing;
    public AudioClip abc;
    private GameManager manager;
void Awake()
{
    manager=FindObjectOfType<GameManager>();
}
    

void Start()
{
    walk=GetComponent<sniperWalk>();
    Player=GameObject.FindWithTag("Player");
    handgun.SetActive(false);
    gun.SetActive(false);
    bulletCount=a;
}

    void OnAnimatorIK()
    {
        if(manager.Pause==false)
        {
        Player=GameObject.FindWithTag("Player");
        if(walk.health<=0)
        {
            gun.SetActive(false);
            handgun.SetActive(false);
            backgun.SetActive(false);
        }
        else
        {
            
        
        if(walk.follow==false)
        {
            handgun.SetActive(false);
            gun.SetActive(false);
            backgun.SetActive(true);
        }
        if(walk.follow==true)
        {
            anim.SetBool("shoot",false);
            handgun.SetActive(true);
            gun.SetActive(false);
            backgun.SetActive(false);

        }
        if(walk.shoot==true)
        {
            //gun.SetActive(false);
            //handgun.SetActive(true);
            //backgun.SetActive(false);
            anim.SetBool("shoot",true);
            
            gun.SetActive(true);
            gun.transform.LookAt(Player.transform.position+new Vector3(0,1.2F,0));
            handgun.SetActive(false);
            backgun.SetActive(false);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            anim.SetIKPosition(AvatarIKGoal.RightHand,Right.transform.position);
           
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand,Left.transform.position);
            
            if(gunDelay<=0 && reload<=0)
            {
           if(Physics.Raycast(gun.transform.position,gun.transform.forward,out hit,200))
           {
            MF_AutoPool.Spawn(bullet,hit.point-gun.transform.forward*3,gun.transform.rotation);
           }
           firing.PlayOneShot(abc,1F);
            gunDelay=0.1F;
            bulletCount--;
            }
            if(bulletCount<=0)
            {
                reload=3;
                
            }
            if(gunDelay>0)
            {
                gunDelay-=Time.deltaTime;
            }
            if(reload>0)
            {
                reload-=Time.deltaTime;
                bulletCount=a;
            }
            
        }
        }
        }
    }
}
