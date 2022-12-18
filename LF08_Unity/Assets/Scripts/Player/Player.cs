using Assets.Scripts.Enemies;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public Transform ShootingPoint;
        public GameObject BulletPrefab;

        public float BulletForce = 30f;
        public float Firerate = 0.5f;
        public float MoveSpeed = 10f;
        public float Health = 100f;
        public long Money;

        private Rigidbody2D _playerRigidbody;
        public Camera Cam;

        private Vector2 _movement;
        private Vector2 _mousePosition;
        private float _nextfire;

        private const string PlayerShootingSoundName = "playerShoot";

        public Healthbar HealthBar;

        public GameObject DeathScreen;
        public LeanTweenType EaseOnDeath;

        public bool GodMode = false;
        public bool CanMove = true;
        private Color _deathScreenColor;

        /// <summary>
        /// Initializes player components and registers for enemy death events.
        /// </summary>
        private void Start()
        {
            Enemy.OnDeath += OnEnemyDeath;
            _playerRigidbody = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Handles player input, updates player rotation and camera position.
        /// </summary>
        private void Update()
        {
            HandlePlayerInput();
            UpdatePlayerRotation();
            UpdateCamera();
        }

        /// <summary>
        /// Updates player movement.
        /// </summary>
        private void FixedUpdate()
        {
            UpdatePlayerMovement();
        }

        /// <summary>
        /// Handles player shooting input.
        /// </summary>
        private void HandlePlayerInput()
        {
            if (!CanMove) return;

            if (Input.GetButton("Fire1") && !PauseMenu.isGamePaused)
            {
                Shoot();
            }

            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _mousePosition = Cam.ScreenToWorldPoint(Input.mousePosition);
        }

        /// <summary>
        /// Fires a bullet.
        /// </summary>
        private void Shoot()
        {
            if (!(Time.time > _nextfire)) return;

            _nextfire = Time.time + Firerate;
            GameObject bullet = Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.TransformDirection(Vector2.up) * BulletForce, ForceMode2D.Impulse);
            AudioManager.main.PlaySFX(PlayerShootingSoundName);
        }

        /// <summary>
        /// Updates player movement based on input.
        /// </summary>
        private void UpdatePlayerMovement()
        {
            _playerRigidbody.MovePosition(_playerRigidbody.position + MoveSpeed * Time.fixedDeltaTime * _movement.normalized);
        }

        /// <summary>
        /// Picks a random hurt sound from resources.
        /// </summary>
        /// <returns>The name of a random hurt sound.</returns>
        private static string PickHurtSound()
        {
            Object[] hurtSounds = Resources.LoadAll("Audio/HurtSound/");
            int randomSound = Random.Range(0, hurtSounds.Length);
            return hurtSounds[randomSound].name;
        }

        /// <summary>
        /// Updates player rotation to face the mouse position.
        /// </summary>
        private void UpdatePlayerRotation()
        {
            Vector2 lookDirection = _mousePosition - _playerRigidbody.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            _playerRigidbody.rotation = angle;
        }

        /// <summary>
        /// Updates the camera position to follow the player.
        /// </summary>
        private void UpdateCamera()
        {
            Cam.transform.position = transform.position + new Vector3(0, 1, -5);
        }

        /// <summary>
        /// Damages the player and updates the health bar.
        /// </summary>
        /// <param name="damage">The amount of damage to apply to the player.</param>
        public void TakeDamage(float damage)
        {
            Health -= damage;
            StartCoroutine(ColorChangeOnDamage());
            AudioManager.main.PlaySFX("HurtSound/" + PickHurtSound());
            HealthBar.SetHealth(Health);

            if (GodMode) return;
            if (Health > 0) return;
            PlayerStatsManager.Instance.PlayerStatsLocal.IncrementDeathCount();
            StartCoroutine(DeathTime());
        }

        /// <summary>
        /// Event handler for enemy death events. Increments the player's kill count.
        /// </summary>
        /// <param name="enemyId">The ID of the enemy that died.</param>
        private void OnEnemyDeath(int enemyId)
        { 
            PlayerStatsManager.Instance.PlayerStatsLocal.IncrementKillCount();

            Debug.Log("Kills: "+ PlayerStatsManager.Instance.PlayerStatsLocal.KillCount);
        }

        /// <summary>
        /// Adds money to the player's total.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(long amount)
        {
            PlayerStatsManager.Instance.PlayerStatsLocal.IncrementMaxGold(amount);
            Money += amount;
            Debug.Log("Money:" + Money);
        }

        /// <summary>
        /// Adds health to the player.
        /// </summary>
        /// <param name="amount">The amount of health to add.</param>
        public void AddHealth(float amount)
        {
            if(Health < 100) Health += amount;
            HealthBar.SetHealth(Health);
            Debug.Log("Added " + amount + " health");
        }
        
        /// <summary>
        /// Coroutine that displays the death screen and loads the End Menu Scene.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DeathTime()
        {
                CanMove = false;
                DeathScreen.SetActive(true);
                Time.timeScale = 0.25f;
                _deathScreenColor = DeathScreen.GetComponent<Image>().color;
                
                LeanTween.value(0f, 1f, 3f)
                         .setOnUpdate(val => 
                         { 
                             _deathScreenColor.a = val;
                             DeathScreen.GetComponent<Image>().color = _deathScreenColor;
                         });

                LeanTween.value(Cam.orthographicSize, 2f, 1f)
                         .setOnUpdate(val => { Cam.orthographicSize = val; })
                         .setEase(EaseOnDeath);

                yield return new WaitForSecondsRealtime(3f);

            LeanTween.cancelAll();
            AudioManager.main.effects.audioMixer.SetFloat("cutOffFreq", 22000f);
            AudioManager.main.effects.audioMixer.SetFloat("effectsPitch", 1f);
            SceneManager.LoadScene(2);
        }
        
        /// <summary>
        /// Coroutine that flashes the player's sprite red when they take damage.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ColorChangeOnDamage()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if(child.TryGetComponent(out SpriteRenderer childSpriteRenderer))
                {
                    childSpriteRenderer.color = Color.red;
                }
            }

            yield return new WaitForSeconds(0.25f);

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.TryGetComponent(out SpriteRenderer childSpriteRenderer))
                {
                    childSpriteRenderer.color = Color.white;
                }
            }
        }
    }
}