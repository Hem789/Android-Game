using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject Action,move;
    public FixedButton Enter;
    public float Maxheight,minHeight;
    private float offset;
    private bool up=false,down=false,moving=false;
    public AudioSource lift;
    private GameManager manager;
    //public liftDoor bot,top;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider b)
    {
        if(b.transform.gameObject.tag=="Player" && manager.scoped==false && manager.outside==true)
        {
            Action.SetActive(true);
        } 
    }
    void OnTriggerStay(Collider c)
    {
       
        if(c.transform.gameObject.tag=="Player" && manager.scoped==false && manager.outside==true)
        {
            if(moving==false)
            {
            Action.SetActive(true);
            }
        }
        if(c.transform.gameObject.tag=="Vehicle")
        {
            Action.SetActive(false);
        }
        
    }
    void OnTriggerExit(Collider a)
    {
        if(a.gameObject.tag=="Player")
        {
            Action.SetActive(false);
        }
    }
    void Awake()
    {
     offset=(Maxheight-minHeight)/2; 
     manager=FindObjectOfType<GameManager>();  
     /*if(bot)
        {
            bot.enabled=false;
        }
        if(top)
        {
            top.enabled=false;
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(manager.outside==false|| manager.follow==false)
        {
            Action.SetActive(false);
        }
        if(manager.Pause==true)
        {
            lift.enabled=false;
        }
        if(Enter.Pressed && move.transform.position.y<Maxheight-offset)
        {
            lift.enabled=true;
            Action.SetActive(false);
            up=true;
            Enter.Pressed=false;
        }
        if(Enter.Pressed && move.transform.position.y>minHeight+offset)
        {   
            lift.enabled=true;
            down=true;
            Enter.Pressed=false;
        }
        //for doors
        
            

        
        if(up==true)
        {
            
            move.transform.position+=new Vector3(0,0.02F,0);
            Action.SetActive(false);
            moving=true;
            if(move.transform.position.y>=Maxheight)
            {
                moving=false;
                lift.enabled=false;
                up=false;
            }
        }
        if(down==true)
        {
            move.transform.position-=new Vector3(0,0.02F,0);
            Action.SetActive(false);
            moving=true;
            if(move.transform.position.y<=minHeight)
            {
                down=false;
                moving=false;
                lift.enabled=false;
            }
        }
        
        
    }
    void Update()
    {
        if(manager.Pause==true)
        {
            lift.enabled=false;
        }
    }
    /*void LateUpdate()
    {
        if(bot)
        {
        if(move.transform.position.y<Maxheight-1)
        {
            bot.enabled=true;
            if(top)
            {
                if(top.timer<=0)
                top.enabled=false;
            }
        }
        }
        if(top)
        {
        if(move.transform.position.y>minHeight+1)
        {
            top.enabled=true;
            if(bot)
            {
                if(bot.timer<=0)
                bot.enabled=false;
            }
        }
        }
    }*/
}
