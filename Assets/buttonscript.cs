using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DoorData
{
    public GameObject door;
    public Vector3 openOffset;

    [HideInInspector]
    public Vector3 closedPosition;
}

public class buttonscript : MonoBehaviour
{
    public bool ispressed;
    public SpriteRenderer spriteRenderer;

    public Sprite sprite;  
    public Sprite sprite1;  

    public float moveSpeed = 2f;

    public List<DoorData> doors = new List<DoorData>();

    void Start()
    {

        foreach (DoorData d in doors)
        {
            if (d.door != null)
                d.closedPosition = d.door.transform.position;
        }
    }

    void Update()
    {
        spriteRenderer.sprite = ispressed ? sprite : sprite1;


        foreach (DoorData d in doors)
        {
            if (d.door == null) continue;

            Vector3 targetPosition = ispressed
                ? d.closedPosition + d.openOffset
                : d.closedPosition;

            d.door.transform.position = Vector3.Lerp(
                d.door.transform.position,
                targetPosition,
                Time.deltaTime * moveSpeed
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

            ispressed = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        ispressed = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            ispressed = false;
    }
}