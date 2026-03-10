using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;

    private void OnDrawGizmos()
    {
        if (boxCollider2D == null) return;

        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawWireCube(boxCollider2D.offset, boxCollider2D.size);
    }
}