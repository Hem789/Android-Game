using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus : MonoBehaviour
{
    public GameObject effect;
    public Animator anime;
    private Vector3 distance;
    private GameObject player;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider a)
    {
        if (a.gameObject.tag=="Player")
        {
            FindObjectOfType<GameManager>().plus();
            MF_AutoPool.Spawn(effect,transform.position,transform.rotation);
            //Destroy(gameObject,.4F);
            //effect.SetActive(true);
            if(FindObjectOfType<GameManager>().level==2 || FindObjectOfType<GameManager>().level==6)
            {
             MF_AutoPool.Despawn(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player=GameObject.FindWithTag("Player");
        distance=transform.position-player.transform.position;
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
