using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liftDoor : MonoBehaviour
{
    private Animator anime;
    public float timer=5,time=1,distance;
    private bool open=false;
    private GameManager manager;
    private Vector3 dist;
    private GameObject player;

    public AudioSource sound;
    void OnTriggerStay(Collider a)
    {
        //if(anime)
        //{
        
        if((a.transform.gameObject.tag=="Player" || a.transform.gameObject.tag=="enemy") && manager.outside==true && open==false)
        {
        anime.SetBool("open",true);
        anime.SetBool("close",false);
        open=true;
        sound.enabled=true;
        timer=5;
        time=1;
        }
        //}
        
    }
    /*void OnTriggerEnter(Collider a)
    {
        //if(anime)
        //{
        
        if((a.transform.gameObject.tag=="Player" || a.transform.gameObject.tag=="enemy") && manager.outside==true && open==false)
        {
        anime.SetBool("open",true);
        anime.SetBool("close",false);
        open=true;
        sound.enabled=true;
        timer=5;
        time=1;
        }
        //}
    }*/
    
    // Start is called before the first frame update
    void Awake()
    {
        anime=GetComponent<Animator>();
        sound.enabled=false;
        manager=FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //anime.SetBool("open",false);
        //sound.enabled=false;
        
        //anime.SetBool("close",false);
        //sound.enabled=true;
        if(timer<=0 && open==true)
        {
        
        anime.SetBool("close",true);
        anime.SetBool("open",false);
        time=1.5F;
        if(anime.enabled==true)
        {
        sound.enabled=true;
        }
        open=false;
        }
        if(timer>0)
        {
            timer-=Time.deltaTime;
        }
        if(time>0)
        {
            time-=Time.deltaTime;
        }
        if(time<=0)
        {
            sound.enabled=false;
            
        }
        
        
        
        

    }
    void Update()
        {
            if(manager.Pause==true)
            sound.enabled=false;
            player=GameObject.FindWithTag("Player");
        dist=player.transform.position-transform.position;
        distance=dist.magnitude;
        if(dist.magnitude>=20)
        {
            anime.enabled=false;
        }
        if(dist.magnitude<20)
        {anime.enabled=true;
        }
        }
}
