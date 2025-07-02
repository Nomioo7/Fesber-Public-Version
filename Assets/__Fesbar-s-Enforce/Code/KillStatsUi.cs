using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KillStatsUi : MonoBehaviour
{
    [SerializeField] private Text _killCountText;
    [SerializeField] private int _targetKills = 5;

    private int _currentlyKilled = 0;

    public UnityEvent OnTargetAchieved;


    public void OnEnemyKilled()
    {
        _currentlyKilled++;
        _killCountText.text = $"{_currentlyKilled}";
        if (_currentlyKilled == _targetKills)
        {
            OnTargetAchieved?.Invoke();
        }
    }
}
