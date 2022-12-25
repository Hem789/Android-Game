using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public GameObject LoadScreen;
    public Slider slide;
    private AudioSource voice;
    void Awake()
    {
        voice=FindObjectOfType<AudioSource>();
    }
    public void Play()
    {
        this.gameObject.SetActive(true);
        Load(2);
    }
    public void Level1()
    {
        this.gameObject.SetActive(true);
        Load(3);
    }
    public void Level2()
    {
       this.gameObject.SetActive(true);
        Load(4);
    }
    public void Level3()
    {
        this.gameObject.SetActive(true);
        Load(5);
    }
    public void Level4()
    {
        this.gameObject.SetActive(true);
        Load(6);
    }
    public void Level5()
    {
        this.gameObject.SetActive(true);
        Load(7);
    }
    public void Exit()
    {
        Application.Quit();
    }
    
    void Load(int i)
    {
        StartCoroutine(Loading(i));
        slide.value=0;
        voice.enabled=false;
    }
    IEnumerator Loading(int i)
    {
        AsyncOperation operation =SceneManager.LoadSceneAsync(i);
        LoadScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress/.9F);
            slide.value=progress;
            
            //percent.text=(progress*100)+"%";
            yield return null;
        }
    }
}
