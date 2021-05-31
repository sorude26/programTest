using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversiTest : MonoBehaviour
{
    [SerializeField] ReversiPice _prefab;
    const int _size = 8;
    ReversiPice[,] _pice = new ReversiPice[_size, _size];
    bool _myTurn;
    int[,] _picelData = new int[_size, _size];
    public PiceColor TurnColor { get; private set; } = PiceColor.None;
    public struct PiceDir
    {
        public int X { get; private set; }
        public int Z { get; private set; }
        public PiceDir(int x,int z)
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

    void Start()
    {
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
        _myTurn = true;
        TurnColor = PiceColor.White;
        TouchPointSearch();
    }
    public void TouchPointSearch()
    {
        if (TurnColor == PiceColor.Black)
        {
            TurnColor = PiceColor.White;
        }
        else
        {
            TurnColor = PiceColor.Black;
        }
        PiceDataReset();
        for (int z = 0; z < _size; z++)
        {
            for (int x = 0; x < _size; x++)
            {
                if (_pice[x,z].PiceColor == TurnColor)
                {
                    PiceDir checkDir = new PiceDir(1, 0);
                    TNeighborPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor,0);
                    checkDir = new PiceDir(0, 1);
                    TNeighborPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0);
                    checkDir = new PiceDir(1, 1);
                    TNeighborPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0);
                    checkDir = new PiceDir(1, -1);
                    TNeighborPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0);
                    checkDir = new PiceDir(-1, 1);
                    TNeighborPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0);
                    checkDir = new PiceDir(-1, -1);
                    TNeighborPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0);
                    checkDir = new PiceDir(-1, 0);
                    TNeighborPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0);
                    checkDir = new PiceDir(0, -1);
                    TNeighborPice(x + checkDir.X, z + checkDir.Z, checkDir, TurnColor, 0);
                }
            }
        }
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

    void CheckNeighborPice(int pointX,int pointZ, PiceDir checkDir ,PiceColor checkColor)
    {
        if (pointX < 0 || pointX >= _size || pointZ < 0 || pointZ >= _size)
        {
            return;
        }
        PiceColor piceColor = _pice[pointX, pointZ].PiceColor;
        if (piceColor == checkColor || _picelData[pointX, pointZ] > 0)
        {
            return;
        }
        if (piceColor == PiceColor.None)
        {
            TNeighborPice(pointX - checkDir.X * 2, pointZ - checkDir.Z * 2, checkDir, checkColor,0);
        }
        else
        {
            TNeighborPice(pointX - checkDir.X, pointZ - checkDir.Z, checkDir, checkColor, 0);
        }
    }
    void TNeighborPice(int pointX, int pointZ, PiceDir checkDir, PiceColor checkColor,int count)
    {
        if (pointX < 0 || pointX >= _size || pointZ < 0 || pointZ >= _size)
        {
            return;
        }
        PiceColor piceColor = _pice[pointX, pointZ].PiceColor;        
        if (piceColor == PiceColor.None && count > 0)
        {
            _pice[pointX, pointZ].TouchOK();
        }
        else if (piceColor != PiceColor.None && piceColor != checkColor)
        {
            TNeighborPice(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, checkColor,1);
        }
    }
    public void ChangeColorNeighorAround(int pointX, int pointZ)
    {
        PiceDir checkDir = new PiceDir(1, 0);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor,0);
        checkDir = new PiceDir(0, 1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0);
        checkDir = new PiceDir(1, 1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0);
        checkDir = new PiceDir(1, -1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0);
        checkDir = new PiceDir(-1, 1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0);
        checkDir = new PiceDir(-1, -1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0);
        checkDir = new PiceDir(-1, 0);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0);
        checkDir = new PiceDir(0, -1);
        ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, TurnColor, 0);
    }
    bool ChangeColorNeighor(int pointX, int pointZ, PiceDir checkDir, PiceColor checkColor ,int count)
    {
        if (pointX < 0 || pointX >= _size || pointZ < 0 || pointZ >= _size)
        {
            return false;
        }
        PiceColor piceColor = _pice[pointX, pointZ].PiceColor;
        if (piceColor == PiceColor.None || count >= _size)
        {
            return false;
        }        
        if (piceColor == checkColor)
        {
            return true;
        }  
        bool color = ChangeColorNeighor(pointX + checkDir.X, pointZ + checkDir.Z, checkDir, checkColor, count++);
        if (color)
        {
            _pice[pointX, pointZ].ChangeColor();
        }
        return color;
    }
    IEnumerable<ReversiPice> CheckNeighborPice(int posX,int posZ)
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