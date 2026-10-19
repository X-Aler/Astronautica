using UnityEngine;
using System.Collections.Generic;

public class AttackHitbox : MonoBehaviour
{
    public bool inHitBox = false;

    public string compareTag;

    public List<GameObject> targets;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(compareTag) && !targets.Contains(collision.gameObject))
        {
            targets.Add(collision.gameObject);
            inHitBox = targets.Count > 0;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(compareTag))
        {
            targets.Remove(collision.gameObject);
            inHitBox = targets.Count > 0;
        }
    }
}