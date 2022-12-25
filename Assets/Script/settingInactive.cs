using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingInactive : MonoBehaviour
{
    //public float timer;
    public FixedButton abc;
    private GameManager manager;
    void Awake()
    {
        manager=FindObjectOfType<GameManager>();
    }
    void Start()
    {
        manager.pause();
        Time.timeScale=1;
    }
    void Update()
    {
        //timer-=Time.deltaTime;
        if(abc.Pressed)
        {
            manager.play();
            this.gameObject.SetActive(false);

        }
        //StartCoroutine(waiting(timer));
        
    }
   /*IEnumerator waiting(float x)
    {
        yield return new WaitForSeconds(x);
        manager.play();
        this.gameObject.SetActive(false);
    }*/
    
}
