using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatAmplitude = 0.25f;   // How high/low it moves
    public float floatSpeed = 3f;          // How fast it moves

    private Vector3 startPos;
    private float randomOffset;            // Random phase offset for unsynced movement

    void Start()
    {
        startPos = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2f); 
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed + randomOffset) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}