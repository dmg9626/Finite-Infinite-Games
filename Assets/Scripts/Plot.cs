using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Plot : MonoBehaviour
{
    /// <summary>
    /// Prompt shown to user (blanks denoted by Prompt.blankIdentifier)
    /// </summary>
    public string prompt;

    /// <summary>
    /// Type of thing requested for each blank field
    /// </summary>
    public string[] fieldEntryTypes;

    public int numberOfBlanks {get; private set;}

    /// <summary>
    /// UI fields user enters their answers into
    /// </summary>
    private EntryItem[] entryItems;

    public void PopulateEntryItems(EntryItem entryItemPrefab, VerticalLayoutGroup layoutGroup)
    {
        // Create array of entry items corresponding to each blank field
        numberOfBlanks = prompt.Count(f => f == Prompt.blankIdentifier);
        entryItems = new EntryItem[numberOfBlanks];

        for(int i = 0; i < numberOfBlanks; i++) {
            // Instantiate entry field with description of thing to enter
            EntryItem entryItem = Instantiate(entryItemPrefab, layoutGroup.transform);
            entryItem.SetItemDescription(fieldEntryTypes[i]);

            // Add ato list
            entryItems[i] = entryItem;
        }
    }

    /// <summary>
    /// Populates blank fields in prompt with user's submissions in EntryItems.
    /// </summary>
    /// <param name="promptField"></param>
    public void PopulateBlanks(Prompt promptField)
    {
        Debug.Log("Plot | Populating blanks");
        string[] entries = new string[entryItems.Length];

        // TODO: check if user left any fields blank
        for(int i = 0; i < entryItems.Length; i++) {
            EntryItem entry = entryItems[i];
            entries[i] = entry.GetUserEntry();
        }
        promptField.PopulateBlanks(entries);
    }
}
