using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Multiplayer : NetworkBehaviour
{
    public static Multiplayer Instance;
    public float WhoPlays = 0;
    public GameObject _playerOne;
    public GameObject _playerTwo;
    void Start()
    {
        Instance = this;
        Time.timeScale = 1f;
    }
}
