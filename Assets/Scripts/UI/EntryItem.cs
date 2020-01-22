using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntryItem : MonoBehaviour
{
    /// <summary>
    /// Describes the type of thing user should enter
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI description;

    /// <summary>
    /// Input field user enters thing into
    /// </summary>
    [SerializeField]
    private TMP_InputField inputField;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetUserEntry()
    {
        return inputField.text;
    }
}
