using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField]
    float time;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, time);
	}
	
}
