using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
    /// <summary>
    /// Text showing number of plots generated
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI plotsGeneratedText;

    /// <summary>
    /// Text showing total box office 
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI totalBoxOfficeText;

    /// <summary>
    /// Button takes player back to Main Menu when pressed
    /// </summary>
    [SerializeField]
    private Button mainMenuButton;

    void Start()
    {
        mainMenuButton.onClick.AddListener(() => SceneLoader.LoadScene("MainMenu"));
    }

    public void ShowResults(int plotsGenerated, float totalBoxOffice)
    {
        plotsGeneratedText.text = "Number of Plots Generated: " + plotsGenerated;
        totalBoxOfficeText.text = string.Format("Total Box Office: ${0} million", totalBoxOffice.ToString()); 
    }
}
