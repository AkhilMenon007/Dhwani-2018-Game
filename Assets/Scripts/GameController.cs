using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameController : MonoBehaviour {

    public static GameController gameController;

    [SerializeField]
    private List<EnemyController> enemies;

    public bool gameStarted = false;

    #region PowerUp

    [SerializeField]
    PowerUp[] powerUps;

    [SerializeField]
    PowerUp powerUp;

    #endregion

    #region UI

    [SerializeField]
    private Text scoreText;


    [SerializeField]
    private Image skillImage;

    #endregion

    #region Selection Menu Items

    [SerializeField]
    private RawImage skillDescImage;
    [SerializeField]
    private Text skillDescText;
    [SerializeField]
    private Text skillDescHeading;
    [SerializeField]
    private Text skillDescCD;

    #endregion

    public List<Transform> Targets;

    [HideInInspector]
    public GameObject Player;

    public float score=0f;
    public float scoreMultiplier = 1f;

    public float CD = 0f;

    [SerializeField]
    Behaviour[] DestroyOnStart;

    #region Singleton Definiton

    private void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
            enemies = new List<EnemyController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    private void Start()
    {
        DisableAllEnemies();
    }

    
    public void StartGame()
    {
        EnableAllEnemies();
        Player.GetComponent<PlayerMover>().enabled = true;
        gameStarted = true;
        foreach (Behaviour toDestroy in DestroyOnStart)
        {
            Destroy(toDestroy);
        }
    }


    private void Update()
    {
        score += scoreMultiplier*Time.deltaTime;
        
        scoreText.text = "Score : " + ((int)score).ToString();
        
        if(CD > 0f)
        {
            CD -= Time.deltaTime;
        }
        skillImage.fillAmount = ( powerUp.powerUpCD - CD ) / powerUp.powerUpCD;

    }
    #region Enemy Related

    public void DisableAllEnemies()
    {
        foreach (EnemyController i in enemies)
        {
            i.mover.enabled = false;
            i.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    public void DisableEnemy(EnemyController enemy)
    {
        enemy.enabled = false;
        enemy.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void EnableAllEnemies()
    {
        foreach (EnemyController i in enemies)
        {
            i.mover.enabled = true;
            i.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }

    public void EnableEnemy(EnemyController enemy)
    {
        enemy.enabled = true;
    }

    public void RegisterEnemy(EnemyController enemyController)
    {
        enemies.Add(enemyController);
    }

    public void UnRegisterEnemy(EnemyController enemyController)
    {
        enemies.Remove(enemyController);
    }

    #endregion

    #region PowerUp Assignment and Refresh

    public void AssignPowerUp(int index)
    {
        powerUp = powerUps[index];
        skillImage.sprite = powerUp.image;
    }

    public void RefreshPowerUpDesc()
    {
        skillDescImage.texture = powerUp.image.texture;
        skillDescText.text = powerUp.description;
        skillDescCD.text = "Cooldown : " + powerUp.powerUpCD;
        skillDescHeading.text = powerUp.name;
    }

    #endregion

    public void TriggerPowerUp()
    {
        if (CD > 0)
            return;
        Debug.Log(powerUp.name);
        powerUp.TriggerPowerUp();
        CD += powerUp.powerUpCD;
    }

    


    #region PowerUp functions

    public void FreezeEnemies(float freezeDuration)
    {
        StartCoroutine(EnemyFreeze(freezeDuration));
    }

    private IEnumerator EnemyFreeze(float freezeDuration)
    {
        DisableAllEnemies();
        yield return new WaitForSeconds(freezeDuration);
        EnableAllEnemies();
    }

    public void DestroyEnemyInRange(float range)
    {
        List <EnemyController> toDestroy=new List<EnemyController>();

        foreach (EnemyController enemy in enemies)
        {
            if((enemy.transform.position-Player.transform.position).magnitude < range)
            {
                toDestroy.Add(enemy);
            }
        }

        foreach (EnemyController enemy in toDestroy)
        {
            enemies.Remove(enemy);
            enemy.DestroyThis();
        }
    }

    #endregion

    public Transform GenerateRandomTarget()
    {
        if (Targets.Count > 0)
            return Targets[Random.Range(0, Targets.Count)];
        else
            return null;
    }


}
