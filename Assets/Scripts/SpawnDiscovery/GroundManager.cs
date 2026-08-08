using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using DefaultNamespace;

public class GroundManager : MonoBehaviour
{
     private GroundSpawner _spawner;
     private GroundMover _mover;
     private GroundRecycler _recycler;
     private BackgroundManager _background;
     private PlayerSpeed _playerSpeed;
    

    private void Awake()
    {
        _spawner = GetComponent<GroundSpawner>();
        _mover = GetComponent<GroundMover>();
        _recycler = GetComponent<GroundRecycler>();
        _background = GetComponent<BackgroundManager>();
        _playerSpeed = GetComponent<PlayerSpeed>();
    }

    void Start()
    {
        _spawner.Initialize();
    }

    void Update()
    {
        _mover.Tick();
        _background.Tick();
        _recycler.Tick();
    }
    
}

