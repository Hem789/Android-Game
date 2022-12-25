using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField]
    private GameObject[] a;
    
    // Start is called before the first frame update
    void OnTriggerStay(Collider x)
    {
        if(x.gameObject.tag=="Player")
        {
            foreach (var items in a)
            {
                if(items)
                items.SetActive(true);
            }
        }
    }

    void Start()
    {
        foreach(var items in a)
        {
            if(items)
            items.SetActive(false);
        }
    }
}