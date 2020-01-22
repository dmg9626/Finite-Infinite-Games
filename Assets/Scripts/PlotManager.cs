using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    /// <summary>
    /// Shows how much money the movie grossed (only shown after submission)
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI boxOfficeTextField;

    /// <summary>
    /// Shows total money grossed
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI totalRevenueField;

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

    /// <summary>
    /// Range of revenue values that each movie plot can gross
    /// </summary>
    [SerializeField]
    private Vector2 boxOfficeRange;

    /// <summary>
    /// Total revenue (in millions)
    /// </summary>
    private float totalRevenue;

    // Start is called before the first frame update
    void Start()
    {
        // Get plots in children
        GetComponentsInChildren<Plot>(plots);
        
        // Start at current plot
        currentPlot = plots[0];
        ShowPlot();

        // Button press event
        submitButton.onClick.AddListener(() => {
            // Populate fields with user entries
            currentPlot.PopulateBlanks(promptField);

            GenerateRevenue();
            
            ClearEntryFields();

            // Wait for seconds before presenting next prompt
            StartCoroutine(NextPlot(1.5f));
        });
    }

    void GenerateRevenue()
    {
        // Show amout of money grossed by this film
        int moneyGrossed = (int)Random.Range(boxOfficeRange.x, boxOfficeRange.y);
        if(moneyGrossed > 0) {
            boxOfficeTextField.text = string.Format("Your film made ${0} dollars!!", moneyGrossed);
        }
        else {
            boxOfficeTextField.text = string.Format("Your film lost ${0} dollars...", moneyGrossed);
        }

        // Convert to millions of dollars
        float revenueInMillions = moneyGrossed / Mathf.Pow(10, 7);
        
        // Update total revenue field with money gained/lost from this film
        totalRevenue += revenueInMillions;
        totalRevenueField.text = string.Format("Total Revenue:\n${0} million", totalRevenue);
    }

    IEnumerator NextPlot(float delay)
    {
        // Wait for seconds before showing next plot prompt
        yield return new WaitForSeconds(delay);

        int index = plots.IndexOf(currentPlot);
        // Go to next plot
        try {
            Debug.LogFormat("Moving from plot {0} to {1}", index, index+1);
            currentPlot = plots[index+1];
        }
        // Or start from beginning if we did the last one
        catch(System.ArgumentOutOfRangeException ex) {
            Debug.Log("Looping back to first plot");
            currentPlot = plots[0];
        }

        ShowPlot();
    }

    void ShowPlot()
    {
        // Set movie prompt in UI
        promptField.InitializePrompt(currentPlot.prompt);

        // Populate new ones
        currentPlot.PopulateEntryItems(entryItemPrefab, verticalLayoutGroup);

        // Clear box office text field
        boxOfficeTextField.text = "";
    }

    void ClearEntryFields()
    {
        // Clear existing entry items
        for(int i = 0; i < verticalLayoutGroup.transform.childCount; i++) {
            Transform child = verticalLayoutGroup.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
