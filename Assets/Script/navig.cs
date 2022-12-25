using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navig : MonoBehaviour
{
[SerializeField]
private Transform target;
private GameObject player;

private Vector3 angle;
private Quaternion rot1,rot2;
private float differenceAngle;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Start()
    {
    
        player=Camera.main.gameObject;
        //angle=target.transform.position-player.transform.position;
        //rot1=Quaternion.LookRotation(new Vector3(angle.x,0,angle.z));
        //transform.rotation=Quaternion.Euler(0,0,rot1.eulerAngles.y);
    }
    
    void Update()
    { 
        
    if(player!=null && target !=null)
    {
        angle=target.transform.position-player.transform.position;
        rot1=Quaternion.LookRotation(new Vector3(angle.x,0,angle.z));
        differenceAngle=rot1.eulerAngles.y-player.transform.rotation.eulerAngles.y;
        rot2=Quaternion.Euler(0,0,-differenceAngle);//*Quaternion.Euler(0,0,player.transform.rotation.eulerAngles.y);
        //Debug.Log("(0,"+differenceAngle+",0)");
        transform.rotation=Quaternion.Slerp(transform.rotation,rot2,1);
    }


        
    }
}
