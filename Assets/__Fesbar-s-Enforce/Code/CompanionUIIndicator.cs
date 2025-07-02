using UnityEngine;

public class CompanionUIIndicator : MonoBehaviour
{
    [SerializeField] private Companion companionScript;
    [SerializeField] private GameObject companionReadyIcon; 

    private void Update()
    {
        companionReadyIcon.SetActive(companionScript.CanSpawn);
    }
}
