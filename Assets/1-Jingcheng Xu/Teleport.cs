using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleport : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    List<Vector3> positions;
    [SerializeField]
    TMP_Dropdown dropdown;
    [SerializeField]
    List<string> dropitems;

    private void Awake()
    {
        dropitems.Add("Main");
        dropitems.Add("Class 1");
        dropitems.Add("Class 2");
        foreach (var item in dropitems)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
        positions.Add(new Vector3(0, 0, 0));
        positions.Add(new Vector3(-10, 9, 0));
        positions.Add(new Vector3(8, 9, 0));
    }

    public void StartTeleport()
    {
        if (dropitems[dropdown.value] == "Main")
        {
            player.transform.position = positions[0];
        }
        else if (dropitems[dropdown.value] == "Class 1")
        {
            player.transform.position = positions[1];
        }
        else if (dropitems[dropdown.value] == "Class 2")
        {
            player.transform.position = positions[2];
        }
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
