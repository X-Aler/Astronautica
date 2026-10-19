using System.Collections.Generic;
using UnityEngine;
public class LayersManager : MonoBehaviour 
{
    [SerializeField] private float setDistance;

    private Dictionary<SpriteRenderer, (string originalLayer, int originalOrder, string needLayer, int needOrder)> data = new();

    private List<SpriteRenderer> objectToSort = new List<SpriteRenderer>();

    private List<Transform> subjects = new List<Transform>();

    public void Awake()
    {
        LayerSettings[] settingsList = Object.FindObjectsByType<LayerSettings>(FindObjectsSortMode.None);
        foreach (var value in settingsList)
        {
            var render = value.GetComponent<SpriteRenderer>();

            var settings = render.GetComponent<LayerSettings>();

            string needLayer = settings.Layer;
            int needOrder = settings.Order;

            if (!data.ContainsKey(render))
            {
                data[render] = (render.sortingLayerName, render.sortingOrder, needLayer, needOrder);
            }

            if (!objectToSort.Contains(render))
            {
                objectToSort.Add(render);
            }
        }
    }
    public void RegisterSubject(Transform subject, PositionObserv position)
    {
        if (!subjects.Contains(subject))
        {
            subjects.Add(subject);
            position.onPositionChange += UpdateLayer;
        }
    }

    public void RegisterObject(SpriteRenderer render)
    {
        if (!objectToSort.Contains(render))
        {
            objectToSort.Add(render);
        }

        if (!data.ContainsKey(render))
        {
            var settings = render.GetComponent<LayerSettings>();

            string needLayer = settings.Layer;
            int needOrder = settings.Order;

            data[render] = (render.sortingLayerName, render.sortingOrder, needLayer, needOrder);
        }
    }

    private void UpdateLayer()
    {
        foreach (var render in objectToSort)
        {
            float closestDistance = float.MaxValue;
            Transform closestSubject = null;

            foreach (var subject in subjects)
            {
                if (subject == null) return;
                float distance = Vector2.Distance(subject.position, render.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSubject = subject;
                }
            }

            var Data = data[render];

            if (closestDistance <= setDistance)
            {
                float subjectY = closestSubject.GetComponent<Collider2D>().bounds.center.y;
                float renderY = render.GetComponent<Collider2D>().bounds.center.y;

                if (subjectY > renderY)
                {
                    render.sortingLayerName = Data.needLayer;
                    render.sortingOrder = Data.needOrder;
                }
                else
                {
                    render.sortingLayerName = Data.originalLayer;
                    render.sortingOrder = Data.originalOrder;
                }
            }
            else
            {
                render.sortingLayerName = Data.originalLayer;
                render.sortingOrder = Data.originalOrder;
            }
        }
    }
}
