using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        // The amount of damage the enemy does when it hits the player
        public float Damage = 20f;
        public float Health = 20f;

        // The direction in which the enemy is moving
        protected Vector2 Movement;

        // The speed at which the enemy moves
        public float MoveSpeed = 5f;

        // The Rigidbody2D component attached to the enemy game object
        protected Rigidbody2D Rb;

        private GameObject _player;
        private bool _isCollidingWithPlayer;

        private void Start()
        {
            // Get the Rigidbody2D component attached to the enemy game object
            Rb = GetComponent<Rigidbody2D>();
            _player = GameObject.Find("Player");
        }

        private void Update()
        {
            // Update the movement direction of the enemy
            UpdateMovement();
        }

        public virtual void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0) Destroy(gameObject);
        }

        // Method to update the movement direction of the enemy
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the enemy hit the player game object
            if (!collision.collider.CompareTag("Player")) return;
            // Deal damage to the player
            collision.collider.GetComponent<Player.Player>().TakeDamage(Damage);
            Debug.Log(collision.collider.name + " was hit!" + " Health: " +
                      collision.collider.GetComponent<Player.Player>().Health);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the enemy is colliding with the player
            if (other.gameObject.CompareTag("Player"))
            {
                _isCollidingWithPlayer = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Check if the enemy is no longer colliding with the player
            if (other.gameObject.CompareTag("Player"))
            {
                _isCollidingWithPlayer = false;
            }
        }
    }
}