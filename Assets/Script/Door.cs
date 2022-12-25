using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Action,move;
    public FixedButton Enter;
    public AudioSource sound;
    //public float Maxangle,minAngle;
    private GameManager manager;
    private bool open=false;
    public AudioClip clip;
    // Start is called before the first frame update
    void OnTriggerStay(Collider a)
    {
        if(a.gameObject.tag=="Player" && manager.outside==true)
        {
            Action.SetActive(true);
        }
        if(a.gameObject.tag=="Vehicle")
        {
            Action.SetActive(false);
        }
    }
    void OnTriggerExit(Collider a)
    {
        if(a.gameObject.tag=="Player")
        {
            Action.SetActive(false);
        }
    }
    void Awake()
    {
        manager=FindObjectOfType<GameManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(manager.outside==false || manager.follow==false)
        {
            Action.SetActive(false);
        }
        if(manager.Pause==true)
        {
           sound.enabled=false;
        }
        if(Enter.Pressed)
        {
            open=!open;
            Enter.Pressed=false;
            Action.SetActive(false);
            sound.enabled=true;
            sound.PlayOneShot(clip,1);
        }
        if(open==false && move.transform.localRotation.eulerAngles.y>1)
        {
            move.transform.Rotate(0,-2F,0);
        }
        if(open==true && move.transform.localRotation.eulerAngles.y<70)
        {
            move.transform.Rotate(0,2F,0);
        }
        

    }
}
