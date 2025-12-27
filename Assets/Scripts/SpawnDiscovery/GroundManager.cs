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

    public GroundMover Mover => _mover;
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
    
    // [SerializeField] private ObjectPool groundPrefab;
    // [SerializeField] private ObjectPool _itemPool;
    // [SerializeField] private ObjectPool _enemyPool;
    // [SerializeField] private ObjectPool _backPool;
    // [SerializeField] private int initialGrounds = 5;
    // [SerializeField] private float moveSpeed = 5f;
    // [SerializeField] private float moveSpeedBack = 2f;
    //
    // [Header("X Spawn")] private Vector2 Xspawning = new Vector2(1, 3);
    //
    // [Header("Y Spawn")] private Vector2 Yspawning = new Vector2(-1, 1);
    //
    // private List<GameObject> grounds = new List<GameObject>();
    // private List<GameObject> Backs = new List<GameObject>();
    // public float MoveSpeed => moveSpeed;
    // void Start()
    // {
    //     float spawnX = 0f;
    //     for (int i = 0; i < initialGrounds; i++)
    //     {
    //         float yPos = Random.Range(Yspawning.x, Yspawning.y);
    //         GameObject go = SpawnGround(spawnX, yPos, true);
    //         float spacing = Random.Range(Xspawning.x, Xspawning.y);
    //         spawnX += GetWidth(go) + spacing;
    //     }
    // }
    //
    // void Update()
    // {
    //     MoveGrounds();
    //     MoveBacks();
    //     CheckForRecycle();
    //     CheckForRecycleBack();
    // }
    //
    // private void MoveGrounds()
    // {
    //     for (int i = 0; i < grounds.Count; i++)
    //     {
    //         grounds[i].transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    //     }
    // }
    //
    // private void MoveBacks()
    // {
    //     for (int i = 0; i < Backs.Count; i++)
    //     {
    //         Backs[i].transform.Translate(Vector3.left * moveSpeedBack * Time.deltaTime);
    //     }
    // }
    //
    // private GameObject SpawnGround(float xPos, float yPos, bool check)
    // {
    //     GameObject newGround = groundPrefab.GetFromPool();
    //     if (newGround != null)
    //     {
    //         newGround.transform.position = new Vector3(xPos, yPos, 0);
    //         newGround.SetActive(true);
    //         grounds.Add(newGround);
    //         SpawnBack(xPos, yPos);
    //         for (int i = 0; i < newGround.transform.childCount; i++)
    //         {
    //             newGround.transform.GetChild(i).gameObject.SetActive(false);
    //             newGround.transform.GetChild(i).SetParent(transform);
    //         }
    //
    //         SpawnItem(newGround, _itemPool, ItemType.item);
    //         if (!check) SpawnItem(newGround, _enemyPool, ItemType.enemy);
    //     }
    //
    //     return newGround;
    // }
    //
    // private void SpawnBack(float xPos, float yPos)
    // {
    //     GameObject newBack = _backPool.GetFromPool();
    //     if (newBack != null)
    //     {
    //         newBack.transform.position = new Vector3(xPos, yPos + (Random.Range(3, 8)), 0);
    //         newBack.SetActive(true);
    //         Backs.Add(newBack);
    //     }
    // }
    //
    // private void SpawnItem(GameObject newGround, ObjectPool pool, ItemType type)
    // {
    //     var chanceExist = Random.Range(0f, 1f);
    //     if (chanceExist > 0.5f) return;
    //     var chancePos = Random.Range(0f, 1f);
    //     float xpos = newGround.transform.position.x;
    //     if (chancePos < 0.35) xpos = newGround.transform.position.x; //middle
    //     else if (chancePos > 0.35 && chancePos < 0.7)
    //         xpos = newGround.transform.position.x + (GetWidth(newGround) / 2f) - 2; //left
    //     else if (chancePos > 0.7) xpos = newGround.transform.position.x - (GetWidth(newGround) / 2f) + 2; //right
    //     var item = pool.GetFromPool();
    //     if (item != null)
    //     {
    //         item.SetActive(true);
    //         item.transform.SetParent(newGround.transform);
    //         if (type== ItemType.enemy)
    //         {
    //             var data = item.GetComponent<FlyingDamage>().Data;
    //             item.transform.position = new Vector3(xpos, newGround.transform.position.y + data.ypos);
    //         }
    //         else item.transform.position = new Vector3(xpos, newGround.transform.position.y + 3);
    //         if (type== ItemType.item) item.transform.DOScale(new Vector3(0.3f, 0.6f, 0.3f), 0.01f);
    //     }
    // }
    //
    // private void CheckForRecycle()
    // {
    //     if (grounds.Count <= 0) return;
    //     GameObject firstGround = grounds[0];
    //     GameObject lastGround = grounds[grounds.Count - 1];
    //     float rightEdge = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
    //     if (lastGround.transform.position.x + GetWidth(lastGround) / 2 < rightEdge + GetWidth(groundPrefab.gameObject))
    //     {
    //         float spacing = Random.Range(Xspawning.x, Xspawning.y);
    //         float spawnX = lastGround.transform.position.x + GetWidth(lastGround) + spacing;
    //         float yPos = Random.Range(Yspawning.x, Yspawning.y);
    //         firstGround.SetActive(false);
    //         grounds.RemoveAt(0);
    //         SpawnGround(spawnX, yPos, false);
    //     }
    // }
    //
    // private void CheckForRecycleBack()
    // {
    //     if (Backs.Count <= 0) return;
    //     GameObject firstBack = Backs[0];
    //     GameObject lastBack = Backs[Backs.Count - 1];
    //     float rightEdge = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
    //     if (_backPool != null && lastBack.transform.position.x + GetWidth(lastBack) / 2 <
    //         rightEdge + GetWidth(_backPool.gameObject))
    //     {
    //         float spacing = Random.Range(Xspawning.x, Xspawning.y);
    //         float spawnX = lastBack.transform.position.x + GetWidth(lastBack) + spacing;
    //         float yPos = Random.Range(Yspawning.x, Yspawning.y);
    //         firstBack.SetActive(false);
    //         Backs.RemoveAt(0);
    //         SpawnBack(spawnX, yPos);
    //     }
    // }
    //
    // float GetWidth(GameObject obj)
    // {
    //     Ground sr = obj.GetComponent<Ground>();
    //     // Debug.Log(           sr.GetComponent<SpriteRenderer>().bounds.size.x);
    //     if (sr != null)
    //         return sr.Data.width;
    //     else
    //         return 10f;
    // }
    //
    // public void SetMoveSpeed(float speed)
    // {
    //     moveSpeed = speed;
    // }
}

