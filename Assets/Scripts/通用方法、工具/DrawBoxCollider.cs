using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoxCollider : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;

    private void OnValidate()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if (boxCollider2D != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(boxCollider2D.transform.position + (Vector3)boxCollider2D.offset, boxCollider2D.size);
        }
    }
}
