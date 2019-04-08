using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Building : MonoBehaviour
{
    [SerializeField] Transform healthBarPrefab;
    [SerializeField] float healthBarYOffset = 0.05f;
    [SerializeField] float health = 100;
    [SerializeField] Sprite destroyedSprite;
    [SerializeField] int lives = 2;

    HealthBar healthBar;
    Sprite houseSprite;
    GameOver gameOver;

    private float houseSpriteHeight;
    private float currentHealth;


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

        gameOver = FindObjectOfType<GameOver>();
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

            if (GameObject.FindGameObjectsWithTag("Destroyed").Length >= lives)
            {
                gameOver.EndGame();
            }
        }
    }

    private void Die()
    {
        gameObject.tag = "Destroyed";
        GetComponent<SpriteRenderer>().sprite = destroyedSprite;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / health;
    }
}
