using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    [SerializeField]
    private string tagName;
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private GameObject x;
    // Start is called before the first frame update
    void OnTriggerStay(Collider a)
    {
        if(a.gameObject.tag==tagName)
        {
            x.transform.position=destination;
        }
    }
}
