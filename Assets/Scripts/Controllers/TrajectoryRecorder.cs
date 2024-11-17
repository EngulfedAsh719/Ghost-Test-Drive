using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRecorder : MonoBehaviour
{
    public float recordInterval = 0.1f;

    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    private List<float> timestamps = new List<float>();
    private float timer = 0f;
    private float startTime;
    private Vector3 lastPosition;

    private void Start()
    {
        startTime = Time.time;
        lastPosition = transform.position;
    }

    private void Update()
    {
        Record();
    }

    private void Record()
    {
        timer += Time.deltaTime;
        if (timer >= recordInterval)
        {
            positions.Add(transform.position);
            rotations.Add(transform.rotation);
            timestamps.Add(Time.time - startTime);
            timer = 0f;
        }
    }

    // Проверка: движется ли игрок вперед
    public bool IsMovingForward()
    {
        Vector3 currentPosition = transform.position;
        bool isForward = currentPosition.z > lastPosition.z;
        lastPosition = currentPosition;
        return isForward;
    }

    // Сброс данных
    public void ResetData()
    {
        positions.Clear();
        rotations.Clear();
        timestamps.Clear();
        startTime = Time.time;
    }

    // Получение данных
    public List<Vector3> GetPositions() => positions;
    public List<Quaternion> GetRotations() => rotations;
    public List<float> GetTimestamps() => timestamps;
}
