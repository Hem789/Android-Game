using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyHelicopter : MonoBehaviour
{
    [SerializeField]
    private bool spaw, seenDependent;
    private RaycastHit hit,hit2,hitLeft,hitRight,hitback;
    public LayerMask mask;
    public CharacterController Controller;
    private string tagy;
    private Vector3 Direction,dist;
  //  [SerializeField]
    public GameObject Player;//,listener;
    [SerializeField]
    private string targetName;
    public GameObject fan,backfan, Left,Right,bullet,fire,effect1,effect2;
    [SerializeField]
    private float delay=0.1F,bulletCount=50, fireGap,decrease,Destr=15,seenCount=0;
    public float Speed,health;
    public AudioSource firing,flying;
    private GameManager manager;
    void OnTriggerEnter(Collider a)
    {
        if(a.gameObject.tag=="Bullet")
        {
            health--;
        }
        if(a.gameObject.tag=="BigBullet" || a.gameObject.tag=="BigBullet2")
        {
            health-=4;
        }
    }
    /*void OnTriggerStay(Collider a)
    {
        //while(true)
        //{
        if(a.gameObject.tag=="Player" || a.gameObject.tag=="Finish")
        {
        Vector3 x=new Vector3(transform.position.x,0,transform.position.z)-new Vector3(a.transform.position.x,0,a.transform.position.z);
        Controller.Move(x*10);
        }
        //}
    }*/
    
    // Start is called before the first frame update
    void Start()
    {
        //listener=Camera.main.gameObject;
        tagy=this.gameObject.tag;
    fire.SetActive(false);
    manager=FindObjectOfType<GameManager>();
    effect1.SetActive(false);
    effect2.SetActive(false);
    }
    void OnEnable()
    {
        if(tagy!=null)
        {
        this.gameObject.tag=tagy;
        }
        health=100;
        fire.SetActive(false);
        firing.enabled=false;
        delay=0.1F;
        bulletCount=50;
        seenCount=0;
        Destr=15;
        decrease=0;
    }
    void FixedUpdate()
    {
        if(this.gameObject.tag=="Player")
        {
            if(Physics.Raycast(transform.position,transform.right,out hitRight,40))
            {
            if(hitRight.transform.gameObject.tag== "Player")
            {
                Vector3 L=new Vector3(transform.position.x,0,transform.position.z)-new Vector3(hitRight.point.x,0,hitRight.point.z);
                L=L/L.magnitude;
                Controller.Move(L*Speed*3);
            }
            }
            if(Physics.Raycast(transform.position,-transform.right,out hitLeft,40))
            {
                if(hitLeft.transform.gameObject.tag=="Player")
            {
                Vector3 R=new Vector3(transform.position.x,0,transform.position.z)-new Vector3(hitLeft.point.x,0,hitLeft.point.z);
                R=R/R.magnitude;
                Controller.Move(R*Speed*3);
            }
            }
            if(Physics.Raycast(transform.position,-transform.forward,out hitback,40))
            {
                if(hitback.transform.gameObject.tag=="Player")
            {
                Vector3 B=new Vector3(transform.position.x,0,transform.position.z)-new Vector3(hitback.point.x,0,hitback.point.z);
                B=B/B.magnitude;
                Controller.Move(B*Speed*3);
            }
            }
        }
       // dist=transform.position-listener.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Player==null)
        {
            firing.enabled=false;
        }
        if(health<=0)
        {
            this.gameObject.tag="Untagged";
        }
        if(seenCount<16 && seenDependent==false)
        {
        seenCount+=Time.deltaTime;
        }
        
        if(manager.Pause==true)
        {
            flying.enabled=false;
            firing.enabled=false;
        }
        if(manager.Pause==false)
    {
        //if(dist.magnitude<110)
        //{
        flying.enabled=true;
        //}
        if(health>0)
        {
            //firing.enabled=false;
       
        fan.transform.Rotate(0,30,0);
        backfan.transform.Rotate(20,0,0);
        Player=GameObject.FindWithTag(targetName);
        if(Player==null)
        {
            if(this.gameObject.tag=="Finish")
            {
                targetName="Player";
                Player=GameObject.FindWithTag(targetName);
            }
        }
        if(Player!=null)
        {
        Direction=Player.transform.position-transform.position;
        if(Physics.Raycast(transform.position,Direction,out hit2,250,mask))
        {
            if(hit2.transform.gameObject.tag==targetName)
            {
                seenCount=0;
            }
        }
        if(transform.position.y<Player.transform.position.y && transform.position.y<33)
        {
            //transform.position+=new Vector3(0,0.1F,0);
        }
        if(transform.position.y<=15)
        {
            Direction.y=0;
        }
        if(Direction.magnitude<=2000 && Direction.magnitude>50)
        {
            Quaternion look=Quaternion.LookRotation(new Vector3(Direction.x,0,Direction.z));
            Quaternion look1=Quaternion.Euler(15,look.eulerAngles.y,look.eulerAngles.z);
            transform.rotation=Quaternion.Slerp(transform.rotation,look1,0.05F);
            Controller.Move(Direction*Time.deltaTime*Speed);
        }
        if(Direction.magnitude<=55)
        {
            Quaternion look=Quaternion.LookRotation(new Vector3(Direction.x,0,Direction.z));
            transform.rotation=Quaternion.Slerp(transform.rotation,look,1F);
            Right.transform.LookAt(Player.transform.position+new Vector3(0,1,0));
            Left.transform.LookAt(Player.transform.position+ new Vector3(0,1,0));
            if(delay<=0 && fireGap<=0 && seenCount<=15)
            {
            MF_AutoPool.Spawn(bullet,Left.transform.position,Left.transform.rotation);
            MF_AutoPool.Spawn(bullet,Right.transform.position,Right.transform.rotation);
            //f(dist.magnitude<100)
            //{
            firing.enabled=true;
            //}
            effect1.SetActive(true);
            effect2.SetActive(true);
           // MF_AutoPool.Spawn(effect,Left.transform.position-transform.forward*0.1F,Left.transform.rotation);
            //MF_AutoPool.Spawn(effect,Right.transform.position-transform.forward*0.1F,Right.transform.rotation);
            bulletCount--;
            delay=0.1F;
            }
            if(seenCount>15)
            {
                MF_AutoPool.Despawn(gameObject);
                //Quaternion look=Quaternion.LookRotation(new Vector3(Direction.x,0,Direction.z));
                //transform.rotation=Quaternion.Slerp(transform.rotation,look*Quaternion.Euler(0,180,0),1F);
                
            }
            
            if(delay>0)
            {
                delay-=Time.deltaTime;
            }
            if(bulletCount<=0)
            {
                effect1.SetActive(false);
                effect2.SetActive(false);
                fireGap=5;
                bulletCount=50;
            }
            if(fireGap>0)
            {
                firing.enabled=false;
                fireGap-=Time.deltaTime;
            }

        }
        }
        }
        else
        {
            
            if(decrease==0)
            {
                if(this.gameObject.tag!="Player")
                {
            manager.chopper();
                }
            //manager.enemy();
            decrease++;
            }
            if(Destr<=0)
            {
                // Destr=15;
                 fire.SetActive(false);
                // health=100;
                if(spaw==false)
                  MF_AutoPool.Despawn(gameObject);
                  else
                  this.gameObject.SetActive(false);
            }
            if(Destr>0)
            {
                Destr-=Time.deltaTime;
            
            //Destroy(gameObject,15);
            
            fire.SetActive(true);
            fire.transform.rotation=Quaternion.Euler(0,0,0);
            }
            
            firing.enabled=false;
                        //MF_AutoPool.Spawn(fire,fan.transform.position+transform.forward*2,transform.rotation);
            if(transform.position.y>-10)
            {
             if(Physics.Raycast(transform.position,new Vector3(0,-1,0),out hit,50))
             {   
            if(hit.transform.gameObject.tag!="Ground" || hit.distance>.5F)
            {
            fan.transform.Rotate(0,10,0);
            backfan.transform.Rotate(10,0,0);
            transform.position+=transform.forward* .1F-transform.up*0.1F+new Vector3(0,-.1F,0);
            transform.Rotate(-1,2.5F,0F);
            }
            }
            }
            else
            {
                flying.enabled=false;
                fire.SetActive(false);
            }

            
        }
    }    
    }
}
