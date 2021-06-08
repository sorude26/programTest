using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonState
{
    Point,
    Glider,
}
public class LifeControl : MonoBehaviour
{
    [SerializeField] int _sizeX = 10;
    [SerializeField] int _sizeY = 10;
    [SerializeField] CubeControl cubePlefab;
    CubeControl[,] _cubes;
    [SerializeField] float _updateTime = 1f;
    float _updateTimer;
    bool[,] _data;
    int[,] _life;
    [SerializeField] bool _randomMode;
    [SerializeField] string[] _debugCode;
    ButtonState _buttonState = ButtonState.Point;
    void Start()
    {
        _cubes = new CubeControl[_sizeX, _sizeY];
        _data = new bool[_sizeX, _sizeY];
        _life = new int[_sizeX, _sizeY];
        for (int i = 0; i < _sizeY; i++)
        {
            for (int a = 0; a < _sizeX; a++)
            {
                _cubes[a, i] = Instantiate(cubePlefab);
                _cubes[a, i].transform.position = new Vector3(a * 1.1f, i * 1.1f, 5);
                _cubes[a, i].transform.SetParent(transform);
                _cubes[a, i].SetPoint(a, i, this);
                if (_randomMode)
                {
                    int r = Random.Range(0, 3);
                    if (r == 0)
                    {
                        _data[a, i] = true;
                        _cubes[a, i].Dead();
                    }
                }
            }
        }
        if (!_randomMode)
        {
            for (int k = 0; k < _sizeY; k++)
            {
                for (int i = 0; i < _sizeX; i++)
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

        for (int i = 0; i < _sizeY; i++)
        {
            for (int a = 0; a < _sizeX; a++)
            {
                CheckAround(a, i);
            }
        }
        for (int k = 0; k < _sizeY; k++)
        {
            for (int j = 0; j < _sizeX; j++)
            {
                LifeCheck(j, k);
            }
        }
    }

    void CheckAround(int checkPointX, int checkPointY)
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
                if (_data[checkPointX - 1, _sizeY - 1])
                {
                    point++;
                }
            }
            if (checkPointY < _sizeY - 1)
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
            if (_data[_sizeX - 1, checkPointY])
            {
                point++;
            }
            if (checkPointY > 0)
            {
                if (_data[_sizeX - 1, checkPointY - 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[_sizeX - 1, _sizeY - 1])
                {
                    point++;
                }
            }
            if (checkPointY < _sizeY - 1)
            {
                if (_data[_sizeX - 1, checkPointY + 1])
                {
                    point++;
                }
            }
            else
            {
                if (_data[_sizeX - 1, 0])
                {
                    point++;
                }
            }
        }
        if (checkPointX < _sizeX - 1)
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
                if (_data[checkPointX + 1, _sizeY - 1])
                {
                    point++;
                }
            }
            if (checkPointY < _sizeY - 1)
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
                if (_data[0, _sizeY - 1])
                {
                    point++;
                }
            }
            if (checkPointY < _sizeY - 1)
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
            if (_data[checkPointX, _sizeY - 1])
            {
                point++;
            }
        }
        if (checkPointY < _sizeY - 1)
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
        _life[checkPointX, checkPointY] = point;
    }
    void LifeCheck(int checkPointX, int checkPointY)
    {
        if (_life[checkPointX, checkPointY] <= 1 || _life[checkPointX, checkPointY] >= 4)
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
    public void OnClickReset()
    {
        for (int y = 0; y < _sizeY; y++)
        {
            for (int x = 0; x < _sizeX; x++)
            {
                _data[x, y] = false;
                _cubes[x, y].Dead();
            }
        }
    }
    public void OnClickStateChange(int number)
    {
        switch (number)
        {
            case 0:
                _buttonState = ButtonState.Point;
                break;
            case 1:
                _buttonState = ButtonState.Glider;
                break;
            default:
                break;
        }
    }
    public void PointAction(int x, int y)
    {
        switch (_buttonState)
        {
            case ButtonState.Point:
                if (_data[x, y])
                {
                    PointDead(x, y);
                }
                else
                {
                    PointRevival(x, y);
                }
                break;
            case ButtonState.Glider:
                PointGlider(x, y);
                break;
            default:
                break;
        }
    }
    public void PointRevival(int x, int y)
    {
        _data[x, y] = true;
        _cubes[x, y].Life();
    }
    public void PointDead(int x, int y)
    {
        _data[x, y] = false;
        _cubes[x, y].Dead();
    }
    public void PointGlider(int x, int y)
    {
        int top = y + 1;
        int bottom = y - 1;
        int left = x - 1;
        int right = x + 1;
        if (top < _sizeY)
        {
            if (left >= 0)
            {
                _cubes[left, top].Life();
                _data[left, top] = true;
            }
            _cubes[x, top].Life();
            _data[x, top] = true;
            if (right < _sizeX)
            {
                _cubes[right, top].Life();
                _data[right, top] = true;
            }
        }
        if (left >= 0)
        {
            _cubes[left, y].Life();
            _data[left, y] = true;
        }
        _cubes[x, y].Dead();
        _data[x, y] = false;
        if (right < _sizeX)
        {
            _cubes[right, y].Dead();
            _data[right, y] = false;
        }
        if (bottom >= 0)
        {
            if (left > 0)
            {
                _cubes[left, bottom].Dead();
                _data[left, bottom] = false;
            }
            _cubes[x, bottom].Life();
            _data[x, bottom] = true;
            if (right < _sizeX)
            {
                _cubes[right, bottom].Dead();
                _data[right, bottom] = false;
            }
        }
    }
}
