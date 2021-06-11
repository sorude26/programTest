using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleControl : MonoBehaviour
{
    [SerializeField] GameObject _buttonPanel;
    [SerializeField] GameObject _startButton;

    private void Start()
    {
        _buttonPanel.SetActive(false);
    }
    public void OnClickStart()
    {
        _buttonPanel.SetActive(true);
        _startButton.SetActive(false);
    }
    public void OnClickGame(string name)
    {
        SceneChangeControl.Instance.SceneChange(name);
    }
}
