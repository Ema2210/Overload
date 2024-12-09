using UnityEngine;

public static class Pause
{
    private static bool isPaused = false;

    public static bool IsPaused
    {
        get { return isPaused; }
    }

    public static void SetPause(bool doYouWantItToBePaused)
    {
        isPaused = !doYouWantItToBePaused;
        TogglePause();
    }

    public static void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        SoundManager.Instance.ToggleMusicVolume(isPaused);

        if(isPaused)
        {
            Sound.Instance.MuffleSoundsForPause();
        }
        else
        {
            Sound.Instance.UnMuffleSoundsForPause();
        }
    }
}

