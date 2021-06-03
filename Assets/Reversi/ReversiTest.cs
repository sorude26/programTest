using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReversiTest : MonoBehaviour
{
    [SerializeField] ReversiPice _prefab;
    [SerializeField] Image _turnColor;
    const int _size = 8;
    ReversiPice[,] _pice = new ReversiPice[_size, _size];
    bool _myTurn = default;
    int[,] _picelData = new int[_size, _size];
    public PiceColor TurnColor { get; private set; } = PiceColor.None;
    public struct PiceDir
    {
        public int X { get; private set; }
        public int Z { get; private set; }
        public PiceDir(int x, int z)
        {
            if (x > 0)
            {
                X = 1;
            }
            else if (x < 0)
            {
                X = -1;
            }
            else
            {
                X = 0;
            }
            if (z > 0)
            {
                Z = 1;
            }
            else if (z < 0)
            {
                Z = -1;
            }
            else
            {
                Z = 0;
            }
        }
    }

    List<List<ReversiPice>> reversiPices = new List<List<ReversiPice>>();
    void Start()
    {
        for (int i = 0; i < _size; i++)
        {
            reversiPices.Add(new List<ReversiPice>());
        }
        for (int z = 0; z < _size; z++)
        {
            for (int x = 0; x < _size; x++)
            {
                _pice[x, z] = Instantiate(_prefab);
                _pice[x, z].transform.SetParent(this.transform);
                _pice[x, z].transform.position = new Vector3(x, 0, z);
                _pice[x, z].SetReversiMaster(this, x, z);
                if (z <= _size / 2 && z >= _size / 2 - 1 && x <= _size / 2 && x >= _size / 2 - 1)
                {
                    if (x != z)
                    {
                        _pice[x, z].ChangeBlack();
                    }
                    else
                    {
                        _pice[x, z].ChangeWhite();
                    }
                }
            }
        }
    }
    void PiceDataReset()
    {
        for (int z = 0; z < _size; z++)
        {
            for (int x = 0; x < _size; x++)
            {
                _pice[x, z].TouchNG();
                _picelData[x, z] = 0;
            }
        }
    }
    public void OnClickGameStart()
    {
        TouchPointSearch();
    }
    public void TouchPointSearch()
    {
        if (TurnColor == PiceColor.White)
        {
            _myTurn = false;
            _turnColor.color = Color.black;
            TurnColor = PiceColor.Black;
        }
        else
        {
            _myTurn = true;
            _turnColor.color = Color.white;
            TurnColor = PiceColor.White;
        }
        int count = 0;
        for (int z = 0; z < _size; z++)
        {
            for (int x = 0; x < _size; x++)
            {
                if (_pice[x, z].PiceColor == TurnColor)
                {
                    PiceDir checkDir = new PiceDir(1, 0);
                    if (CheckNeighorPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0)) { count++; }
                    checkDir = new PiceDir(0, 1);
                    if (CheckNeighorPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0)) { count++; }
                    checkDir = new PiceDir(1, 1);
                    if (CheckNeighorPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0)) { count++; }
                    checkDir = new PiceDir(1, -1);
                    if (CheckNeighorPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0)) { count++; }
                    checkDir = new PiceDir(-1, 1);
                    if (CheckNeighorPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0)) { count++; }
                    checkDir = new PiceDir(-1, -1);
                    if (CheckNeighorPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0)) { count++; }
                    checkDir = new PiceDir(-1, 0);
                    if (CheckNeighorPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0)) { count++; }
                    checkDir = new PiceDir(0, -1);
                    if (CheckNeighorPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0)) { count++; }
                }
            }
        }
        if (count == 0)
        {
            Debug.Log("パス");
        }
        int white = 0;
        int black = 0;
        foreach (var item in _pice)
        {
            if (item.PiceColor == PiceColor.None)
            {
                return;
            }
            if (item.PiceColor == PiceColor.Black)
            {
                black++;
            }
            else
            {
                white++;
            }
        }
        Debug.Log("ゲーム終了、白：" + white + " 黒：" + black);
    }
    void TouchPointSearch0()
    {
        for (int z = 0; z < _size; z++)
        {
            for (int x = 0; x < _size; x++)
            {
                if (_pice[x, z].PiceColor != PiceColor.None)
                {
                    continue;
                }
                var pice = CheckNeighborPice(x, z);
                foreach (var item in pice)
                {
                    if (item.PiceColor != PiceColor.None)
                    {

                    }
                }
            }
        }
    }


    bool CheckNeighorPice(int pointX, int pointZ, PiceDir checkDir, PiceColor checkColor, int count)
    {
        if (pointX < 0 || pointX >= _size || pointZ < 0 || pointZ >= _size)
        {
            return false;
        }
        PiceColor piceColor = _pice[pointX, pointZ].PiceColor;
        if (piceColor == PiceColor.None && count > 0)
        {
            _pice[pointX, pointZ].TouchOK();
            return true;
        }
        else if (piceColor != PiceColor.None && piceColor != checkColor)
        {
            return CheckNeighorPice(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, checkColor, 1);
        }
        return false;
    }
    public void ChangeColorNeighorAround2(int pointX, int pointZ)
    {
        foreach (var item in reversiPices)
        {
            item.Clear();
        }
        PiceDir checkDir = new PiceDir(1, 0);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0, reversiPices[0]);
        checkDir = new PiceDir(0, 1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0, reversiPices[1]);
        checkDir = new PiceDir(1, 1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0, reversiPices[2]);
        checkDir = new PiceDir(1, -1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0, reversiPices[3]);
        checkDir = new PiceDir(-1, 1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0, reversiPices[4]);
        checkDir = new PiceDir(-1, -1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0, reversiPices[5]);
        checkDir = new PiceDir(-1, 0);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0, reversiPices[6]);
        checkDir = new PiceDir(0, -1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0, reversiPices[7]);
        StartCoroutine(ChangeColor());
    }
    bool ChangeColorNeighor(int pointX, int pointZ, PiceDir checkDir, PiceColor checkColor, int count, List<ReversiPice> pices)
    {
        if (pointX < 0 || pointX >= _size || pointZ < 0 || pointZ >= _size)
        {
            return false;
        }
        PiceColor piceColor = _pice[pointX, pointZ].PiceColor;
        if (piceColor == PiceColor.None || count > _size)
        {
            return false;
        }
        if (piceColor == checkColor)
        {
            return true;
        }
        bool change = ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, checkColor, count++, pices);
        if (change)
        {
            pices.Add(_pice[pointX, pointZ]);
        }
        return change;
    }
    public IEnumerator ChangeColor()
    {
        PiceDataReset();
        int i = _size - 1;
        while (i >= 0)
        {
            float w = 0;
            for (int k = 0; k < _size; k++)
            {
                if (reversiPices[k].Count > 0)
                {
                    if (reversiPices[k].Count > i)
                    {
                        reversiPices[k][i].ChangeColor();
                        w = 1;
                    }
                }
            }
            i--;
            yield return new WaitForSeconds(w * 0.5f);
        }
        TouchPointSearch();
    }
    IEnumerable<ReversiPice> CheckNeighborPice(int posX, int posZ)
    {
        int top = posZ + 1;
        int bottom = posZ - 1;
        int left = posX - 1;
        int right = posX + 1;
        if (top >= 0)
        {
            if (left > 0)
            {
                yield return _pice[top, left];
            }
            yield return _pice[top, posZ];
            if (right < posZ)
            {
                yield return _pice[top, right];
            }
        }
        if (left > 0)
        {
            yield return _pice[posX, left];
        }
        yield return _pice[posX, posZ];
        if (right < posZ)
        {
            yield return _pice[posX, right];
        }
        if (bottom < posX)
        {
            if (left > 0)
            {
                yield return _pice[bottom, left];
            }
            yield return _pice[bottom, posZ];
            if (right < posZ)
            {
                yield return _pice[bottom, right];
            }
        }
    }
}
