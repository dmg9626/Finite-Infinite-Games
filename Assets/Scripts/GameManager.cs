using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// Displays game results when time runs out (only for finite game)
    /// </summary>
    [SerializeField]
    private EndScreen endScreen;

    [SerializeField]
    private GameObject gameScreen;

    [SerializeField]
    private PlotManager plotManager;

    [Header("Settings")]
    /// <summary>
    /// True if this is a finite game, false otherwise
    /// </summary>
    public bool finite;

    /// <summary>
    /// Duration of game (only used if finite is true)
    /// </summary>
    [SerializeField]
    [Range(5, 120)]
    private float timeLimit;

    /// <summary>
    /// Amount of time remaining
    /// </summary>
    public float timeRemaining {get; private set;}

    public bool timeExpired {
        get { return timeRemaining < 0; }
        private set {}
    }


    /// <summary>
    /// Event fired when game timer runs out
    /// </summary>
    public onTimeExpired OnTimeExpired;
    public delegate void onTimeExpired();

    void Start()
    {
        if(finite) {
            timeRemaining = timeLimit;
            StartCoroutine(Tick());

            // Trigger game over when time runs out
            OnTimeExpired += () => GameOver();
        }
    }

    private IEnumerator Tick()
    {
        // Decrement time every frame
        while(!timeExpired) {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        // Fire event when time runs out
        OnTimeExpired?.Invoke();
    }

    private void GameOver()
    {
        endScreen.gameObject.SetActive(true);
        gameScreen.SetActive(false);
        endScreen.ShowResults(plotManager.plotsGenerated, plotManager.totalRevenue);
    }
}