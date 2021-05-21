using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    private int _currentPlayer = 0;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    
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
        //switching current player 
        if (players.Count <= 0) return;
        if (Input.GetKeyDown(KeyCode.DownArrow)) SwitchPlayerTo(_currentPlayer - 1);
        if (Input.GetKeyDown(KeyCode.UpArrow)) SwitchPlayerTo(_currentPlayer + 1);
    }

    private void SwitchPlayerTo(int newPlayer)
    {
        //making sure the player exists
        if (newPlayer > players.Count - 1) newPlayer = 0;
        if (newPlayer < 0) newPlayer = players.Count - 1;

        //fixing camera position and new player position.
        players[newPlayer].gameObject.transform.position = players[_currentPlayer].transform.position;
        cinemachineCamera.Follow = players[newPlayer].transform;
        cinemachineCamera.LookAt = players[newPlayer].transform;
        //fixing player rotation
        players[newPlayer].gameObject.transform.rotation = players[_currentPlayer].gameObject.transform.rotation;
        
        //disable old player enable new player
        players[_currentPlayer].gameObject.SetActive(false);
        players[newPlayer].gameObject.SetActive(true);
        
        //fixing current player to be the latest.
        _currentPlayer = newPlayer;
    }
}
