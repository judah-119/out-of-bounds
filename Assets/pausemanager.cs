using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemanager : MonoBehaviour
{
    public bool pause = false;
    public GameObject pausemenu;
    public GameObject setting;
    public bool sttingopen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        SceneManager.LoadScene(0);
        
    }
    public void seting()
    {
        sttingopen = !sttingopen;
    }
}
