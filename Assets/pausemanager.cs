using System.Collections;
using TMPro;
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
    public TMP_Text level;
    public int totalCoins = 0;
    public int collectedCoins = 0;
    public static pausemanager Instance;
    public TMP_Text coin;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("shade").GetComponent<Animator>();
        level.text = "lvl: " + SceneManager.GetActiveScene().buildIndex.ToString();
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
        coin.text = "coins: " + GetCoinText();
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
        if (pause == false)
        {
            pause = true;
        }
        if(pause == true)
        {
            pause = false;
        }
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
    public void RegisterCoin(bool alreadyCollected)
    {
        totalCoins++;

        if (alreadyCollected)
            collectedCoins++;
    }

    public void CollectCoin()
    {
        if (collectedCoins < totalCoins)
        {
            collectedCoins++;
        }
    }

    public string GetCoinText()
    {
        return collectedCoins + " / " + totalCoins;
    }

}
