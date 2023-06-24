using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpawnManager : MonoBehaviour
{
    [Header(DS_Constants.DO_NOT_ASSIGN)]
    [SerializeField] private ObjectPooler objectPooler;

    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] private float spawnRate;
    [SerializeField] private Transform[] MagnetTransforms;

    private void Start()
    {
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(objectPooler.playerMosMagnetSO);
        StartCoroutine(SpawnCoroutine());
    }
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnMagnet();
        }
    }
    private void SpawnMagnet()
    {
        Transform randT = MagnetTransforms[Random.Range(0, MagnetTransforms.Length)];
        objectPooler.SpawnFromPool(objectPooler.playerMosMagnetSO.pool.tag, 
            new Vector3(randT.position.x, randT.position.y), Quaternion.identity);
    }
}
