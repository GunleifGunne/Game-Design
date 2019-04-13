using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUpdate : MonoBehaviour
{
    [SerializeField] Image firstLife;
    [SerializeField] Image secondLife;

    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = new SceneLoader();
        LifeManager.ResetLife();
        firstLife.gameObject.SetActive(true);
        secondLife.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLifeIconsAndScene(firstLife, secondLife);
    }

    private void UpdateLifeIconsAndScene(Image firstLife, Image secondLife)
    {
        if(LifeManager.Counter == 1)
        {
            secondLife.gameObject.SetActive(false);
        }
        
        if(LifeManager.Counter <= 0)
        {
            firstLife.gameObject.SetActive(false);
            LifeManager.GameOver();
            StartCoroutine(GameOverWithDelay());
        }
    }

    IEnumerator GameOverWithDelay()
    {
        if (LifeManager.isGameOver)
        {
            yield return new WaitForSeconds(2);
            sceneLoader.LoadGameOverScene();
        }
    }
}
