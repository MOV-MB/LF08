using Assets.Scripts.Enemies;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class Bullet : MonoBehaviour
    {
        public GameObject HitEffect;
        private GameObject _enemy;

        private const string BulletHitEnemySoundName = "bulletHitEnemy";
        private const string BulletHitSoundName = "bulletHit";

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
        
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
            if (hit.collider != null)
            {
                GameObject effect = Instantiate(HitEffect, hit.point, Quaternion.identity);
                Destroy(effect, 0.5f);
                AudioManager.main.PlaySFX(BulletHitSoundName);
            }
        
            if (collision2D.collider.CompareTag("Enemy"))
            {
                // Get Enemy GameObject
                _enemy = collision2D.gameObject;
                _enemy.GetComponent<Enemy>().TakeDamage(10f);
                Debug.Log(_enemy.name + " was hit!" + " Health: " + _enemy.GetComponent<Enemy>().Health);
                AudioManager.main.PlaySFX(BulletHitEnemySoundName);
            }

            if (collision2D.collider.CompareTag("Tilemap"))
            {
                Debug.Log("Hit Tilemap Edge Collider");
            
            }
        
            Destroy(this.gameObject);
        }

    }
}