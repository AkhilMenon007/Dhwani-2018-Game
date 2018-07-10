using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy dontDestroy;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (dontDestroy == null)
        {
            dontDestroy = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
