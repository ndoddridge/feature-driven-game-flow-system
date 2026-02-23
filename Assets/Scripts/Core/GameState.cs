namespace GameFlow.Core
{
    [System.Flags]
    public enum GameState
    {
        None = 0,
        Busy = 1 << 0,
        FeatureLock = 1 << 1,
        Interrupted = 1 << 2
    }
}