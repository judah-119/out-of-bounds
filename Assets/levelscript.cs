using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelscript : MonoBehaviour
{
    public int level;
    public  TMP_Text t;
    public Animator animator;
    public float transtime;
    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t.text = level.ToString();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        t.text = level.ToString();
    }
    public void click()
    {
       
        StartCoroutine(loadlevel(level));
    }
    IEnumerator loadlevel(int lvlindex)
    {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(transtime);
        SceneManager.LoadScene(lvlindex);
        Destroy(gameObject);
    }
}
