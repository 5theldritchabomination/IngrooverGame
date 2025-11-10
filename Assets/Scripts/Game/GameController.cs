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

    // -------------------
    // 🔹 GESTION DU JEU
    // -------------------

    public void StartGame()
    {
        isGameActive = true;
        score = 0;

        // Tu pourras ici lancer le MIDI plus tard :
        // MidiReader.Instance.LoadMidi(GameData.midiFilePath);

        SpawnInitialEnemies();
        Debug.Log("🚀 Jeu lancé !");
    }

    public void EndGame()
    {
        isGameActive = false;
        Debug.Log("💀 Game Over !");
        SceneManager.LoadScene("MainMenu"); // Retour menu
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log($"Score: {score}");
        UIHandler.instance.UpdateScore(score);
    }

    // -------------------
    // 🔹 ENNEMIS
    // -------------------
    void SpawnInitialEnemies()
    {
        foreach (Transform spawn in enemySpawnPoints)
        {
            Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
        }
    }
}
