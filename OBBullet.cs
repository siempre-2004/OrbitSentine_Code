using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class OBBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;
    public int bulletAttackValue;
    float _time = 1f;
    float coolingTime = 1;
    int bullet_number = 1;
    public List<GameObject> planets = new List<GameObject>();
    public AudioSource gunSound;
    public GameObject Decoy;

    public Animator animator;
    private void Start()
    {
        Decoy.SetActive(false);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (bullet_number > 0)
            {
                ShootBullet();
                PlayGunShotSound();
                animator.SetBool("IsShooting", true);
            }
        }

        void PlayGunShotSound()
        {
            if (gunSound != null)
            {
                gunSound.Play();
            }
        }

        if (bullet_number == 0)
        {
            if (_time >= 0)
            {
                _time -= Time.deltaTime;
            }
        }
        if (_time <= 0)
        {
            _time = coolingTime;
            bullet_number = 1;
            animator.SetBool("IsShooting", false);
        }
    }

    void ShootBullet()
    {
        Vector3 shootingDirection = transform.position.normalized;
        shootingDirection.z = 0;
        shootingDirection = shootingDirection.normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletShooter>().bulletAttackValue = bulletAttackValue;
        bullet_number -= 1;

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = shootingDirection * bulletSpeed;

        Vector3 shootingStraight = transform.position;
        transform.up = shootingStraight.normalized;
        Destroy(bullet, bulletLifetime);

    }

    public void UnlockMultiPlanets(int countToUnlock)
    {
        List<GameObject> tempPlanets = new List<GameObject>(planets);
        int unlockCount = Mathf.Min(countToUnlock, tempPlanets.Count);

        for (int i = 0; i < unlockCount; i++)
        {
            int index = Random.Range(0, tempPlanets.Count);
            Debug.Log("Random index: " + index);
            Debug.Log("Planets count before accessing: " + tempPlanets.Count);
            GameObject planetToUnlock = tempPlanets[index];
            UnlockPlanets(planetToUnlock);
            tempPlanets.RemoveAt(index);
        }
    }

    public void UnlockPlanets(GameObject planetToUnlock)
    {
        Star starComponent = planetToUnlock.GetComponent<Star>();
        Debug.Log("starComponent1");
        if (starComponent != null)
        {
            starComponent.HandleAsteroidCollision();
            starComponent.UnlockStar();
            Debug.Log("starComponent2");
        }
        else
        {
            Debug.LogError("Star component not found on " + planetToUnlock.name);
        }
        planetToUnlock.GetComponent<Unlock>()?.SetPlanetOpacity(1f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("asteroid"))
        {
            collision.GetComponent<asteroid>().OnDisposeHurt(bulletAttackValue);
            Debug.Log("bulletAttackValue=" + bulletAttackValue);
            Destroy(bulletPrefab);
            upgradeAsteroid.asteroidsDestroyed++;

        }

        if (collision.gameObject.CompareTag("upgradeAsteroid"))
        {
            planets.Add(GameObject.Find("planet1"));
            planets.Add(GameObject.Find("planet2"));
            planets.Add(GameObject.Find("planet3"));
            planets.Add(GameObject.Find("planet4"));
            planets.Add(GameObject.Find("planet5"));
            planets.Add(GameObject.Find("planet6"));
            UnlockMultiPlanets(1);
            Destroy(collision.gameObject);
            Destroy(bulletPrefab);
        }

        if (collision.gameObject.CompareTag("missile"))
        {
            planets.Add(GameObject.Find("planet1"));
            planets.Add(GameObject.Find("planet2"));
            planets.Add(GameObject.Find("planet3"));
            planets.Add(GameObject.Find("planet4"));
            planets.Add(GameObject.Find("planet5"));
            planets.Add(GameObject.Find("planet6"));
            collision.GetComponent<Missile>().OnDisposeHurt(bulletAttackValue);
            Debug.Log("bulletAttackValue=" + bulletAttackValue);

            Destroy(bulletPrefab);
        }

        if (collision.gameObject.CompareTag("Planet"))
        {
            Destroy(bulletPrefab); // collision.gameObject
        }
    }

    public void CoolTime()
    {
        if (coolingTime > 0.5f)
        {
            coolingTime -= 0.05f;
        }
    }

    public void DisableShooting()
    {
        bullet_number = 0;
        animator.SetBool("IsShooting", false);
    }

    public void EnableShooting()
    {
        bullet_number = 1;
    }
}


