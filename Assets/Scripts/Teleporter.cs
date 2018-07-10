using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class Teleporter : MonoBehaviour
{
    Transform endTransform;
    Quaternion endRotation;

    [SerializeField]
    float scoreBonus = 100f;

    [SerializeField]
    float offset;

    // Use this for initialization
    void Start ()
    {
        endTransform = GetComponent<OffMeshLink>().endTransform;
        endRotation = endTransform.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAIMover>() != null)
        {
            other.GetComponent<EnemyAIMover>().enabled = false;
            other.GetComponent<NavMeshAgent>().enabled = false;
        }

        if(other.GetComponent<PlayerMover>() != null)
        {
            GameController.gameController.score += scoreBonus;
        }
        other.GetComponent<Transform>().rotation = endRotation;
        other.GetComponent<Transform>().position = endTransform.localPosition + other.transform.forward*offset;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyAIMover>() != null)
        {
            other.GetComponent<EnemyAIMover>().enabled = true;
            other.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
