using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(PanelData))] :絶対に設定してほしいコンポーネントを指定できる
public class BingoTest : MonoBehaviour
{
    [SerializeField] int _bingoSize = 5;
    [SerializeField] PanelData _panelPrefab;
    PanelData[] _bingoPanels;
    [SerializeField] int _bingoMaxNumber = 99;
    [SerializeField] Transform _bingoDataPanel;
    [SerializeField] GameObject _messgaeBing;
    [SerializeField] GameObject[] _reachBars;
    [SerializeField] Text text;
    int[] _bingoData;
    int _nowBingoNumber;
    private void Awake()
    {
        _bingoPanels = new PanelData[_bingoSize * _bingoSize];
    }
    void Start()
    {
        text.text = "";
        _messgaeBing.SetActive(false);
        foreach (var item in _reachBars)
        {
            item.SetActive(false);
        }
        _bingoData = new int[_bingoMaxNumber];
        for (int i = 0; i < _bingoMaxNumber; i++)
        {
            _bingoData[i] = i + 1;
        }
        for (int b = 0; b < _bingoMaxNumber; b++)
        {
            int r = Random.Range(0, _bingoMaxNumber);
            int c = _bingoData[b];
            _bingoData[b] = _bingoData[r];
            _bingoData[r] = c;
        }
        Transform canvas = GameObject.Find("BingoPanel").transform;
        for (int i = 0; i < _bingoSize; i++)
        {
            for (int k = 0; k < _bingoSize; k++)
            {
                var panel = Instantiate(_panelPrefab);
                panel.transform.SetParent(canvas);
                _bingoPanels[i * _bingoSize + k] = panel;
                if ((_bingoSize + 1)/2 == i + 1 && (_bingoSize + 1) / 2 == k + 1)
                {
                    continue;
                }
                _bingoPanels[i * _bingoSize + k].SetData(_bingoData[i * _bingoSize + k]);
            }
        }
    }

    public void OnClickBingoStart()
    {
        if (_nowBingoNumber == 0)
        {
            _bingoPanels[_bingoSize / 2 + _bingoSize * (_bingoSize / 2)].OpenThis();
            _bingoData = new int[_bingoMaxNumber];
            for (int i = 0; i < _bingoMaxNumber; i++)
            {
                _bingoData[i] = i + 1;
            }
            for (int b = 0; b < _bingoMaxNumber; b++)
            {
                int r = Random.Range(0, _bingoMaxNumber);
                int c = _bingoData[b];
                _bingoData[b] = _bingoData[r];
                _bingoData[r] = c;
            }
            var panel = Instantiate(_panelPrefab);
            panel.SetData2(_bingoData[_nowBingoNumber]);
            panel.transform.SetParent(_bingoDataPanel);
            Debug.Log(_bingoData[_nowBingoNumber] + "がでた");
            CheckBingo(_bingoData[_nowBingoNumber]);
            _nowBingoNumber++;
        }
        else if (_nowBingoNumber < _bingoMaxNumber)
        {
            var panel = Instantiate(_panelPrefab);
            panel.SetData2(_bingoData[_nowBingoNumber]);
            panel.transform.SetParent(_bingoDataPanel);
            Debug.Log(_bingoData[_nowBingoNumber] + "がでた");
            CheckBingo(_bingoData[_nowBingoNumber]);
            _nowBingoNumber++;
        }
    }
    void CheckBingo(int number)
    {
        foreach (var item in _bingoPanels)
        {
            if (item.PanelNumber == number)
            {
                item.OpenThis();
            }
        }
        if (_nowBingoNumber >= _bingoSize - 1)
        {
            int reachCount = 0;
            int x = CheckBingoLineX();
            if (Bingo(x))
            {
                _reachBars[0].SetActive(true);
                reachCount++;
            }
            x = CheckBingoLineX2();
            if (Bingo(x))
            {
                _reachBars[1].SetActive(true);
                reachCount++;
            }
            for (int i = 0; i < _bingoSize; i++)
            {
                x = CheckBingoLineY(i);
                if (Bingo(x))
                {
                    _reachBars[i + 2].SetActive(true);
                    reachCount++;
                }
            }
            for (int i = 0; i < _bingoSize * _bingoSize; i += _bingoSize)
            {
                x = CheckBingoLineY2(i);
                if (Bingo(x))
                {
                    _reachBars[i / _bingoSize + _bingoSize + 2].SetActive(true);
                    reachCount++;
                }
            }
            if (reachCount > 0)
            {
                if (reachCount == 1)
                {
                    text.text = "Reach！";
                }
                else if (reachCount == 2)
                {
                    text.text = "DoubleReach！";
                }
                else if (reachCount == 3)
                {
                    text.text = "TripleReach！";
                }
                else
                {
                    text.text = reachCount + "Reach！";
                }
            }
        }
    }
    bool Bingo(int x)
    {
        if (x >= _bingoSize - 1)
        {
            if (x == _bingoSize)
            {
                //Debug.Log("Bingo！");
                _messgaeBing.SetActive(true);                
            }
            else if(x == _bingoSize - 1)
            {
                //Debug.Log("Reach！");
                return true;
            }
        }
        return false;
    }
    int CheckBingoLineX()
    {
        int count = 0;        
        for (int i = 0; i < _bingoPanels.Length; i += _bingoSize + 1)
        {
            if (_bingoPanels[i].OpenThisMark)
            {
                count++;
            }
        }
        return count;
    }
    int CheckBingoLineX2()
    {
        int count = 0;
        for (int i = _bingoSize - 1; i < _bingoPanels.Length - 1; i += _bingoSize - 1)
        {
            if (_bingoPanels[i].OpenThisMark)
            {
                count++;
            }
        }
        return count;
    }
    int CheckBingoLineY(int lineNumber)
    {
        int count = 0;
        for (int i = lineNumber; i < _bingoPanels.Length; i += _bingoSize)
        {
            if (_bingoPanels[i].OpenThisMark)
            {
                count++;
            }
        }
        return count;
    }
    int CheckBingoLineY2(int lineNumber)
    {
        int count = 0;
        for (int i = lineNumber; i < _bingoSize + lineNumber; i++)
        {
            if (_bingoPanels[i].OpenThisMark)
            {
                count++;
            }
        }
        return count;
    }
}
