using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    public GameObject effect;
    private float desTime=20;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider a)
    {
        if (a.gameObject.tag=="Player")
        {
            FindObjectOfType<GameManager>().goldcoin();
            MF_AutoPool.Spawn(effect,transform.position,transform.rotation);
            //effect.SetActive(true);
            if(FindObjectOfType<GameManager>().level==2 || FindObjectOfType<GameManager>().level==4)
            {
            MF_AutoPool.Despawn(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
    void OnEnable()
    {
        desTime=20;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (desTime<=0)
        {
            MF_AutoPool.Despawn(gameObject);
        }
        else
        {
            desTime-=Time.deltaTime;
        }
    }
}
