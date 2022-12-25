using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelStorage : MonoBehaviour
{
    public static int currentLevel,yourScore,myScore;
    public float normal,shoot,scoped,vol;
    //public static float stanormal,stashoot,stascoped;

    public Slider norm,shot,scop,volu;
    public int don,storedValue;
    public Animator anime;
    private Vector3 distance;
    private GameObject player;
    public Text level,score,scores;
    
    void OnEnable()
    {
        if(norm)
        {
            levelData data=SaveManager.StoredItem();
            if(data!=null)
            {
            norm.value=data.normal;
            shot.value=data.zoom;
            scop.value=data.scoped;
            //if(data.volume!=null)
            volu.value=data.volume;
            storedValue=data.store;
            }
            
            else
            {
                Default();
            }
        }
    }
    void Start()
    {
        if(FindObjectOfType<GameManager>())
        {
        currentLevel=FindObjectOfType<GameManager>().level;
        if(currentLevel==6)
        {
            this.gameObject.SetActive(false);
        }
        if(currentLevel!=0)
        {
        levelData data=SaveManager.StoredItem();
        if(data!=null)
        {
        normal=data.normal;
        shoot=data.zoom;
        scoped=data.scoped;
        vol=data.volume;
        //Debug.Log(normal+","+shoot+","+scoped);
        }

       // norm.value=.5F;
        //shot.value=.35F;
        //scop.value=.04F;
        }
        }
        if(level)
        {
            if(currentLevel==2)
            {
                level.text= "Survival";
            }
            else
            {
                int h=currentLevel-2;
            level.text="Level "+h;    
            }
            score.text="Your Score="+yourScore+" kills,";
            if(currentLevel!=4 && currentLevel!=6 && currentLevel!=7)
            {
            scores.text="Helicopter Destroyed="+myScore+" ,";
            }
            else
            {
                scores.text=" ";
            }        
        }
        
        
    }
    void Update()
    {
         if(FindObjectOfType<GameManager>())
        {
        yourScore=FindObjectOfType<GameManager>().Deaths;
        myScore=FindObjectOfType<GameManager>().Destrys;
        }
        don=currentLevel;
        if(anime!=null)
        {
        player=GameObject.FindWithTag("Player");
        distance=transform.position-player.transform.position;
        if(distance.magnitude>30)
        {
            anime.enabled=false;
        }
        if(distance.magnitude<=30)
        {
            anime.enabled=true;
        }
        }
    }


    // Update is called once per frame
    public void Yes()
    {
        SceneManager.LoadScene(currentLevel);
    }

    // Update is called once per frame
    public void No()
    {
        SceneManager.LoadScene(0);
    }
    void OnTriggerEnter()
    {

        if(storedValue+1>currentLevel)
        {
            don=storedValue+1;
        }
        SaveManager.Save(this);
        if(FindObjectOfType<GameManager>())
        {
            FindObjectOfType<GameManager>().levelUP();
        }
        
    }
    public void Reset()
    {
        currentLevel=0;
        don=2;
        normal=norm.value;
        shoot=shot.value;
        scoped=scop.value;
        vol=volu.value;
        SaveManager.Save(this);
    }
   /* public void normSens(float a)
    {
        normal=a;
    }
    public void shotSense(float a)
    {
        shoot=a;
    }
    public void zomSense(float a)
    {
        scoped=a;
    }*/
    public void Default()
    {
        norm.value=.5F;
        shot.value=.34F;
        scop.value=.04F;
        volu.value=0.5F;
    }
    public void Save()
    {
        normal=norm.value;
        shoot=shot.value;
        scoped=scop.value;
        vol=volu.value;
        don=storedValue+1;
        SaveManager.Save(this);
        
    }

}
