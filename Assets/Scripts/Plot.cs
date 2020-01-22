using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Plot : MonoBehaviour
{
    /// <summary>
    /// Prompt shown to user (blanks denoted by Prompt.blankIdentifier)
    /// </summary>
    public string prompt;

    /// <summary>
    /// Reference to entry item prefab
    /// </summary>
    private EntryItem entryItemPrefab;

    private int numberOfBlanks;

    [SerializeField]
    private EntryItem[] entryItems;

    // Start is called before the first frame update
    void Start()
    {
        // Create array of entry items corresponding to each blank field
        numberOfBlanks = prompt.Count(f => f == Prompt.blankIdentifier);
        // entryItems = new EntryItem[numberOfBlanks];
    }

    // TODO: instantaite verticallayoutgroup with entry items
    public void PopulateEntryItems()
    {

    }

    public void PopulateBlanks(Prompt promptField)
    {
        Debug.Log("Plot | Populating blanks...");
        string[] entries = new string[entryItems.Length];
        for(int i = 0; i < entryItems.Length; i++) {
            EntryItem entry = entryItems[i];
            entries[i] = entry.GetUserEntry();
        }
        promptField.PopulateBlanks(entries);
    }
}
