using System.Collections;
using UnityEngine;

public class Companion : MonoBehaviour
{
    [SerializeField] private GameObject companionPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float aliveTime = 30f;
    [SerializeField] private float spawnDistance = 1.5f;
    [SerializeField] private float raycastHeightOffset = 2f;
    [SerializeField] private LayerMask groundLayer;

    private GameObject spawnedCompanion;
    private bool alreadySpawned = false;
    public bool CanSpawn => !alreadySpawned;
    public void SpawnCompanion()
    {
        if (alreadySpawned) return;

        alreadySpawned = true;

        Vector3[] directions = new Vector3[]
        {
            playerTransform.right,                  // Right
            -playerTransform.right,                 // Left
            -playerTransform.forward,               // Behind
            playerTransform.forward                 // In Front
        };

        Vector3 spawnPos = Vector3.zero;
        bool foundPosition = false;

        foreach (Vector3 dir in directions)
        {
            Vector3 tryPos = playerTransform.position + dir.normalized * spawnDistance;
            Vector3 rayOrigin = tryPos + Vector3.up * raycastHeightOffset;

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, raycastHeightOffset * 2f, groundLayer))
            {
                spawnPos = hit.point;
                foundPosition = true;
                break;
            }
        }

        if (!foundPosition)
        {
            // fallback: spawn directly at player's feet
            spawnPos = playerTransform.position;
        }

        spawnedCompanion = Instantiate(companionPrefab, spawnPos, Quaternion.identity, transform);
        spawnedCompanion.SetActive(true);

        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(aliveTime);
        if (spawnedCompanion != null)
            Destroy(spawnedCompanion);

        alreadySpawned = false;
    }
}
