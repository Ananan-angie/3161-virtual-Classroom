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
        dropitems.Add("Student 1");
        dropitems.Add("Lecturer 1");
        dropitems.Add("Student 2");
        dropitems.Add("Lecturer 1");
        foreach (var item in dropitems)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
        positions.Add(new Vector3(0, 0, 0));
        positions.Add(new Vector3(-10, 9, 0));
        positions.Add(new Vector3(-10, 19, 0));
        positions.Add(new Vector3(8, 9, 0));
        positions.Add(new Vector3(8, 19, 0));

    }

    public void StartTeleport()
    {
        if (dropdown.value <= positions.Capacity)
        {
            player.transform.position = positions[dropdown.value];
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
