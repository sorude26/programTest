using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelData : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] GameObject _openMark;
    public bool OpenThisMark { get; private set; }
    public int PanelNumber { get; private set; }
    private void Start()
    {
        _openMark.SetActive(false);
    }
    public void SetData(int data)
    {
        PanelNumber = data;
        _text.text = data.ToString();
        _text.fontSize = 80;
    }
    public void OpenThis()
    {
        OpenThisMark = true;
        _openMark.SetActive(true);
    }
    public void SetData2(int data)
    {
        PanelNumber = data;
        _text.text = data.ToString();
        _text.fontSize = 50;
    }
}
