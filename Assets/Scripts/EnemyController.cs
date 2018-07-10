using UnityEngine;

[RequireComponent(typeof(EnemyAIMover))]
public class EnemyController : MonoBehaviour {

    [HideInInspector]
    public EnemyAIMover mover;

    [SerializeField]
    GameObject deathExplosion;    

	// Use this for initialization
	void Start ()
    {
        mover = GetComponent<EnemyAIMover>();
        GameController.gameController.RegisterEnemy(this);
    }


    private void OnDisable()
    {
        GameController.gameController.UnRegisterEnemy(this);
    }

    public void DestroyThis()
    {
        Instantiate(deathExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
