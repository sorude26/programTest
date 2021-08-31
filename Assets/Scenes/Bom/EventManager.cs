using System;
public class EventManager 
{
    public static event Action OnGameEnd;
    public static event Action OnGameClear;
    public static event Action OnRestart;
    public static event Action OnDataSet;
    public static void GameEnd()
    {
        OnGameEnd?.Invoke();
    }
    public static void GameClear()
    {
        OnGameClear?.Invoke();
    }
    public static void Restart()
    {
        OnRestart?.Invoke();
    }
    public static void DataSet()
    {
        OnDataSet?.Invoke();
    }
}
