using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PiceColor
{
    None = -1,
    White = 0,
    Black = 1,
}
public class ReversiPice : MonoBehaviour
{
    [SerializeField] GameObject _pice;
    [SerializeField] GameObject _touchPanel;
    public PiceColor PiceColor { get; private set; } = PiceColor.None;
    public bool TouchMode { get; private set; } = false;
    int _posX = 0;
    int _posZ = 0;
    ReversiTest _instanse;
    float _movePos = 0;
    bool _startMove = false;
    bool _moveNow = default;
    [SerializeField] float _changeSpeed = 3f;
    public void SetReversiMaster(ReversiTest instanse,int posX,int posY)
    {
        _instanse = instanse;
        _posX = posX;
        _posZ = posY;
        _touchPanel.SetActive(false);
        TouchMode = false;
        _pice.SetActive(false);
        PiceColor = PiceColor.None;
    }
    public void ChangeWhite()
    {
        _pice.SetActive(true);
        _pice.transform.rotation = Quaternion.Euler(0, 0, 0);
        PiceColor = PiceColor.White;
    }
    public void ChangeBlack()
    {
        _pice.SetActive(true);
        _pice.transform.rotation = Quaternion.Euler(0, 0, 180);
        PiceColor = PiceColor.Black;
    }
    public void ChangeColor()
    {
        if (PiceColor == PiceColor.None)
        {
            if (_instanse.TurnColor == PiceColor.Black)
            {
                ChangeBlack();
            }
            else
            {
                ChangeWhite();
            }
            return;
        }
        if (_instanse.TurnColor == PiceColor.Black)
        {
            PiceColor = PiceColor.Black;
            _moveNow = true;
            _startMove = true;
        }
        else
        {
            PiceColor = PiceColor.White;
            _moveNow = true;
            _startMove = true;
        }
    }
    public void TouchOK()
    {
        TouchMode = true;
        _touchPanel.SetActive(true);
    }
    public void TouchNG()
    {
        TouchMode = false;
        _touchPanel.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (!TouchMode)
        {
            return;
        }
        TouchNG();
        _instanse.ChangeColorNeighorAround2(_posX,_posZ);
        ChangeColor();
        //_instanse.TouchPointSearch();
    }
    public void AITouch()
    {
        TouchMode = false;
        _instanse.ChangeColorNeighorAround2(_posX, _posZ);
        ChangeColor();
    }
    public string GetPos()
    {
        return _posX.ToString() + " " + _posZ.ToString();
    }
    private void Update()
    {
        if (PiceColor == PiceColor.None || !_moveNow)
        {
            return;
        }
        if (_startMove)
        {
            _movePos += _changeSpeed * Time.deltaTime;
            if (_movePos >= 1)
            {
                _movePos = 1;
                _startMove = false;
            }
            if (PiceColor == PiceColor.Black)
            {
                _pice.transform.localPosition = new Vector3(0, 0.08f + _movePos, 0);
                _pice.transform.rotation = Quaternion.Euler(0, 0, 90 * _movePos);
            }
            else
            {
                _pice.transform.localPosition = new Vector3(0, 0.08f + _movePos, 0);
                _pice.transform.rotation = Quaternion.Euler(0, 0, 180 - 90 * _movePos);
            }
        }
        else
        {
            _movePos -= _changeSpeed * Time.deltaTime;
            if (_movePos <= 0)
            {
                _movePos = 0;
                _moveNow = false;
            }
            if (PiceColor == PiceColor.Black)
            {
                _pice.transform.localPosition = new Vector3(0, 0.08f + _movePos, 0);
                _pice.transform.rotation = Quaternion.Euler(0, 0, 180 - 90 * _movePos);
            }
            else
            {
                _pice.transform.localPosition = new Vector3(0, 0.08f + _movePos, 0);
                _pice.transform.rotation = Quaternion.Euler(0, 0, 90 * _movePos);
            }
        }
    }
}
