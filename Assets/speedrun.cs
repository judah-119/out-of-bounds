using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class speedrun : MonoBehaviour
{
    public static speedrun instance;
    public float timer = 0f;
    public TMP_Text text;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            timer += Time.deltaTime;
        }
        float time = timer;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        text.text = string.Format("{0:00}:{1:00}.{2:000}",
            minutes, seconds, milliseconds);
    }
}
