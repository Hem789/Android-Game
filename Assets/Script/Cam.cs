using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField]
    private Joystick Joystick1, Joystick2;
    private Vector3 offset;
    public float min,max;
    [SerializeField]
    private float rotSpeed,joyrot,zoom,normal;
    public Transform pivot,body;
    public FixedTouchField touch;
    private GameManager manager;
    private Camera camu;



    
    void Awake()
    {
        offset=pivot.position-transform.position;
        manager=FindObjectOfType<GameManager>();
        camu=Camera.main;
        levelData data=SaveManager.StoredItem();
        if(data!=null)
        {
            normal=data.normal;
        joyrot=data.zoom;
        zoom=data.scoped;
        }
    }
    void OnEnable()
    {
        camu=Camera.main;
    }


   
    void LateUpdate()
    {
        //camu.transform.position=pivot.transform.position;
        //camu.transform.LookAt(pivot);
        
        if(manager.outside==true && manager.follow==false)
        {
            pivot.transform.position=new Vector3(body.position.x,body.position.y+1.5F,body.position.z);
        }
        if(manager.scoped==true)
        {
            rotSpeed=zoom;
        }
        else
        {
            rotSpeed=normal;
        }
    //}
    // Update is called once per frame
    //void LateUpdate()
    //{
        if(manager.Pause==false)
    {
        //float x=Input.GetAxis("Mouse X")*rotSpeed;
        float x=touch.TouchDist.x * rotSpeed;
        if(manager.scoped==false)
        {
        x+=Joystick1.Horizontal*joyrot+Joystick2.Horizontal*.5F;
        }
        pivot.Rotate(0,x,0);
        //float y=Input.GetAxis("Mouse Y")*rotSpeed;
        float y=touch.TouchDist.y * rotSpeed;
        if(manager.scoped==false)
        {
        y+=(Joystick1.Vertical+Joystick2.Vertical)*joyrot;
        }
        pivot.Rotate(-y,0,0);
    
        if(pivot.rotation.eulerAngles.x>=max && pivot.rotation.eulerAngles.x<=180F)
        {
            pivot.rotation=Quaternion.Euler(max,pivot.rotation.eulerAngles.y,0);
        }
        if(pivot.rotation.eulerAngles.x>180F && pivot.rotation.eulerAngles.x<=360+min)
        {
           pivot.rotation=Quaternion.Euler(360+min,pivot.rotation.eulerAngles.y,0); 
        }
        pivot.rotation=Quaternion.Euler(pivot.rotation.eulerAngles.x,pivot.rotation.eulerAngles.y,0);

        float a=pivot.eulerAngles.x;
        float b=pivot.eulerAngles.y;
        if(manager.croach==false)
        {
        Quaternion axis=Quaternion.Euler(a,b,0);
        transform.position=pivot.position-(axis*offset);
        }
        if(manager.croach==true)
        {
        Quaternion axis=Quaternion.Euler(a,b-45,0);
        transform.position=pivot.position-(axis*offset); 
        }
        transform.LookAt(pivot);
    }
    }
    
    
    
  
} 
