using PrimeTween;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnvironmentController : MonoBehaviour
{
    public event Action onDayStarted;
    public event Action onNightStarted;

    public CurrentTime isDay = new CurrentTime(true);

    LightingManager lightingManager;
    TimeManager timeManager;
    VoidsManager voidsManager;

    [Header("Voids")]
    [SerializeField] private GameObject voidStonesBlack;
    [SerializeField] private GameObject voidStonesFire;

    [Header("Time")]
    [SerializeField] private float cycleDuration;
    [SerializeField] private float timeToChange;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Lighting Settings")]
    [SerializeField] private GameObject _light;
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private float dayIntensity;
    [SerializeField] private float nightIntensity;
    [SerializeField] private Color _globalDayLightColor;
    [SerializeField] private Color dayVoidsColor;
    [SerializeField] private Color _globalNightLightColor;
    [SerializeField] private Color nightVoidsColor;

    [Header("FallOff Settings")]
    [SerializeField] private float endFallOffValue;
    [SerializeField] private float fallOffDuration;

    private List<Light2D> lights = new List<Light2D>();
    private List<SpriteRenderer> voidsBlack = new List<SpriteRenderer>();
    private List<SpriteRenderer> voidsFire = new List<SpriteRenderer>();

    public void StartEnvironmentController()
    {
        GetEnvironment();
        TimeInit();
        ChangeEnvironment();
        StartCoroutine(timeManager.RunTimer());
    }

    private void OnEnable()
    {
        onDayStarted += ChangeCycle;
        onNightStarted += ChangeCycle;
    }

    private void OnDisable()
    {
        onDayStarted -= ChangeCycle;
        onNightStarted -= ChangeCycle;
    }

    private void ChangeCycle()
    {
        ChangeDay();
        ChangeEnvironment();
    }

    private void ChangeEnvironment()
    {
        Color voidsColor = isDay.Value ? dayVoidsColor : nightVoidsColor;
        Color globalColor = isDay.Value ? _globalDayLightColor : _globalNightLightColor;
        float intensity = isDay.Value ? dayIntensity : nightIntensity;

        ChangeLight(voidsColor, globalColor, intensity);

        ChangeVoids(voidsBlack, isDay.Value);   
        ChangeVoids(voidsFire, !isDay.Value);
    }

    private void ChangeDay()
    {
        isDay.Value = !isDay.Value;
    }

    private void ChangeLight(Color color, Color globalColor, float intensity)
    {
        foreach (var obj in lights)
        {
            lightingManager = new LightingManager(obj, color, intensity, timeToChange);
            ILighting light = isDay.Value ? lightingManager.GetDayLight() : lightingManager.GetNightLight();
            light.SetLighting();
        }
        Tween.Custom(_globalLight.color, globalColor, timeToChange, (Color c) => _globalLight.color = c);
    }

    private void ChangeVoids(IEnumerable<SpriteRenderer> voids, bool inverse)
    {
        foreach (var obj in voids)
        {
            voidsManager = new VoidsManager(obj, timeToChange, isDay.Value, inverse);
            voidsManager.SetVoids();
        }
    }

    private void TimeInit()
    {
        timeManager = new TimeManager(cycleDuration, timerText, isDay);

        timeManager.OnCycleEnded += CycleEnd;
    }

    private void CycleEnd()
    {
        if (isDay.Value)
        {
            onNightStarted?.Invoke();
        }
        else
        {
            onDayStarted?.Invoke();
        }

        StartCoroutine(timeManager.RunTimer());
    }

    private void GetEnvironment()
    {
        lights.AddRange(_light.GetComponentsInChildren<Light2D>());

        voidsBlack.AddRange(voidStonesBlack.GetComponentsInChildren<SpriteRenderer>());
        voidsFire.AddRange(voidStonesFire.GetComponentsInChildren<SpriteRenderer>());
    }
}
