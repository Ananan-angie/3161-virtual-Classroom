using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class SettingSL : MonoBehaviour
{
    [SerializeField]
    Slider volume;
    
    public static void SaveVolume(float v)
    {
        string path = Path.Combine(Application.persistentDataPath, "Saves/Settings");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path = Path.Combine(path, "Volume.humble");

        File.WriteAllText(path, v.ToString());
    }

    public static float loadVolume()
    {
        string path = Path.Combine(Application.persistentDataPath, "Saves/Settings");
        path = Path.Combine(path, "Volume.humble");

        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);
            float f;
            float.TryParse(loadJson, out f);
            return f;
        }
        else
        {
            Debug.LogError("Save file not found.");
            return 0;
        }
    }

    private void Awake()
    {
        volume.value = loadVolume();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
