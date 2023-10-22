using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private Text timer;
    [SerializeField]
    private SystemController system;

    private int roundTime = 10;
    public bool canStartRoundTimer = false;
    public Coroutine timerCoroutine; 
    private bool canPassTurn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<SystemController>();
        timer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTimer()
    {
        canPassTurn = false;

        timer.enabled = true;
        roundTime = 10;
        timer.text = roundTime.ToString();

        while (roundTime != -1)
        {
            timer.text = roundTime.ToString();
            roundTime--;
            yield return new WaitForSeconds(1f);
        }

        //system.PassRound();
    }

    void ResetRoundTimer()
    {
        timer.enabled = false;
        StopCoroutine("StartTimer");
        canStartRoundTimer = false;
    }
}
