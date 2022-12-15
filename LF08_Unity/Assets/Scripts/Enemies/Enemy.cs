using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        // The amount of damage the enemy does when it hits the player
        public float Damage = 20f;
        public float Health = 20f;

        public delegate void OnDeathHandler(int enemyId);
        public static event OnDeathHandler OnDeath;

        // The direction in which the enemy is moving
        protected Vector2 Movement;

        // The speed at which the enemy moves
        public float MoveSpeed = 5f;

        // The Rigidbody2D component attached to the enemy game object
        protected Rigidbody2D Rb;

        private GameObject _player;
        private float _timeSinceLastHit = 1f;

        private const string enemyMeleeSoundName = "meleeEnemy";

        private void Start()
        {
            // Get the Rigidbody2D component attached to the enemy game object
            Rb = GetComponent<Rigidbody2D>();
            _player = GameObject.Find("Player");
        }

        private void FixedUpdate()
        {
            // Update the movement direction of the enemy
            //UpdateMovement();
        }
        
        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            // Check if the enemy hit the player game object
            if (!collision.collider.CompareTag("Player")) return;
            // Increment the time since the last hit
            _timeSinceLastHit += Time.deltaTime;

            // Check if at least one second has passed since the last hit
            if (!(_timeSinceLastHit >= 1f)) return;
            AudioManager.main.PlaySFX(enemyMeleeSoundName);
            _player.GetComponent<Player.Player>().TakeDamage(Damage);
            Debug.Log(collision.collider.name + " was hit!" + " Health: " +
                      collision.collider.GetComponent<Player.Player>().Health);

            // Reset the time since the last hit
            _timeSinceLastHit = 0f;
        }

        public virtual void TakeDamage(float damage)
        {
            Health -= damage;
            if (!(Health <= 0)) return;
            OnEnemyDeath();
            Destroy(gameObject);

        }

        public virtual void OnEnemyDeath()
        {
            OnDeath?.Invoke(gameObject.GetInstanceID());
        }

        /*
        protected virtual void UpdateMovement()
        {
            // Check if the player is still alive
            if (!GameObject.FindWithTag("Player"))
                return;

            // Get the position of the player game object
            Vector2 playerPosition = GameObject.FindWithTag("Player").transform.position;


            // Calculate the direction in which the enemy should move to reach the player
            Vector2 direction = (playerPosition - Rb.position).normalized;

            // face player
            transform.up = direction;

            // Check if the movement direction is in front of the enemy
            if (Vector2.Dot(direction, transform.right) > 0)
                Movement = direction;

            Rb.MovePosition(Rb.position + MoveSpeed * Time.fixedDeltaTime * Movement);
        }
        */
        
        
    }
}