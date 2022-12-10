using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Bullet : MonoBehaviour
    {
        public GameObject HitEffect;
        private GameObject _enemy;

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
                // Get Enemy GameObject
                _enemy = collider.gameObject;
                _enemy.GetComponent<Enemy>().TakeDamage(10f);
                Debug.Log(_enemy.name + " was hit!" + " Health: " + _enemy.GetComponent<Enemy>().Health);
            }

            if (collider.CompareTag("Tilemap"))
            {
                Debug.Log("Hit Tilemap Edge Collider");
            
            }
        
            Destroy(this.gameObject);
        }

    }
}
