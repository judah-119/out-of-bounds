using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public float floatAmplitude = 0.25f;
    public float floatSpeed = 3f;

    private Vector3 startPos;
    private float randomOffset;

    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    public GameObject liht;
    public BoxCollider2D boxCollider;
    public ParticleSystem burstEffect;

    public bool iscoll;

    public string coinID; 

    void Start()
    {

        coinID = "coin_" + transform.position.ToString() + SceneManager.GetActiveScene().buildIndex.ToString() + "pp";
        startPos = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2f);

        if (PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            iscoll = true;

            spriteRenderer.color = Color.gray;
        }
        pausemanager.Instance.RegisterCoin(iscoll);
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed + randomOffset) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
        if (iscoll)
        {
            spriteRenderer.color = Color.gray;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           


            PlayerPrefs.SetInt(coinID, 1);
            PlayerPrefs.Save();
            if (iscoll == false)
            {
                pausemanager.Instance.CollectCoin();
            }
            else
            {
                iscoll = true;
            }

            boxCollider.enabled = false;
            liht.SetActive(false);
            spriteRenderer.enabled = false;

            audioSource.Play();
            burstEffect.Play();

            Destroy(gameObject, audioSource.clip.length);
        }
    }

}