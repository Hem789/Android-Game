using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cops : MonoBehaviour
{
    public Transform t1,t2,t3,t4,t5,t6,target;
    public GameObject coin;
    public Animator anime;
    private Vector3 moveDirection;
    private bool active=false;
    private int health=100;
    private Collider colli;
    public GetIn a;
    // Start is called before the first frame update
    void OnTriggerStay(Collider a)
    {
        if(a.gameObject.tag=="Player")
        {
            active=true;
            anime.enabled=true;
        }
        
    }
    void OnCollisionEnter(Collision a)
    {
        if(a.gameObject.tag=="Bullet")
        {
            health--;
        }
        if(a.gameObject.tag=="Bullet0")
        {
            health--;
        }
        if(a.gameObject.tag=="BigBullet")
        {
            health-=5;
        }
        if(a.gameObject.tag=="BigBullet2")
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
        colli=GetComponent<Collider>();
        target.position=t1.position;
        t1.transform.parent=null;
        t2.transform.parent=null;
        t3.transform.parent=null;
        t4.transform.parent=null;
        t5.transform.parent=null;
        t6.transform.parent=null;
        target.transform.parent=null;
        anime.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
        {
            colli.enabled=false;
            anime.enabled=false;
            FindObjectOfType<GameManager>().gameOver();
        }
        if(active==true)
        {
        if(target.position==t6.position && moveDirection.magnitude<=0.2F) 
        {
            if(a)
            {
            a.enabled=true;
            }
            if(coin)
            {
            coin.SetActive(true);
            }
            t1.transform.parent=transform;
            t2.transform.parent=transform;
            t3.transform.parent=transform;
            t4.transform.parent=transform;
            t5.transform.parent=transform;
            t6.transform.parent=transform;
            Destroy(gameObject);
            //target.position=t1.position;
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
    }
}
