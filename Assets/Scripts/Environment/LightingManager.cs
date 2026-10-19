using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightingManager
{
    private Light2D currentLight;

    private Color currentColor;

    private float currentIntensity;

    private float currentTimeToChange;

    public LightingManager(Light2D light, Color color, float intensity, float timeToChange)
    {
        currentLight = light;
        currentColor = color;
        currentIntensity = intensity;
        currentTimeToChange = timeToChange;
    }

    public ILighting GetDayLight()
    {
        return new DayLighting(currentLight, currentColor, currentIntensity, currentTimeToChange);
    }

    public ILighting GetNightLight()
    {
        return new NightLighting(currentLight, currentColor, currentIntensity, currentTimeToChange);
    }

}
