using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using DefaultNamespace;

public class GroundManager : MonoBehaviour
{
    [SerializeField] private GroundSpawner _spawner;
    [SerializeField] private GroundMover _mover;
    [SerializeField] private GroundRecycler _recycler;
    [SerializeField] private BackgroundManager _background;
    [SerializeField] private PlayerSpeed _playerSpeed;

    public GroundMover Mover => _mover;
    public GroundSpawner Spawner => _spawner;
    public PlayerSpeed PlayerSpeed => _playerSpeed;
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

    public void SetMoveSpeed(float speed)
    {
        _mover.SetSpeed(speed);
    }
}

