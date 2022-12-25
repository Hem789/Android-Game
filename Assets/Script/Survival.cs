using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survival : MonoBehaviour
{
    public GameObject tactical,sniper,player,helicopter,car,plus;//heli,enemyHeli, car,player;
    private float time=1,sniperTime=5, helitime=80,cartime=10,plustime=0;
    public float number,carNum;
    private RaycastHit hit;
    private GameManager manager;
//    private bool enemyHeli=false;
    // Start is called before the first frame update
    void Start()
    {
      manager=FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      player=GameObject.FindWithTag("Player");
      float x=Random.Range(-15,15);
      float y=Random.Range(-15,15);
      float z=Random.Range(-15,15);
      if(plustime<=0)
      {
        MF_AutoPool.Spawn(plus, new Vector3(341,.82F,153.78F),Quaternion.identity);
        plustime=30;
      }
      if(plustime>0)
      {
        plustime-=Time.deltaTime;
      }
      if(time<=0 )
      {  
        if(player.transform.position.y>-.5F && player.transform.position.y<10)
        {
          if(Physics.Raycast(player.transform.position+new Vector3(3*x,3,3*z),new Vector3(0,-1,0),out hit,10))
          {
            if(hit.transform.gameObject.tag=="Ground")
            {
            if((number-manager.Deaths)<=5)
            Instantiate(tactical,new Vector3(player.transform.position.x+2*x,hit.point.y+.5F,player.transform.position.z+2*z),player.transform.rotation* Quaternion.Euler(0,180,0));
            time=5;
            number++;
            }
          }
        }
      }
      if(sniperTime<=0)
      {
        if((number-manager.Deaths)<=5)
        Instantiate(sniper,new Vector3(413.4416F,10.63689F,167.5125F),Quaternion.Euler(0,0,0));
        sniperTime=180;
      }
        
      if(time>0)
      {
        time-=Time.deltaTime;
      }
      if(sniperTime>0)
      {
        sniperTime-=Time.deltaTime;
      }
      if(helitime<=0 )
      {
        if(player.transform.position.y>-.5F && player.transform.position.y<10 && manager.outside==true)
        {
          if(Physics.Raycast(player.transform.position-new Vector3(5*x,30,5*z),new Vector3(0,-1,0),out hit,35))
          {
            if(hit.point.y<1)
            {
            Instantiate(helicopter,new Vector3(player.transform.position.x-5*x, hit.point.y, player.transform.position.z-5*z), Quaternion.Euler(0,0,0));
            helitime=80;
            }
          }
        }
      }
      if(helitime>0)
      {
        helitime-=Time.deltaTime;
      }
      if(carNum<=3)
      {
      if(cartime<=0 )
      {
        if(player.transform.position.y>-.5F && player.transform.position.y<10 && manager.outside==true)
        {
          if(Physics.Raycast(player.transform.position+new Vector3(5*x,30,5*z),new Vector3(0,-1,0),out hit,35))
          {
            if(hit.point.y<1)
            {
            MF_AutoPool.Spawn(car,new Vector3(player.transform.position.x+5*x, hit.point.y, player.transform.position.z+5*z), Quaternion.Euler(0,0,0));
            cartime=45;
            carNum++;
            }
          }
        }
      }
      if(cartime>0)
      {
        cartime-=Time.deltaTime;
      }
      }
    }
    public void a()
    {
      number+=2;
    }
   
}
