using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// UI element allowing player to enter text that will fill in the blanks
/// </summary>
public class EntryItem : MonoBehaviour
{
    /// <summary>
    /// Describes the type of thing user should enter (ex. Person, Place, Group)
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI description;

    /// <summary>
    /// Input field user enters thing into
    /// </summary>
    [SerializeField]
    private TMP_InputField inputField;

    /// <summary>
    /// Returns text entered by user
    /// </summary>
    /// <returns></returns>
    public string GetUserEntry()
    {
        return inputField.text;
    }

    public void SetItemDescription(string description)
    {
        this.description.text = description;
    }
}
