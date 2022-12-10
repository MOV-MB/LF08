using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject HitEffect;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        if (hit.collider != null)
        {
            GameObject effect = Instantiate(HitEffect, hit.point, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
        
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit" + collider.name);
            Destroy(collider.gameObject);
        }

        if (collider.CompareTag("Tilemap"))
        {
            Debug.Log("Hit Tilemap Edge Collider");
            
        }
        
        Destroy(this.gameObject);
    }

}
