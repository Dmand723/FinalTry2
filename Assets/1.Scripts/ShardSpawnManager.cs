using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardSpawnManager : MonoBehaviour
{
    [Header("Shard Spawn Points")]
    [SerializeField] GameObject AtherSpawnPoints;
    Transform[] AtherSPTransforms;
    [SerializeField] GameObject IrisSpawnPoints;
    Transform[] IrisSPTransforms;
    [SerializeField] GameObject MyceliaSpawnPoints;
    Transform[] MyceliaSPTransforms;

    [Header("Shard Prefabs")]
    [SerializeField] GameObject Shard1Prefab;
    [SerializeField] GameObject Shard2Prefab;
    [SerializeField] GameObject Shard3Prefab;

    private void Awake()
    {
        AtherSPTransforms = AtherSpawnPoints.GetComponentsInChildren<Transform>();
        IrisSPTransforms = IrisSpawnPoints.GetComponentsInChildren<Transform>();
        MyceliaSPTransforms = MyceliaSpawnPoints.GetComponentsInChildren<Transform>();

        spawnShardsAther();
        spawnShardIris();
        spawnShardsMycelia();
    }
    void spawnShardsAther()
    {
        int randomIndex = Random.Range(1, AtherSPTransforms.Length);
        Vector3 spawnPosition = AtherSPTransforms[randomIndex].position;
        Instantiate(Shard1Prefab, spawnPosition, Quaternion.identity);
    }
    void spawnShardIris()
    {
        int randomIndex = Random.Range(1, IrisSPTransforms.Length);
        Vector3 spawnPosition = IrisSPTransforms[randomIndex].position;
        Instantiate(Shard2Prefab, spawnPosition, Quaternion.identity);
    }
    void spawnShardsMycelia()
    {
        int randomIndex = Random.Range(1, MyceliaSPTransforms.Length);
        Vector3 spawnPosition = MyceliaSPTransforms[randomIndex].position;
        Instantiate(Shard3Prefab, spawnPosition, Quaternion.identity);
    }
}
