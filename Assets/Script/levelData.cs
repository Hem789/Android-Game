using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class levelData //: MonoBehaviour
{
    // Start is called before the first frame updat
    public int store;
    public float normal,zoom,scoped, volume;
    public bool Level2,Level3,Level4,Level5;
    public levelData(levelStorage a)
    {
        if(store<a.don-1)
        {
        store=a.don-1;
        }
       
        normal=a.normal;
        zoom=a.shoot;
        scoped=a.scoped;
        volume=a.vol;
        
       // Debug.Log(normal+","+ zoom+ "," +scoped);
        if(store>=2)
        {
            Level2=true;
        }
        if(store>=3)
        {
            Level3=true;
        }
        if(store>=4)
        {
            Level4=true;
        }
        if(store>=5)
        {
            Level5=true;
        }
    }

    // Update is called once per frame

}
