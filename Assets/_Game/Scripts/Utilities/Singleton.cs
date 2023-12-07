using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    [SerializeField]
    private bool isPersistent = false;

    public bool IsPersistent
    {
        get { return isPersistent; }
        set { isPersistent = value; }
    }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;

            if (isPersistent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}