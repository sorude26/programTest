using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeControl : MonoBehaviour
{
    public static SceneChangeControl Instance { get; private set; }
    [SerializeField] Image _fadePanel;
    [SerializeField] float _fadeSpeed = 2f;
    string _targetScene = "";
    bool _fadeNow;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(StartFadeIn());
    }
    public void SceneChange(string targetScene)
    {
        if (_fadeNow)
        {
            return;
        }
        _targetScene = targetScene;
        _fadeNow = true;
        StartCoroutine(StartFadeOut());
    }
    IEnumerator StartFadeIn()
    {
        _fadePanel.gameObject.SetActive(true);
        _fadePanel.color = Color.black;
        float clearScale = 1f;
        while (clearScale > 0)
        {
            clearScale -= _fadeSpeed * Time.deltaTime;
            if (clearScale <= 0)
            {
                clearScale = 0;
            }
            _fadePanel.color = new Color(0, 0, 0, clearScale);
            yield return new WaitForEndOfFrame();
        }
        _fadePanel.gameObject.SetActive(false);
    }
    IEnumerator StartFadeOut()
    {
        _fadePanel.gameObject.SetActive(true);
        _fadePanel.color = Color.clear;
        float clearScale = 0f;
        while (clearScale < 1)
        {
            clearScale += _fadeSpeed * Time.deltaTime;
            if (clearScale >= 1)
            {
                clearScale = 1;
            }
            _fadePanel.color = new Color(0, 0, 0, clearScale);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(_targetScene);
    }
}
