using UnityEngine;

/// <summary>
/// Менеджер глобальных настроек (фреймрейт, физика, ориентация экрана и т.п.).
/// </summary>
[DisallowMultipleComponent]
public class SettingsManager : MonoBehaviour
{
    [Header("Frame rate & VSync")]
    [Tooltip("0 = no limit, otherwise target fps")]
    [SerializeField] private int targetFrameRate = 60;
    [Tooltip("VSync count (0 = off, 1 = sync every frame, 2 = every second frame)")]
    [SerializeField] private int vSyncCount = 0;

    [Header("Physics / Time")]
    [Tooltip("Fixed timestep for physics (seconds). Typical: 0.02 (50Hz) или 0.0166667 (60Hz)")]
    [SerializeField] private float fixedTimestep = 0.0166667f;
    [Tooltip("Maximum allowed delta time (seconds) - prevents huge physics steps")]
    [SerializeField] private float maximumDeltaTime = 0.1f;

    [Header("Screen orientation")]
    [Tooltip("Which orientation to force on startup")]
    [SerializeField] private ScreenOrientation forceOrientation = ScreenOrientation.LandscapeLeft;
    [SerializeField] private bool autorotateToPortrait = false;
    [SerializeField] private bool autorotateToPortraitUpsideDown = false;
    [SerializeField] private bool autorotateToLandscapeLeft = true;
    [SerializeField] private bool autorotateToLandscapeRight = true;

    [Header("Behavior")]
    [Tooltip("Apply settings automatically in Awake()")]
    [SerializeField] private bool applyOnAwake = true;

    public static SettingsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) { Destroy(gameObject); return; }

        if (applyOnAwake)
            Apply();
    }

    /// <summary>
    /// Применить настройки (можно вызывать вручную).
    /// </summary>
    public void Apply()
    {
        ApplyFrameRate();
        ApplyTimeSettings();
        ApplyScreenOrientation();
    }

    private void ApplyFrameRate()
    {
        // VSync: UnityEngine.QualitySettings.vSyncCount
        QualitySettings.vSyncCount = Mathf.Clamp(vSyncCount, 0, 2);

        // Если VSync выключен (0), устанавливаем targetFrameRate (если > 0)
        if (QualitySettings.vSyncCount == 0)
        {
            if (targetFrameRate <= 0)
                Application.targetFrameRate = -1; // no limit
            else
                Application.targetFrameRate = targetFrameRate;
        }
        else
        {
            Application.targetFrameRate = -1;
        }
    }

    private void ApplyTimeSettings()
    {
        if (fixedTimestep > 0f)
            Time.fixedDeltaTime = fixedTimestep;

        if (maximumDeltaTime > 0f)
            Time.maximumDeltaTime = maximumDeltaTime;
    }

    private void ApplyScreenOrientation()
    {
        Screen.orientation = forceOrientation;

        Screen.autorotateToPortrait = autorotateToPortrait;
        Screen.autorotateToPortraitUpsideDown = autorotateToPortraitUpsideDown;
        Screen.autorotateToLandscapeLeft = autorotateToLandscapeLeft;
        Screen.autorotateToLandscapeRight = autorotateToLandscapeRight;
    }

    // Публичные сеттеры для изменения настроек во время выполнения 
    public void SetTargetFrameRate(int fps)
    {
        targetFrameRate = fps;
        ApplyFrameRate();
    }

    public void SetVSyncCount(int count)
    {
        vSyncCount = Mathf.Clamp(count, 0, 2);
        ApplyFrameRate();
    }

    public void SetFixedTimestep(float dt)
    {
        fixedTimestep = dt;
        ApplyTimeSettings();
    }

    public void SetForceOrientation(ScreenOrientation orientation)
    {
        forceOrientation = orientation;
        ApplyScreenOrientation();
    }
}

