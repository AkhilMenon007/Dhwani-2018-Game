using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMover))]
public class PlayerController : MonoBehaviour {

    public bool isKillable = true;
    public bool isDamageable = true;
    [SerializeField]
    float collisionSlowAmount = 2f;
    [SerializeField]
    float collisionSlowTime = 3f;


    [SerializeField]
    Color fullHPColor = Color.green;
    [SerializeField]
    Color invulnerabilityColor = Color.blue;
    [SerializeField]
    Color noDamageColor = Color.white;
    [SerializeField]
    Color noHPColor = Color.red;

    Color targetColor;

    [SerializeField]
    float colorChangeMultiplier=2f;

    [SerializeField]
    Material ChangingMaterial;

    [SerializeField]
    int collisionDamageMultiplier = 1;

    [HideInInspector]
    public int health = 100;


    private void OnEnable()
    {
        targetColor = fullHPColor;
        GameController.gameController.Player = gameObject;
        ChangingMaterial.color = fullHPColor;
    }


    private void Update()
    {
        ChangingMaterial.color=Color.Lerp(ChangingMaterial.color, targetColor, Time.deltaTime * colorChangeMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDamageable)
            return;
        TakeDamage((int)(collisionDamageMultiplier * GetComponent<Rigidbody>().velocity.magnitude));

        TriggerNoDamage(1f);

        TriggerSlow(collisionSlowTime, collisionSlowAmount);
    }

    public void DestroyPlayer()
    {
        if(isKillable)
            StartCoroutine(Death());
    }

    public void TakeDamage(int damage)
    {
        if (isDamageable)
        {
            health -= damage;
            SetColor();
        }
        if (health <= 0)
        {
            DestroyPlayer();
        }
    }

    public void SetColor()
    {
        targetColor = Color.Lerp(fullHPColor, noHPColor, (100 - (float)health) / 100);
    }

    public void ResetColor()
    {
        targetColor = fullHPColor;
    }
    public void TriggerSlow(float slowTime,float slowAmount)
    {
        StartCoroutine(Slow(slowTime, slowAmount));
    }

    public void TriggerInvulnerable(float invulTime)
    {
        StartCoroutine(Invulnerability(invulTime));
    }


    public void TriggerNoDamage(float noDamageTime)
    {
        StartCoroutine(NoDamage(noDamageTime));
    }

    public void StartGame()
    {
        GameController.gameController.StartGame();
    }

    #region Coroutines

    private IEnumerator Invulnerability(float invulTime)
    {

        isDamageable = false;
        isKillable = false;
        targetColor = invulnerabilityColor;
        yield return new WaitForSeconds(invulTime);
        SetColor();
        isKillable = true;
        isDamageable = true;
    }

    private IEnumerator NoDamage(float noDamageTime)
    {
        isDamageable = false;
        targetColor = noDamageColor;
        yield return new WaitForSeconds(noDamageTime);
        SetColor();
        isDamageable = true;
    }



    private IEnumerator Slow(float slowTime,float slowAmount)
    {
        PlayerMover playerMover = GetComponent<PlayerMover>();
        float initialSpeed = playerMover.initialSpeed;

        playerMover.speed = initialSpeed / slowAmount;

        yield return new WaitForSeconds(slowTime);

        playerMover.speed = initialSpeed;
    }

    private IEnumerator Death()
    {
        GetComponent<PlayerMover>().enabled = false;
        //Insert Particle effect and Restart menu functions here
        GameController.gameController.DisableAllEnemies();
        yield return new WaitForSeconds(3f);

        SceneManager.LoadSceneAsync(0);
    }

    #endregion
}
