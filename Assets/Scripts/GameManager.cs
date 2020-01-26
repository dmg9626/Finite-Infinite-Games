using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Settings")]
    /// <summary>
    /// True if this is a finite game, false otherwise
    /// </summary>
    public bool finite;

    /// <summary>
    /// Duration of game (only used if finite is true)
    /// </summary>
    [SerializeField]
    [Range(30, 120)]
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
}