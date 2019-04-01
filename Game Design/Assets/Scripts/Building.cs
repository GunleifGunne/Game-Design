using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] Transform healthBarPrefab;
    [SerializeField] float healthBarYOffset = 0.05f;
    [SerializeField] float health = 100;

    HealthBar healthBar;
    Sprite houseSprite;
    EnemySpawner enemySpawner;

    private float houseSpriteHeight;
    private float currentHealth;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();

        houseSprite = GetComponent<SpriteRenderer>().sprite;
        houseSpriteHeight = houseSprite.bounds.size.y / 2;

        Transform healthBarTransform = Instantiate(healthBarPrefab, 
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (houseSpriteHeight + healthBarYOffset)), 
            Quaternion.identity);
        healthBarTransform.transform.parent = gameObject.transform;
        healthBar = healthBarTransform.GetComponent<HealthBar>();

        currentHealth = health;
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
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enemySpawner.RemoveFromHousesList(gameObject);
        gameObject.tag = "Destroyed";
    }

    public float GetHealthPercentage()
    {
        return currentHealth / health;
    }
}
