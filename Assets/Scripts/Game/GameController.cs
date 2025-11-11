
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;  // Singleton
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    public GameObject player;
    public bool isGameActive = false;

    private int score = 0;

    void Awake()
    {
        // Assure qu'il n'y a qu'un seul GameController dans la scène
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        MidiReader.Instance.LoadMidi(GameData.midiFilePath.ToString());
        foreach (byte channel in MidiReader.Instance.GetChannels())
        {
            Debug.Log(channel); 
        }

        SpawnInitialEnemies();
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu"); // Retour menu
    }

    public void AddScore(int value)
    {
        score += value;
        UIHandler.instance.UpdateScore(score);
    }
    void SpawnInitialEnemies()
    {
        foreach (Transform spawn in enemySpawnPoints)
        {
            Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
        }
    }
}
