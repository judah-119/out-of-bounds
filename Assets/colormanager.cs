using UnityEngine;

public class colormanager : MonoBehaviour
{
    public static colormanager Instance;
    public int color;
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
        color = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
