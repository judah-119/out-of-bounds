using UnityEngine;
using UnityEngine.SceneManagement;

public class poopoo : MonoBehaviour
{
    public static poopoo instance;

    public int level;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);


        level = PlayerPrefs.GetInt("Level", 1);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 > level && level > 0)
        {
            level = SceneManager.GetActiveScene().buildIndex;
        }


        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
    }
}