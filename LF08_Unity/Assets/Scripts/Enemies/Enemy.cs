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
        // public float MoveSpeed = 5f;

        // The Rigidbody2D component attached to the enemy game object
        protected Rigidbody2D Rb;

        private GameObject _player;
        private float _timeSinceLastHit = 1f;

        private const string EnemyMeleeSoundName = "meleeEnemy";


        //Money drop
        [SerializeField]
        private GameObject[] _itemList; // Stores the game items
        private int _itemNum; // Selects a number to choose from the itemList
        private int _randNum; // chooses a random number to see if loot os dropped- Loot chance
        private Transform _epos; // enemy position

        private void Start()
        {
            // Get the Rigidbody2D component attached to the enemy game object
            Rb = GetComponent<Rigidbody2D>();
            _epos = GetComponent<Transform>();
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
            if (_timeSinceLastHit <= 1f) return;
            AudioManager.main.PlaySFX(EnemyMeleeSoundName);
            _player.GetComponent<Player.Player>().TakeDamage(Damage);
            Debug.Log(collision.collider.name + " was hit!" + " Health: " +
                      collision.collider.GetComponent<Player.Player>().Health);

            // Reset the time since the last hit
            _timeSinceLastHit = 0f;
        }

        public virtual void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health > 0) return;
            OnEnemyDeath();
            Destroy(gameObject);
        }

        public virtual void OnEnemyDeath()
        {
            OnDeath?.Invoke(gameObject.GetInstanceID());
            DropItem();
        }

        public void DropItem()
        {
            _randNum = Random.Range(0, 101); // 100% total for determining loot chance;
            Debug.Log("Random Number is " + _randNum);

            switch (_randNum)
            {
                //super rare drop
                case >= 90:
                    _itemNum = 2;
                    Instantiate(_itemList[_itemNum], _epos.position, Quaternion.identity);
                    break;
                //rare drop
                case >= 75:
                    _itemNum = 1;
                    Instantiate(_itemList[_itemNum], _epos.position, Quaternion.identity);
                    break;
                //common drop
                case >= 25:
                    _itemNum = 0;
                    Instantiate(_itemList[_itemNum], _epos.position, Quaternion.identity);
                    break;
                default:
                    return; //no drop
            }
        }
    }
}