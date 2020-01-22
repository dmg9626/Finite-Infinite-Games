using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Prompt : MonoBehaviour
{
    /// <summary>
    /// Describes the type of thing user should enter
    /// </summary>
    private TextMeshProUGUI promptField;

    /// <summary>
    /// Prompt with blanks denoted with blankIdentifier
    /// </summary>
    private string prompt;

    /// <summary>
    /// String used to show blanks in UI
    /// </summary>
    private static string blankPlaceholder = "______";

    /// <summary>
    /// String used to show blanks in backend
    /// </summary>
    /// <value></value>
    public static char blankIdentifier = '#';

    // Start is called before the first frame update
    void Awake()
    {
        if(!TryGetComponent(out promptField)) {
            Debug.LogError("Error: TextMeshProUGUI component missing from " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Initializes prompt field with incompleted movie prompt
    /// </summary>
    /// <param name="prompt">Prompt with blank fields denoted by blankIdentifier</param>
    public void InitializePrompt(string prompt)
    {
        // Save raw prompt text
        this.prompt = prompt;

        // Show prompt in UI with pretty blanks
        promptField.text = prompt.Replace(blankIdentifier.ToString(), blankPlaceholder);
    }

    public void PopulateBlanks(string[] entries)
    {
        string newPrompt = prompt;
        int index = 0;
        for(int i = 0; i < entries.Length; i++) {
            string entry = entries[i];

            // Find index of blank
            index = newPrompt.IndexOf(blankIdentifier, index);

            // Abort if index = -1
            if(index < 0) {
                Debug.LogError("index < 0");
                return;
            }

            // Replace blank with user entry
            newPrompt = ReplaceFirstOccurrence(newPrompt, blankIdentifier.ToString(), entry);
            Debug.Log(newPrompt);
        }
        
        Debug.Log("Finished prompt: " + newPrompt);
        promptField.text = newPrompt;
    }

    private string ReplaceFirstOccurrence (string source, string find, string replace)
    {
        int Place = source.IndexOf(find);
        string result = source.Remove(Place, find.Length).Insert(Place, replace);
        return result;
    }
}
