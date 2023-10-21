using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class SystemController : MonoBehaviour
{
    public bool isMatchOver = false;
    public GameObject playerPrefab;
    public List<GameObject> teamRed;
    public List<GameObject> teamBlue;
    string[] teams = new string[] { "RED", "BLUE" };

    public Text timer;
    private int roundTime = 10;
    public bool canStartRoundTimer = false;
    public Coroutine timerCoroutine;

    public string currentTeam;
    public GameObject currentPlayer;

    private bool canNextRound = false;
    

    private void Start()
    {
        timer = GetComponent<Text>();
        PreStartGame();            
    }

    private void Update()
    {        
        while (!isMatchOver && canNextRound)
        {
            //Find other player to pass time        
            if (currentTeam == "RED")
            {
                FindNextPlayerTeamRed();
            }
            else
            {
                FindNextPlayerTeamBlue();
            }
        }
    }

    void PreStartGame()
    {
        //Instantiate players
        teamBlue.Add(Instantiate(playerPrefab, new Vector3(0, -1.885728f, 0), new Quaternion()));
        teamRed.Add(Instantiate(playerPrefab, new Vector3(60, -1.885728f, 0), new Quaternion()));        

        // Sort which team plays
        Array.Sort(teams);
        currentTeam = teams[0];

        // Get the first player to start the battle
        currentPlayer = currentTeam == "RED" ? teamRed[0] : teamBlue[0];
        
        //Reset match vars
        teamBlue.ForEach(player => {
            PlayerController playerControll = player.GetComponent<PlayerController>();
            playerControll.team = "BLUE";
            playerControll.havePlayed = false;
        });
        teamRed.ForEach(player => {
            PlayerController playerControll = player.GetComponent<PlayerController>();
            playerControll.team = "RED";
            playerControll.havePlayed = false;
        });

        //Start first round
        canNextRound = true;
    }
    
    IEnumerator RoundTimer()
    {
        canNextRound = false;

        timer.enabled = true;
        roundTime = 10;
        timer.text = roundTime.ToString(); 

        while (roundTime != -1)
        {
            timer.text = roundTime.ToString();
            roundTime--;
            yield return new WaitForSeconds(1f);
        }            
                 
        PassRound();        
    }
    void ResetRoundTimer()
    {
        //system.timer.enabled = false;
        //matchControll.StopCoroutine("RoundTimer");
        //matchControll.canStartRoundTimer = false;
    }
    public void PassRound()
    {
        //Hide timer
        StopCoroutine("RoundTimer");
        timer.enabled = false;

        //Enable havedPlayed var
        PlayerController playerControll = currentPlayer.GetComponent<PlayerController>();
        playerControll.havePlayed = true; 

        //Change team        
        currentTeam = currentTeam == "BLUE" ? "RED" : "BLUE";
        canNextRound = true;
    }
    void FindNextPlayerTeamRed()
    {
        teamRed.ForEach(player =>
        {
            PlayerController playerControll = player.GetComponent<PlayerController>();
            Debug.Log("player red -> " + playerControll.havePlayed);
            if (!playerControll.havePlayed)
            {
                currentPlayer = player;
                //canStartRoundTimer = true;
                StartCoroutine("RoundTimer");
            } else
            {
                playerControll.havePlayed = false;
            }
        });
    }
    void FindNextPlayerTeamBlue()
    {
        teamBlue.ForEach(player =>
        {
            PlayerController playerControll = player.GetComponent<PlayerController>();
            Debug.Log("player blue -> " + playerControll.havePlayed);
            if (!playerControll.havePlayed)
            {
                currentPlayer = player;
                //canStartRoundTimer = true;                
                StartCoroutine("RoundTimer");
            }
            else
            {
                playerControll.havePlayed = false;
            }
        });
    }
}
