using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endlevelscript : MonoBehaviour
{
    public float floatAmplitude = 0.25f;   
    public float floatSpeed = 3f;          

    private Vector3 startPos;
    private float randomOffset;            
    public Animator animator;
    public float transtime;
    public AudioSource audioSource;
    public BoxCollider2D boxCollider;
    public GameObject player;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem burstEffect;
    public GameObject lightss;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
            burstEffect.Play();
            boxCollider.enabled = false;
            audioSource.Play();
            nextlevel();
        }
    }
    public void nextlevel()
    {
        StartCoroutine(loadlevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator loadlevel(int lvlindex)
    {
        lightss.SetActive(false);
        player.tag = "Untagged";
        spriteRenderer.enabled = false;
        animator.SetTrigger("start");
        yield return new WaitForSeconds(transtime);
        SceneManager.LoadScene(lvlindex);
        Destroy(gameObject);
    }
}