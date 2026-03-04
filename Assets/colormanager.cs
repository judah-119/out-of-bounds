using UnityEngine;

public class colormanager : MonoBehaviour
{
    public static colormanager Instance;
    public int color;
    public int whichcolor;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
