using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TrajectoryRecorder recorder;
    public GameObject ghostPrefab;
    public Transform startPoint;
    public Text lapText;
    public int totalLaps = 3;

    private int currentLap = 0;
    private bool isLapValid = false;
    private List<GameObject> ghosts = new List<GameObject>();

    private void Start()
    {
        UpdateLapText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 relativePosition = transform.InverseTransformPoint(other.transform.position);
            if (relativePosition.z < 0)
            {
                isLapValid = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isLapValid)
        {
            Vector3 relativePosition = transform.InverseTransformPoint(other.transform.position);
            if (relativePosition.z > 0)
            {
                currentLap++;
                isLapValid = false;
                if (currentLap <= totalLaps)
                {
                    UpdateLapText();
                }
                if (currentLap > 1)
                {
                    RemoveOldGhosts();
                    SpawnGhost();
                }
                if (currentLap == totalLaps + 1)
                {
                    lapText.text = "Все круги пройдены, возврат в меню через 3 секунды";
                    StartCoroutine(CountdownToMenu());
                }
                recorder.ResetData();
            }
        }
    }

    // Спавн призрака
    private void SpawnGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab, startPoint.position, startPoint.rotation);
        GhostController ghostController = ghost.GetComponent<GhostController>();
        ghostController.SetData(recorder.GetPositions(), recorder.GetRotations(), recorder.GetTimestamps());
        ghosts.Add(ghost);
    }

    // Удаление старых призраков
    private void RemoveOldGhosts()
    {
        foreach (GameObject ghost in ghosts)
        {
            Destroy(ghost);
        }
        ghosts.Clear();
    }

    // Обновление текста кругов
    private void UpdateLapText()
    {
        lapText.text = $"{currentLap}/{totalLaps}";
    }

    // Отсчет времени до выхода в сцену меню
    private IEnumerator CountdownToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MenuScene");
    }
}
