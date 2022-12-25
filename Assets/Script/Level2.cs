using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    private float time=1,plustime=0;
    public GameObject tactical,plus,player,coin;
    private GameManager manager;
    [SerializeField]
    private navig compass;
    private RaycastHit hit;
    [SerializeField]
    private float number, kills;
    // Start is called before the first frame update
    void Start()
    {
        manager=FindObjectOfType<GameManager>();
        compass.gameObject.SetActive(false);
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
        MF_AutoPool.Spawn(plus, new Vector3(303.7F,13.5F,163.3F),Quaternion.identity);
        plustime=20;
      }
      if(plustime>0)
      {
        plustime-=Time.deltaTime;
      }
      if(time<=0 )
      {  
        if(player.transform.position.y>-13F && player.transform.position.y<20)
        {
          if(Physics.Raycast(player.transform.position+new Vector3(3*x,20,3*z),new Vector3(0,-1,0),out hit,30))
          {
            if(hit.transform.gameObject.tag=="Ground")
            {
            if((number-manager.Deaths)<=5)
            {
              Instantiate(tactical,new Vector3(player.transform.position.x+2*x,hit.point.y,player.transform.position.z+2*z),player.transform.rotation* Quaternion.Euler(0,180,0));
              number++;
            }
            time=15;
            }
          }
        }
      }
      if(time>0)
      {
        time-=Time.deltaTime;
      }
      if(manager.Deaths>=kills)
      {
        compass.gameObject.SetActive(true);
          coin.SetActive(true);
      }
    }
}
