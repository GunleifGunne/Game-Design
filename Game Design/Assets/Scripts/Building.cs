using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] Transform healthBarPrefab;
    [SerializeField] float healthBarYOffset = 0.05f;
    [SerializeField] float health = 100;
    [SerializeField] Sprite destroyedSprite;
    [SerializeField] CameraShake shake;

    private AudioSource DestroySound;

    HealthBar healthBar;
    Sprite houseSprite;
    SceneLoader sceneLoader = new SceneLoader();
    Rigidbody gameObjectsRigidbody;

    private float houseSpriteHeight;
    private float currentHealth;

    int lifeCounter = 2;
    bool isShake = false;

    private void Start()
    {
        houseSprite = GetComponent<SpriteRenderer>().sprite;
        houseSpriteHeight = houseSprite.bounds.size.y / 2;

        Transform healthBarTransform = Instantiate(healthBarPrefab, 
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (houseSpriteHeight + healthBarYOffset)), 
            Quaternion.identity);
        healthBarTransform.transform.parent = gameObject.transform;
        healthBar = healthBarTransform.GetComponent<HealthBar>();

        currentHealth = health;

        DestroySound = GameObject.Find("Sound").GetComponent<BGMusic>().houseDestroyed;
        
    }

    private void Update()
    {
        if (LifeManager.Counter < 2 && isShake == true)
        {
            if (LifeManager.isGameOver)
            {
                StartCoroutine(shake.shakeTrigger(6));
            }
            else
            {
                StartCoroutine(shake.shakeTrigger(1));
            }
        }
        isShake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damage)
    {
        currentHealth -= damage.GetDamage();
        damage.Hit();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.tag = "Destroyed";
        GetComponent<SpriteRenderer>().sprite = destroyedSprite;
        DestroySound.Play();
        LifeManager.ReduceLife();

        isShake = true;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / health;
    }

    void OnCollisionEnter2D (Collision2D other){
        if(other.gameObject.name == "Player Projectile Ice(Clone)" || other.gameObject.name == "Player Projectile Fire(Clone)" || other.gameObject.name == "Player Projectile Water(Clone)"|| other.gameObject.name == "Player Projectile Earth(Clone)"){
        Destroy(other.gameObject);
        }
    }
}
