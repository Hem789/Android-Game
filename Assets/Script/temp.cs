using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    private ArmyHelicopter jpt;
    [SerializeField]
    private string a;
    // Start is called before the first frame update
    void Awake()
    {
        jpt=GetComponent<ArmyHelicopter>();
    }
    void OnEnable()
    {
        jpt.enabled=true;
        this.gameObject.tag=a;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jpt.Player==null)
        {
            jpt.Player=GameObject.FindWithTag("Player");
        }
    }
}
