public static class Settings
{
    private static float _volumeMaster = 0f;
    private static float _volumeMusic = 0f;
    private static float _volumeMenu = 0f;
    private static float _volumeFX = 0f;
    private static bool _volumeMute = false;

    public static float VolumeMaster { get { return _volumeMaster; } set { _volumeMaster = value; } }
    public static float VolumeMusic { get { return _volumeMusic; } set { _volumeMusic = value; } }
    public static float VolumeMenu { get { return _volumeMenu; } set { _volumeMenu = value; } }
    public static float VolumeFX { get { return _volumeFX; } set { _volumeFX = value; } }
    public static bool VolumeMute { get { return _volumeMute; } set { _volumeMute = value; } }
}
