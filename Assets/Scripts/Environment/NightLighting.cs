using PrimeTween;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightLighting : ILighting
{
    private Light2D _light;

    private Color color;

    private float intensity;
    private float timeToChange;

    public NightLighting(Light2D _light, Color color, float intensity, float timeToChange)
    {
        this._light = _light;
        this.color = color;
        this.intensity = intensity;
        this.timeToChange = timeToChange;
    }

    public void SetLighting()
    {
        int layerID = SortingLayer.NameToID("Player");
        if (!_light.targetSortingLayers.Contains(layerID))
        {
            _light.targetSortingLayers = _light.targetSortingLayers
                                        .Concat(new int[] { layerID })
                                        .ToArray();
        }

        Tween.Custom(_light.color, color, timeToChange, (Color c) => _light.color = c);

        Tween.Custom(_light.intensity, intensity, timeToChange, (float i) => _light.intensity = i);
    }

}
