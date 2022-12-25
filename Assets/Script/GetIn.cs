using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIn : MonoBehaviour
{
    
    Car car;
    public float swim,boomTime=0;
    private float  black=15,drawntime=10;
    public GameObject CarControl,carcam,fire,carrcam,heli;
    public GameObject player, sphere,canvas,enter;
    public Player x;
    public GameObject camu;
    private Camera cam;
    private bool inside,dead=false,playsound=false;
    public FixedButton xirbool,niski;
    public Renderer Rend;
    public Rigidbody Rig;
    public float health;
    public AudioSource Idle,explode;
    public AudioClip Boom;
    private GameManager manager;
    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag=="Bullet" || c.gameObject.tag=="Bullet0")
        {
            health--;
        }
        if(c.gameObject.tag=="BigBullet2"|| c.gameObject.tag=="BigBullet" )
        {
            health-=4;
        }
        if(c.gameObject.tag=="sniper")
        {
            health-=4;
        }
    }
    void Awake()
    {
        carrcam.transform.localRotation=Quaternion.Euler(0,0,0);
        carrcam.SetActive(false);
        manager=FindObjectOfType<GameManager>();
        cam=Camera.main;
        x=FindObjectOfType<Player>();

    }

    // Start is called before the first frame update
    void OnTriggerStay(Collider a)
    {
        if(this.enabled==true)
        {
        if(a.gameObject.tag=="Player" && manager.scoped==false)
        {
            enter.SetActive(true);
            if(inside==false && (Input.GetKey(KeyCode.F)||xirbool.Pressed==true))
            {
            carrcam.SetActive(true);
            car.enabled=true;
            a.gameObject.transform.parent=transform;
            cam.transform.parent=carcam.transform;
            cam.transform.position=carcam.transform.position;
            cam.transform.rotation=carcam.transform.rotation;
            cam.farClipPlane=160F;
            player.transform.localScale=new Vector3(1,1,1);
            player.SetActive(false);
            sphere.SetActive(false);
            inside=true;
            manager.vehicleEnter();
            xirbool.Pressed=false;
            CarControl.SetActive(true);
            canvas.SetActive(false);
            enter.SetActive(false);
            transform.gameObject.tag="Player";
            playsound=true;
            if(x.croach==true)
            {
                x.sit();
            }
            if(heli!=null)
            {
                heli.SetActive(true);
            }
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
        car=GetComponent<Car>();
        car.enabled=false;
        fire.SetActive(false);
        inside=false;
//        c=1;

    }

    // Update is called once per frame
    void Update()
    {
        Idle.enabled=playsound;
        if(manager.Pause==true)
        {
            Idle.enabled=false;
        }
        if(health<=0)
        {
            if(dead==false && transform.position.y>swim)
            {
                fire.SetActive(true);
                fire.transform.rotation=Quaternion.Euler(0,fire.transform.rotation.eulerAngles.y,0);
            }
            if(transform.position.y<=swim)
            {
                fire.SetActive(false);
            }
            //Instantiate(fire,transform.position+new Vector3(0,1F,0)+transform.forward*2,transform.rotation);
            if(black<=0)
            {
            dead=true;
            }
            else
            {
                black-=Time.deltaTime;
            }
        }
        if(dead==true)
        {
            Rend.material.color=Color.black;
            
            if(boomTime<=0)
            {
            explode.PlayOneShot(Boom,1);
            boomTime++;
            }
            fire.SetActive(false);
           
            if(inside==false)
            {
                Destroy(gameObject, 10);
            }
            if(inside==true)
            {
                //game over
                manager.gameOver();
            }
        }
        if(car.right.Pressed==false && car.left.Pressed==false)
        {
        if(inside==true && (Input.GetKey(KeyCode.X)||niski.Pressed==true))
        {
            
            CarControl.SetActive(false);
            player.SetActive(true);
            player.transform.localScale=new Vector3(1F,1F,1F);
            player.transform.parent=null;
            player.transform.rotation=Quaternion.Euler(0,player.transform.rotation.eulerAngles.y,0);
            sphere.SetActive(true);
            cam.transform.parent=camu.transform;
            cam.transform.position=camu.transform.position;
            cam.transform.rotation=camu.transform.rotation;
            cam.farClipPlane=100F;
            car.enabled=false;
            inside=false;
            manager.vehicleExit();
            canvas.SetActive(true);
            niski.Pressed=false;
            transform.gameObject.tag="Vehicle";
            playsound=false;
            carrcam.SetActive(false);
        }
        }
        if(transform.position.y<=swim )
        {
            //transform.position+=new Vector3(0,0.05F,0);
             if(drawntime<=0 && inside==true)
            {
                
                manager.gameOver();
            }
            if(drawntime>0)
            {
                drawntime-=Time.deltaTime;
            }
             if(inside==false)
            {

                Destroy(gameObject,10);
            }
            if(transform.position.y<=swim-3)
            {
                transform.position+=new Vector3(0,.3F,0);
                transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,transform.rotation.eulerAngles.y,0),.6F);
                Rig.useGravity=false;
            Rig.constraints=RigidbodyConstraints.FreezePositionX;  
            Rig.constraints=RigidbodyConstraints.FreezeRotationZ;  
            Rig.constraints=RigidbodyConstraints.FreezePositionZ;
            Rig.constraints=RigidbodyConstraints.FreezeRotationX;
            Rig.constraints=RigidbodyConstraints.FreezeRotationY;
            Rig.constraints=RigidbodyConstraints.FreezePositionY;
            }
        }
    }
    
  
}
