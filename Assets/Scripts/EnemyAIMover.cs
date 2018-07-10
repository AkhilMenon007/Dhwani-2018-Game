using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMover : MonoBehaviour {


    public bool isChasingAI=true;
    public bool teleporting = false;
    public bool isChasing = false;

    public float speed = 5f;
    public float turning = 90f;
    public float radius = 5f;
    
    public Transform target;

    Vector3 targetPosition;

    Transform player;

    float turn;
    NavMeshAgent agent;

    float moveVertical;
    float moveHorizontal;

    private void Start()
    {
        targetPosition = new Vector3();
        targetPosition.y = transform.position.y;
        player = GameController.gameController.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        target = GameController.gameController.GenerateRandomTarget();
    }

    private void Update()
    {
        
        if ( isChasingAI && Vector3.Magnitude(transform.position - player.position) < radius )
        {
            agent.SetDestination(player.position);
            isChasing = true;
        }
        else
        {
            if(isChasing && agent.remainingDistance < radius)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.SetDestination(target.position);
                isChasing = false;
            }
        }


        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            target = GameController.gameController.GenerateRandomTarget();
        }

        targetPosition.x = agent.nextPosition.x;
        targetPosition.z = agent.nextPosition.z;
        transform.position = targetPosition;

    }


}
