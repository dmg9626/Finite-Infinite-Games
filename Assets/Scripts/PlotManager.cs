using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotManager : MonoBehaviour
{
    [Header("UI References")]
    /// <summary>
    /// Holds all the entry fields for user to fill out blanks
    /// </summary>
    [SerializeField]
    private VerticalLayoutGroup verticalLayoutGroup;

    /// <summary>
    /// Used to display prompt in UI
    /// </summary>
    [SerializeField]
    private Prompt promptField;

    /// <summary>
    /// Fills out the plot with user entries when clicked
    /// </summary>
    [SerializeField]
    private Button submitButton;

    [Header("Data")]
    /// <summary>
    /// Reference to entry item prefab
    /// </summary>
    [SerializeField]
    private EntryItem entryItemPrefab;

    /// <summary>
    /// List of plots prompts to give the player
    /// </summary>
    [SerializeField]
    private List<Plot> plots;

    /// <summary>
    /// Current plot displayed
    /// </summary>
    private Plot currentPlot;

    // Start is called before the first frame update
    void Start()
    {
        // Start at current plot
        currentPlot = plots[0];
        ShowPlot();

        // Populate blanks with user entries after pressing button
        submitButton.onClick.AddListener(() => {
            currentPlot.PopulateBlanks(promptField);
            
        });
    }

    void NextPlot()
    {
        int index = plots.IndexOf(currentPlot);
        // Go to next plot
        try {
            currentPlot = plots[index++];
        }
        // Or start from beginning if we did the last one
        catch(System.IndexOutOfRangeException ex) {
            currentPlot = plots[0];
        }

        ShowPlot();
    }

    void ShowPlot()
    {
        // Set movie prompt in UI
        promptField.InitializePrompt(currentPlot.prompt);

        // Clear existing entry items
        for(int i = 0; i < verticalLayoutGroup.transform.childCount; i++) {
            Transform child = verticalLayoutGroup.transform.GetChild(i);
            Destroy(child);
        }
        // Populate new ones
        currentPlot.PopulateEntryItems(entryItemPrefab, verticalLayoutGroup);
    }
}
