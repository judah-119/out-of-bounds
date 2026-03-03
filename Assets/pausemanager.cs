using UnityEngine;

public class pausemanager : MonoBehaviour
{
    public bool pause = false;
    public GameObject pausemenu;

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
    }
    public void Pause()
    {
        pause = !pause;
    }
}
