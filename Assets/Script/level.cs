using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour
{
    public int show;
    public GameObject level2,level3,level4,level5;
    // Start is called before the first frame update
    
    // Update is called once per frame
    public void load()
    {
        levelData data=SaveManager.StoredItem();
        show=data.store;
        
        if(show>=2)
        {
            level2.SetActive(data.Level2);
            level3.SetActive(data.Level3);
            level4.SetActive(data.Level4);
            level5.SetActive(data.Level5);
        }
        if(show<2)
        {
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(false);
            level5.SetActive(false);
        }
        
       
    }
}
