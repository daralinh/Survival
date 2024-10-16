using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance != null && this.gameObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void ResetInstance()
    {
        foreach (Transform child in instance.transform)
        {
            Destroy(child.gameObject);
        }

        Destroy(instance);
        instance = null;
    }
}
