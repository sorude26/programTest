using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomTest : MonoBehaviour
{
    public static BomTest Instance { get; private set; }
    [SerializeField] Cell _prefb;
    [SerializeField] int _size = 10;
    [SerializeField] int _bomNumber = 8;
    [SerializeField] GameObject _bom;
    [SerializeField] GameObject _clear;
    [SerializeField] GameObject _restartButton;
    int[] _data;
    Cell[] _cells;
    bool _start;
    public bool ExplosionBom { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _restartButton.SetActive(false);
        Transform canvas = GameObject.Find("Canvas").transform;
        _data = new int[_size * _size];
        _cells = new Cell[_size * _size];
        for (int i = 0; i < _size; i++)
        {
            for (int k = 0; k < _size; k++)
            {
                var cell = Instantiate(_prefb);
                cell.transform.SetParent(canvas);
                cell.GetComponent<RectTransform>().localPosition = new Vector2(k * 101 - _size * 50, i * 101 - _size * 50 + 50);
                cell.CellState = CellState.None;
                cell.SellID = i * _size + k;
                _cells[i * _size + k] = cell;
            }
        } 
    }
    void SetMine(int stratPos)
    {
        DataReset();
        for (int a = 0; a < _bomNumber; a++)
        {
            BomSet(stratPos);
        }
        for (int w = 0; w < _size * _size; w++)
        {
            if (_data[w] < 0)
            {
                DataSet(w);
            }
        }
        for (int v = 0; v < _size * _size; v++)
        {
            _cells[v].CellState = (CellState)_data[v];
        }
        AroundCheck(stratPos);
    }
    void DataReset()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            _data[i] = 0;
        }
    }
    public void BomSet(int startPos)
    {
        int bom = Random.Range(0, _size * _size);
        if (bom == startPos)
        {
            BomSet(startPos);
        }
        else if (_data[bom] < 0)
        {
            BomSet(startPos);
        }
        else
        {
            _data[bom] = -1;
        }
    }
    void DataSet(int bom)
    {
        if (bom - _size >= 0)
        {
            if (_data[bom - _size] >= 0)
            {
                _data[bom - _size]++;
            }
        }
        if (bom - _size - 1 >= 0 && bom % _size != 0)
        {
            if (_data[bom - _size - 1] >= 0)
            {
                _data[bom - _size - 1]++;
            }
        }
        if (bom - _size + 1 >= 0 && (bom + 1) % _size != 0)
        {
            if (_data[bom - _size + 1] >= 0)
            {
                _data[bom - _size + 1]++;
            }
        }
        if (bom - 1 >= 0 && bom % _size != 0)
        {
            if (_data[bom - 1] >= 0)
            {
                _data[bom - 1]++;
            }
        }
        if (bom + 1 < _size * _size && (bom + 1) % _size != 0)
        {
            if (_data[bom + 1] >= 0)
            {
                _data[bom + 1]++;
            }
        }
        if (bom + _size < _size * _size)
        {
            if (_data[bom + _size] >= 0)
            {
                _data[bom + _size]++;
            }
        }
        if (bom + _size - 1 < _size * _size && bom % _size != 0)
        {
            if (_data[bom + _size - 1] >= 0)
            {
                _data[bom + _size - 1]++;
            }
        }
        if (bom + _size + 1 < _size * _size && (bom + 1) % _size != 0)
        {
            if (_data[bom + _size + 1] >= 0)
            {
                _data[bom + _size + 1]++;
            }
        }
    }
    public void AroundCheck(int cellPos)
    {
        if (!_start)
        {
            _start = true;
            SetMine(cellPos);
            return;
        }        
        if (_cells[cellPos].CellState != CellState.None)
        {
            return;
        }
        //8•ûŒüŒŸõ‚O‚È‚ç‘±s,”‚ª‚ ‚é‚È‚ç’âŽ~
        if (cellPos - _size >= 0)
        {
            CellCheck(cellPos - _size);
        }
        if (cellPos - _size - 1 >= 0 && cellPos % _size != 0)
        {
            CellCheck(cellPos - _size - 1);
        }
        if (cellPos - _size + 1 >= 0 && (cellPos + 1) % _size != 0)
        {
            CellCheck(cellPos - _size + 1);
        }
        if (cellPos - 1 >= 0 && cellPos % _size != 0)
        {
            CellCheck(cellPos - 1);
        }
        if (cellPos + 1 < _size * _size && (cellPos + 1) % _size != 0)
        {
            CellCheck(cellPos + 1);
        }
        if (cellPos + _size < _size * _size)
        {
            CellCheck(cellPos + _size);
        }
        if (cellPos + _size - 1 < _size * _size && cellPos % _size != 0)
        {
            CellCheck(cellPos + _size - 1);
        }
        if (cellPos + _size + 1 < _size * _size && (cellPos + 1) % _size != 0)
        {
            CellCheck(cellPos + _size + 1);
        }
    }
    void CellCheck(int checkPos)
    {
        if (_cells[checkPos].Check)
        {
            return;
        }
        _cells[checkPos].OpenThis();
        if (_cells[checkPos].CellState == CellState.None)
        {
            AroundCheck(checkPos);
        }
        return;
    }
    public void Explosion()
    {
        _bom.SetActive(true);
        ExplosionBom = true;
        EventManager.GameEnd();
        _restartButton.SetActive(true);
    }
    public void ClearCheck()
    {
        if (ExplosionBom)
        {
            return;
        }
        int clearNumber = 0;
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].Check)
            {
                clearNumber++;
            }
        }
        if (clearNumber >= _size * _size - _bomNumber)
        {
            ExplosionBom = true;
            _clear.SetActive(true);
            EventManager.GameClear();
            _restartButton.SetActive(true);
        }
    }
    public void OnClickRestart()
    {
        EventManager.Restart();
        _start = false;
        ExplosionBom = false;
        _clear.SetActive(false);
        _bom.SetActive(false);
        _restartButton.SetActive(false);
    }
}
