using System;
using UnityEngine;

public class PositionObserv : MonoBehaviour
{
    public event Action onPositionChange;

    private Vector3 lastSubjectPosition;

    private void Update()
    {
        if (lastSubjectPosition != transform.position)
        {
            lastSubjectPosition = transform.position;
            onPositionChange?.Invoke();
        }
    }
}
