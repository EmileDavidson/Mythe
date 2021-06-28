using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private List<ParticleSystem> playerParticle = new List<ParticleSystem>();
    private int _currentPlayer = 0;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    
    //switch timer
    [SerializeField] private float cooldown;
    [SerializeField] private float currentTime;
    [SerializeField] private bool switched = false;

    public UnityEvent allPlayersDied = new UnityEvent();
    private void Start()
    {
        //disable all players an enable current player.
        if (players.Count > 0)
        {
            foreach (GameObject player in players) player.gameObject.SetActive(false);
            players[_currentPlayer]?.SetActive(true);
        }
        
        //set the camera to follow current player.
        if (players[_currentPlayer] == null) return;
        cinemachineCamera.Follow = players[_currentPlayer].transform;
        cinemachineCamera.LookAt = players[_currentPlayer].transform;
    }

    private void Update()
    {
        if (switched)
        {
            currentTime += Time.deltaTime;
            if (!(currentTime >= cooldown)) return;
            switched = false;
            currentTime = 0;
        }
        //switching current player 
        if (players.Count <= 0) return;
        if (Input.GetKeyDown(KeyCode.DownArrow)) SwitchPlayerTo(_currentPlayer - 1);
        if (Input.GetKeyDown(KeyCode.UpArrow)) SwitchPlayerTo(_currentPlayer + 1);
    }

    private bool IsPlayerAlive()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<Health>().health > 0) return true;
        }

        return false;
    }

    private void SwitchPlayerTo(int newPlayer)
    {
        //check if there is a player alive.
        if (!IsPlayerAlive())
        {
            NoAlivePlayerFound();
            return;
        }
        
        //making sure the player exists and isn't dead
        if (newPlayer > players.Count - 1) newPlayer = 0;
        if (newPlayer < 0) newPlayer = players.Count - 1;

        //IF PLAYER IS DEAD CHECK FOR OTHER PLAYER 
        if (players[newPlayer].GetComponent<Health>().health <= 0)
        {
            //player is dead. find first new one
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetComponent<Health>().health <= 0) continue;
                newPlayer = i;
            }
        }
        
        FixPlayerInfo(newPlayer);
    }

    public void SwitchPlayerToNext()
    {
        SwitchPlayerTo(_currentPlayer + 1);
    }

    private void NoAlivePlayerFound()
    {
        allPlayersDied.Invoke();
    }

    private void FixPlayerInfo(int newPlayer)
    {
        //fixing camera position and new player position.
        players[newPlayer].gameObject.transform.position = players[_currentPlayer].transform.position;
        cinemachineCamera.Follow = players[newPlayer].transform;
        cinemachineCamera.LookAt = players[newPlayer].transform;
        //fixing player rotation
        players[newPlayer].gameObject.transform.rotation = players[_currentPlayer].gameObject.transform.rotation;
        
        //fixing last land position
        players[newPlayer].gameObject.GetComponent<PlayerMovement>().lastLandPosition =
            players[_currentPlayer].gameObject.GetComponent<PlayerMovement>().lastLandPosition;
        
        //disable old player enable new player
        players[_currentPlayer].gameObject.SetActive(false);
        players[newPlayer].gameObject.SetActive(true);
        
        //particle
        if (playerParticle[newPlayer] != null)
        {
            playerParticle[newPlayer].Play();
        }
        
        //fixing current player to be the latest.
        _currentPlayer = newPlayer;
        switched = true;
    }

    public GameObject GetCurrentPlayer()
    {
        return players[_currentPlayer].gameObject;
    }
}
