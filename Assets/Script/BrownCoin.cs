using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownCoin : MonoBehaviour
{
    public GameObject effect;
    public Animator anime;
    private Vector3 distance;
    private GameObject player;
    //private float destime=180;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider a)
    {
        if (a.gameObject.tag=="Player")
        {
            FindObjectOfType<GameManager>().browncoin();
            MF_AutoPool.Spawn(effect,transform.position,transform.rotation);
            //effect.SetActive(true);
            if(FindObjectOfType<GameManager>().level==2 )
            {
            MF_AutoPool.Despawn(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
    /*void OnEnable()
    {
        destime=180;
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        player=GameObject.FindWithTag("Player");
        distance=transform.position-player.transform.position;
        /*if(destime<=0)
        {
            MF_AutoPool.Despawn(gameObject);
        }
        if(destime>0)
        {
            destime-=Time.deltaTime;
        }*/
        if(distance.magnitude>30)
        {
            anime.enabled=false;
        }
        if(distance.magnitude<=30)
        {
            anime.enabled=true;
        }
    }
}
