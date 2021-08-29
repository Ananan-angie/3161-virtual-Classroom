using UnityEngine;

//  Code from:
// https://www.youtube.com/watch?v=ErJgQY5smnw&t=3s

public abstract class Singleton<T> : MonoBehaviour where T : Component {
    private static T instance;

    private static bool m_applicationIsQuitting = false;

    public static T Instance {
        get
		{
            if (m_applicationIsQuitting) { return null; }

            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    /* IMPORTANT!!! To use Awake in a derived class you need to do it this way
     * protected override void Awake()
     * {
     *     base.Awake();
     *     //Your code goes here
     * }
     * 
     * To allow DontDestroyOnLoad (persistent when changing scenes), need to override Awake
     * as by default the isDontDestroyOnLoad is false
     * */


    protected virtual void Awake (bool isDontDestroyOnLoad = false) {
        if (instance == null) {
            instance = this as T;
            if (isDontDestroyOnLoad) DontDestroyOnLoad (gameObject);
        } else if (instance != this as T) {
            Destroy (gameObject);
        } else if (isDontDestroyOnLoad) { DontDestroyOnLoad (gameObject); }
    }

    private void OnApplicationQuit () {
        m_applicationIsQuitting = true;
    }
}