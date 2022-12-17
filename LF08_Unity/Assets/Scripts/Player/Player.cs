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
        private PlayerStatsManager _playerStatsManager;

        public float BulletForce = 30f;
        public float Firerate = 0.5f;
        public float MoveSpeed = 10f;
        public float Health = 100f;
        public ulong Money = 0;

        private Rigidbody2D _playerRigidbody;
        public Camera Cam;

        private Vector2 _movement;
        private Vector2 _mousePosition;
        private float _nextfire;

        private const string playerShootingSoundName = "playerShoot";

        public Healthbar HealthBar;

        public GameObject deathScreen;

        public bool GodMode = false;
        public bool CanMove = true;
        private Color deathScreenColor;

        private void Start()
        {
            _playerStatsManager = new PlayerStatsManager();

            Enemy.OnDeath += OnEnemyDeath;
            _playerRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            HandlePlayerInput();
            UpdatePlayerRotation();
            UpdateCamera();
        }

        private void FixedUpdate()
        {
            UpdatePlayerMovement();
        }

        private void HandlePlayerInput()
        {
            if (CanMove)
            {
                if (Input.GetButton("Fire1") && !PauseMenu.isGamePaused)
                {
                    Shoot();
                }

                _movement.x = Input.GetAxisRaw("Horizontal");
                _movement.y = Input.GetAxisRaw("Vertical");

                _mousePosition = Cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        private void Shoot()
        {
            if (!(Time.time > _nextfire)) return;
            _nextfire = Time.time + Firerate;
            GameObject bullet = Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.TransformDirection(Vector2.up) * BulletForce, ForceMode2D.Impulse);
            AudioManager.main.PlaySFX(playerShootingSoundName);
        }

        private void UpdatePlayerMovement()
        {
            _playerRigidbody.MovePosition(_playerRigidbody.position + MoveSpeed * Time.fixedDeltaTime * _movement.normalized);
        }

        private string PickHurtSound()
        {
            Object[] hurtSounds = Resources.LoadAll("Audio/HurtSound/");
            int randomSound = Random.Range(0, hurtSounds.Length);
            return hurtSounds[randomSound].name;
        }

        private void UpdatePlayerRotation()
        {
            Vector2 lookDirection = _mousePosition - _playerRigidbody.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            _playerRigidbody.rotation = angle;
        }

        private void UpdateCamera()
        {
            Cam.transform.position = transform.position + new Vector3(0, 1, -5);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            StartCoroutine(colorChangeOnDamage());
            AudioManager.main.PlaySFX("HurtSound/" + PickHurtSound());
            HealthBar.SetHealth(Health);

            if (GodMode) return;
            if (Health > 0) return;
            StartCoroutine(deathTime());
        }

        private void OnEnemyDeath(int enemyId)
        {
            _playerStatsManager._playerStats.IncrementKillCount();
            Debug.Log(_playerStatsManager._playerStats.KillCount);
        }

        public void AddMoney(ulong amount)
        {
            Money += amount;
            Debug.Log("Money:" + Money);
        }

        public void AddHealth(float hp)
        {
            if(Health < 100) Health += hp;
            HealthBar.SetHealth(Health);
            Debug.Log("Added " + hp + " health");
        }
        
        private IEnumerator deathTime()
        {
                CanMove = false;
                deathScreen.SetActive(true);
                Time.timeScale = 0.25f;
                deathScreenColor = deathScreen.GetComponent<Image>().color;
                
                LeanTween.value(0f, 1f, 3f)
                         .setOnUpdate((float val) => 
                         { 
                             deathScreenColor.a = val;
                             deathScreen.GetComponent<Image>().color = deathScreenColor;
                         });

                LeanTween.value(Cam.orthographicSize, 2f, 1f)
                         .setOnUpdate((float val) => { Cam.orthographicSize = val; })
                         .setEase(easeOnDeath);

                yield return new WaitForSecondsRealtime(3f);

            LeanTween.cancelAll();
            SceneManager.LoadScene(2);
        }


        private IEnumerator colorChangeOnDamage()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if(child.TryGetComponent<SpriteRenderer>(out SpriteRenderer childSpriteRenderer))
                {
                    childSpriteRenderer.color = Color.red;
                }
            }

            yield return new WaitForSeconds(0.25f);

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.TryGetComponent<SpriteRenderer>(out SpriteRenderer childSpriteRenderer))
                {
                    childSpriteRenderer.color = Color.white;
                }
            }
        }
    }
}