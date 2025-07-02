using UnityEngine;

public class LevelBtn : MonoBehaviour
{
    [SerializeField] private int _iLevel = 0;
    [SerializeField] private GameObject _lockObj;

    private void Start()
    {
        int latestUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        _lockObj.SetActive(_iLevel > latestUnlocked);
    }
}
