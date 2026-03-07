using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemanager : MonoBehaviour
{
    public bool pause = false;
    public GameObject pausemenu;
    public GameObject setting;
    public bool sttingopen = false;
    public Animator animator;
    public float transtime = 0.75f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("shade").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pause == true)
        {
            Time.timeScale = 0f;
            pausemenu.SetActive(true);
        }
        if (pause == false)
        {
            Time.timeScale = 1.0f;
            pausemenu.SetActive(false);
        }
        if (sttingopen == false)
        {
            setting.SetActive(false);
        }
        else
        {
            setting.SetActive(true);
        }
    }
    public void Pause()
    {
        pause = !pause;
    }
    public void quit()
    {
        Debug.Log("quitting");
        Time.timeScale = 1.0f;
        Application.Quit();
    }
    public void menu()
    {
        pause = !pause;
        StartCoroutine(loadlevel());
        
    }
    public void seting()
    {
        sttingopen = !sttingopen;
    }

    IEnumerator loadlevel()
    {
        animator.SetTrigger("start");
        yield return new WaitForSecondsRealtime(transtime);
        SceneManager.LoadScene(0);
    }
}
