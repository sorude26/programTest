using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControl : MonoBehaviour
{
    [SerializeField] int _size = 10;
    [SerializeField] CubeControl cubePlefab;
    CubeControl[,] _cubes;
    [SerializeField] float _updateTime = 1f;
    float _updateTimer;
    bool[,] _data;
    int[,] _life;
    [SerializeField] bool _randomMode;
    [SerializeField] string[] _debugCode;
    void Start()
    {
        _cubes = new CubeControl[_size , _size];
        _data = new bool[_size , _size];
        _life = new int[_size , _size];
        for (int i = 0; i < _size; i++)
        {
            for (int a = 0; a < _size; a++)
            {
                _cubes[i , a] = Instantiate(cubePlefab);
                _cubes[i , a].transform.position = new Vector3(a * 1.1f, i * 1.1f, 5);
                _cubes[i , a].transform.SetParent(transform);
                _cubes[i , a].SetPoint(i, a, this);
                if (_randomMode)
                {
                    int r = Random.Range(0, 3);
                    if (r == 0)
                    {
                        _data[i, a] = true;
                        _cubes[i, a].Dead();
                    }
                }
            }
        }
        if (!_randomMode)
        {
            for (int i = 0; i < _size; i++)
            {
                for (int k = 0; k < _size; k++)
                {
                    _cubes[i, k].Dead();
                    if (i < _debugCode.Length)
                    {                        
                        if (k < _debugCode[i].Length)
                        {
                            if (_debugCode[i][k] != '0')
                            {
                                _data[i, k] = true;
                                _cubes[i, k].Life();
                            }
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        if (_randomMode)
        {
            _updateTimer += Time.deltaTime;
            if (_updateTimer >= _updateTime)
            {
                _updateTimer = 0;
                NextStep();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextStep();
            }
        }
    }
    void NextStep()
    {

        for (int i = 0; i < _size; i++)
        {
            for (int a = 0; a < _size; a++)
            {
                CheckAround(a, i);
            }
        }
        for (int k = 0; k < _size; k++)
        {
            for (int j = 0; j < _size; j++)
            {
                LifeCheck(j, k);
            }
        }
    }

    void CheckAround(int checkPointX , int checkPointY)
    {
        int point = 0;
        if (checkPointX > 0)
        {
            if (_data[checkPointX - 1, checkPointY])
            {
                point++;
            }
            if (checkPointY > 0)
            {
                if (_data[checkPointX - 1, checkPointY - 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[checkPointX - 1, _size - 1])
                {
                    point++;
                }
            }
            if (checkPointY < _size - 1)
            {
                if (_data[checkPointX - 1, checkPointY + 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[checkPointX - 1, 0])
                {
                    point++;
                }
            }
        }
        else
        {
            if (_data[_size - 1, checkPointY])
            {
                point++;
            }
            if (checkPointY > 0)
            {
                if (_data[_size - 1, checkPointY - 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[_size - 1, _size - 1])
                {
                    point++;
                }
            }
            if (checkPointY < _size - 1)
            {
                if (_data[_size - 1, checkPointY + 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[_size - 1, 0])
                {
                    point++;
                }
            }
        }
        if (checkPointX < _size - 1)
        {
            if (_data[checkPointX + 1, checkPointY])
            {
                point++;
            }
            if (checkPointY > 0)
            {
                if (_data[checkPointX + 1, checkPointY - 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[checkPointX + 1, _size - 1])
                {
                    point++;
                }
            }
            if (checkPointY < _size - 1)
            {
                if (_data[checkPointX + 1, checkPointY + 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[checkPointX + 1, 0])
                {
                    point++;
                }
            }
        }
        else
        {
            if (_data[0, checkPointY])
            {
                point++;
            }
            if (checkPointY > 0)
            {
                if (_data[0, checkPointY - 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[0, _size - 1])
                {
                    point++;
                }
            }
            if (checkPointY < _size - 1)
            {
                if (_data[0, checkPointY + 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[0, 0])
                {
                    point++;
                }
            }
        }

        if (checkPointY > 0)
        {
            if (_data[checkPointX, checkPointY - 1])
            {
                point++;
            }
        }
        else
        {
            if (_data[checkPointX, _size - 1])
            {
                point++;
            }
        }
        if (checkPointY < _size - 1)
        {
            if (_data[checkPointX, checkPointY + 1])
            {
                point++;
            }
        }
        else
        {
            if (_data[checkPointX, 0])
            {
                point++;
            }
        }
        _life[checkPointX,checkPointY] = point;
    }
    void LifeCheck(int checkPointX,int checkPointY) 
    {
        if (_life[checkPointX,checkPointY] <= 1 || _life[checkPointX, checkPointY] >= 4)
        {
            _data[checkPointX, checkPointY] = false;
            _cubes[checkPointX, checkPointY].Dead();
        }
        else
        {
            if (!_data[checkPointX, checkPointY] && _life[checkPointX, checkPointY] == 3)
            {
                _data[checkPointX, checkPointY] = true;
                _cubes[checkPointX, checkPointY].Life();
            }
        }
        _life[checkPointX, checkPointY] = 0;
    }

    public void PointRevival(int x, int y)
    {
        _data[x, y] = true;
    }
}
