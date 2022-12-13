using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class Bullet : MonoBehaviour
    {
        public GameObject HitEffect;
        private GameObject _enemy;

        private void OnCollisionEnter2D(Collision2D collider)
        {
        
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
            if (hit.collider != null)
            {
                GameObject effect = Instantiate(HitEffect, hit.point, Quaternion.identity);
                Destroy(effect, 0.5f);
            }
        
            if (collider.collider.CompareTag("Enemy"))
            {
                // Get Enemy GameObject
                _enemy = collider.gameObject;
                _enemy.GetComponent<Enemy>().TakeDamage(10f);
                Debug.Log(_enemy.name + " was hit!" + " Health: " + _enemy.GetComponent<Enemy>().Health);
            }

            if (collider.collider.CompareTag("Tilemap"))
            {
                Debug.Log("Hit Tilemap Edge Collider");
            
            }
        
            Destroy(this.gameObject);
        }

    }
}