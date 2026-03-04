using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menumanagerscript : MonoBehaviour
{
    public GameObject select;
    bool selectopen = false;
    public int level;
    public GameObject pannel1;
    public GameObject pannel2;
    public bool which;
    public GameObject setting;
    public bool sttingopen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectopen == false)
        {
            select.SetActive(false);
        }
        else
        {
            select.SetActive(true);
        }
        if (sttingopen == false)
        {
            setting.SetActive(false);
        }
        else
        {
            setting.SetActive(true);
        }
        if (which == false)
        {
            pannel1.SetActive(false);
            pannel2.SetActive(true);
        }
        else
        {
            pannel1.SetActive(true);
            pannel2.SetActive(false);
        }
    }
    public void seleect()
    {
        selectopen = !selectopen;
    }
    public void seting()
    {
        sttingopen = !sttingopen;
    }
    public void quit()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
    public void makelevel()
    {
        SceneManager.LoadScene(level);
    }
    public void Move()
    {
        which = !which;
    }

}
