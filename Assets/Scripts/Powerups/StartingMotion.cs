using UnityEngine;
using UnityEngine.AI;

public class StartingMotion : MonoBehaviour {

    NavMeshAgent agent;
    [SerializeField]
    float turning = 45f;
    float turn;
    Vector3 targetPosition;
    [SerializeField]
    float radius=10f;
    private void Start()
    {
        targetPosition = new Vector3();
        agent=GetComponent<NavMeshAgent>();
        agent.SetDestination(GameController.gameController.GenerateRandomTarget().position);
        targetPosition.y = transform.position.y;
        agent.speed = GetComponent<PlayerMover>().movementOffset * GetComponent<PlayerMover>().speed;
    }
    private void Update()
    {

        targetPosition.x = agent.nextPosition.x;
        targetPosition.z = agent.nextPosition.z;
        transform.position = agent.nextPosition;
        turn = agent.velocity.x / agent.speed;

        Quaternion.LookRotation(targetPosition - transform.position, transform.up);

        //transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + turn * turning * Time.deltaTime, 0f);
        if (agent.remainingDistance  <= radius)
        {
            agent.SetDestination(GameController.gameController.GenerateRandomTarget().position);
        }
    }
}
