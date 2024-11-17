using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // Публичные переменные
    public float speed = 5f;

    // Приватные переменные
    private List<Vector3> positions;
    private List<Quaternion> rotations;
    private List<float> timestamps;
    private int currentIndex = 0;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        GhostControl();
    }

    private void GhostControl()
    {
        if (currentIndex < positions.Count - 1)
        {
            float elapsedTime = Time.time - startTime;
            while (currentIndex < positions.Count - 1 && elapsedTime >= timestamps[currentIndex + 1])
            {
                currentIndex++;
            }

            if (currentIndex < positions.Count - 1)
            {
                float t = (elapsedTime - timestamps[currentIndex]) / (timestamps[currentIndex + 1] - timestamps[currentIndex]);
                transform.position = Vector3.Lerp(positions[currentIndex], positions[currentIndex + 1], t);
                transform.rotation = Quaternion.Lerp(rotations[currentIndex], rotations[currentIndex + 1], t);
            }
        }
    }

    // Установка данных
    public void SetData(List<Vector3> newPositions, List<Quaternion> newRotations, List<float> newTimestamps)
    {
        positions = new List<Vector3>(newPositions);
        rotations = new List<Quaternion>(newRotations);
        timestamps = new List<float>(newTimestamps);
        currentIndex = 0;
        startTime = Time.time;
    }
}
