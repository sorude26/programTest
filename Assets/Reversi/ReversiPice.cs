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
        _pice.transform.rotation = Quaternion.Euler(180, 0, 0);
        PiceColor = PiceColor.Black;
    }
    public void ChangeColor()
    {
        if (_instanse.TurnColor == PiceColor.Black)
        {
            ChangeBlack();
        }
        else
        {
            ChangeWhite();
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
        _instanse.ChangeColorNeighorAround(_posX,_posZ);
        ChangeColor();
        _instanse.TouchPointSearch();
    }
}
