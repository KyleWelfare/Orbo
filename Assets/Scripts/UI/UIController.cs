using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private Image attackTutorial, directionalAttackTutorial, endofLevelMessage;

    [SerializeField] private Sprite fullHp, emptyHp, halfHp;
    [SerializeField] private Image hp1, hp2, hp3;

    private CanvasGroup deathPanel;
    public float fadeSpeed;
    [SerializeField] private bool shouldFadeToBlack, shouldFadeFromBlack;

    [SerializeField] private int coinsCollected;
    [SerializeField] Text coinText;

    public void Awake()
    {
        instance = this; 
        deathPanel = GetComponentInChildren<CanvasGroup>();
    }

    void Start()
    {
        coinsCollected = 0;      
    }

    void Update()
    {
        if (shouldFadeToBlack)
        {
            deathPanel.alpha = Mathf.MoveTowards(deathPanel.alpha, 1f, fadeSpeed * Time.deltaTime);
            if (deathPanel.alpha == 1f)
            {
                shouldFadeToBlack = false;
            }
        }
        if (shouldFadeFromBlack)
        {
            deathPanel.alpha = Mathf.MoveTowards(deathPanel.alpha, 0f, fadeSpeed * Time.deltaTime);
            if (deathPanel.alpha == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }
    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }
    public void UpdateHealthDisplay()
    {
        switch (PlayerHealthController.instance.currentHealth)
        {
            case 3:
                hp1.sprite = fullHp;
                hp2.sprite = fullHp;
                hp3.sprite = fullHp;
                break;
            case 2:
                hp1.sprite = fullHp;
                hp2.sprite = fullHp;
                hp3.sprite = emptyHp;
                break;
            case 1:
                hp1.sprite = fullHp;
                hp2.sprite = emptyHp;
                hp3.sprite = emptyHp;
                break;
            case 0:
                hp1.sprite = emptyHp;
                hp2.sprite = emptyHp;
                hp3.sprite = emptyHp;
                break;
            default:
                hp1.sprite = emptyHp;
                hp2.sprite = emptyHp;
                hp3.sprite = emptyHp;
                break;
        }
    }

    public void EnableAttackTutorial()
    {
        if (!attackTutorial.IsActive())
        {
            attackTutorial.gameObject.SetActive(true);
        }
    }

    public void DisableAttackTutorial()
    {
        if (attackTutorial.IsActive())
        {
            attackTutorial.gameObject.SetActive(false);
        }
    }

    public void EnableDirectionalAttackTutorial()
    {
        if (!directionalAttackTutorial.IsActive())
        {
            directionalAttackTutorial.gameObject.SetActive(true);
        }
    }

    public void DisableDirectionalAttackTutorial()
    {
        if (directionalAttackTutorial.IsActive())
        {
            directionalAttackTutorial.gameObject.SetActive(false);
        }
    }

    public void DisplayEndOfLevelMessage()
    {
        StartCoroutine(EndOfLevelMessageDecay());
    }

    private IEnumerator EndOfLevelMessageDecay()
    {
        endofLevelMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        endofLevelMessage.gameObject.SetActive(false);
    }

    public void UpdateCoinCount()
    {
        Debug.Log("+coin");
        coinsCollected++;
        coinText.text = coinsCollected + " / 3";

    }


}
