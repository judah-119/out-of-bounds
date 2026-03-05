using UnityEngine;

public class colormanager : MonoBehaviour
{
    public static colormanager Instance;
    public int color;
    public int whichcolor = 4;
    public GameObject pannel1;
    public GameObject pannel2;
    public GameObject pannel3;
    public GameObject pannel4;
    public GameObject pannel5;

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

       
        whichcolor = PlayerPrefs.GetInt("whichcolor", 4); 
    }

    void Start()
    {
        if (whichcolor == 0)
        {
            color = 1;
        }
        if (whichcolor == 1)
        {
            color = 2;
        }
        if (whichcolor == 2)
        {
            color = 3;
        }
        if (whichcolor == 3)
        {
            color = 4;
        }
        if (whichcolor == 4)
        {
            color = Random.Range(1, 5);
        }
    }

    void Update()
    {
        if (whichcolor == 0)
        {
            color = 1;
            pannel1.SetActive(true);
            pannel2.SetActive(false);
            pannel3.SetActive(false);
            pannel4.SetActive(false);
            pannel5.SetActive(false);
        }
        if (whichcolor == 1)
        {
            color = 2;
            pannel1.SetActive(false);
            pannel2.SetActive(true);
            pannel3.SetActive(false);
            pannel4.SetActive(false);
            pannel5.SetActive(false);
        }
        if (whichcolor == 2)
        {
            color = 3;
            pannel1.SetActive(false);
            pannel2.SetActive(false);
            pannel3.SetActive(true);
            pannel4.SetActive(false);
            pannel5.SetActive(false);
        }
        if (whichcolor == 3)
        {
            color = 4;
            pannel1.SetActive(false);
            pannel2.SetActive(false);
            pannel3.SetActive(false);
            pannel4.SetActive(true);
            pannel5.SetActive(false);
        }
        if (whichcolor == 4)
        {
            pannel1.SetActive(false);
            pannel2.SetActive(false);
            pannel3.SetActive(false);
            pannel4.SetActive(false);
            pannel5.SetActive(true);
        }
    }

    public void more()
    {
        whichcolor++;
        PlayerPrefs.SetInt("whichcolor", whichcolor); 
        PlayerPrefs.Save();
    }

    public void less()
    {
        whichcolor--;
        PlayerPrefs.SetInt("whichcolor", whichcolor); 
        PlayerPrefs.Save();
    }
}