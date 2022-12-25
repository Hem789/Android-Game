using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCamera : MonoBehaviour
{
    public float height, minHeight,currentHeight;
    private GameObject Player;
    
    public FixedTouchField touch;
    public FixedButton zoomIn,zoomOut;
    // Start is called before the first frame update
    public void position()
    {
        Player=GameObject.FindWithTag("Player");
        transform.position=new Vector3(Player.transform.position.x,height,Player.transform.position.z);
        currentHeight=height;
    }
    

    // Update is called once per frame
    void Update()
    {
        Player=GameObject.FindWithTag("Player");
        float z=touch.TouchDist.y*0.1F;
        float x=touch.TouchDist.x*0.1F;
        transform.position=new Vector3(transform.position.x,currentHeight,transform.position.z);
        transform.position+=new Vector3 (-x,0,-z);
        if(zoomIn.Pressed)
        {
            ZoomIn();
        }
        if(zoomOut.Pressed)
        {
            ZoomOut();
        }
    }
    void ZoomIn()
    {
        if(currentHeight>minHeight)
        {
        currentHeight-=1F;
        }
    }
    void ZoomOut()
    {
        if(currentHeight<height)
        {
        currentHeight+=1F;
        }
    }
}
