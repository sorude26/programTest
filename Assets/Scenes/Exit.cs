using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] string _homeName = "TitleScene";
    public void OnClickExit()
    {
        SceneChangeControl.Instance.SceneChange(_homeName);
    }
}
