using JUTPS;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Image _loadingFiller;
    [SerializeField] private Text _loadingAmtText;

    private int _latestUnlockedLevel = 1;
    private int _maxAllowdLevel = 5;

    public bool CanPause { private set; get; } = true;

    private void Start()
    {
        _latestUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    }

    public void UpdateCanPauseState(bool state)
    {
        CanPause = state;
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(LoadGameSceneCrt(sceneName));
    }

    IEnumerator LoadGameSceneCrt(string TargetScene)
    {
        _loadingFiller.fillAmount = 0;
        float progress = 0;
        _loadingPanel.SetActive(true);

        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(TargetScene.ToString());
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            progress = Mathf.MoveTowards(progress, operation.progress, 0.3f * Time.deltaTime);

            _loadingFiller.fillAmount = progress / 0.9f;
            _loadingAmtText.text = Mathf.CeilToInt((progress / 0.9f) * 100).ToString() + "%";

            if (_loadingFiller.fillAmount > 0.99f)
            {
                _loadingFiller.fillAmount = 1;
                _loadingAmtText.text = "100%";

                yield return new WaitForSeconds(1f);

                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    public void UnlockNextLevel()
    {
        if (_latestUnlockedLevel >= _maxAllowdLevel) return;

        _latestUnlockedLevel++;

        PlayerPrefs.SetInt("UnlockedLevel", _latestUnlockedLevel);
    }
}
