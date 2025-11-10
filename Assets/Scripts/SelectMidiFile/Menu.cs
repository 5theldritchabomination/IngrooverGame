using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class AddFiles : MonoBehaviour
{
public TMP_Dropdown dropdown;
    public Button playButton;
    public Button returnButton;
    public Button addFileButton;

    public string midiFolderPath = "Assets/MidiFiles"; // Dossier contenant les fichiers MIDI
    private static string midiFilePath;

    void Start()
    {
        RefreshDropdown();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        playButton.onClick.AddListener(PlayGame);
        returnButton.onClick.AddListener(ReturnGame);
        addFileButton.onClick.AddListener(LoadMIDI);
    }

    void RefreshDropdown()
    {
        dropdown.ClearOptions();

        if (!System.IO.Directory.Exists(midiFolderPath))
        {
            Debug.LogWarning("Dossier MIDI introuvable : " + midiFolderPath);
            return;
        }

        // Récupère tous les fichiers MIDI (.mid ou .midi)
        string[] midiFiles = System.IO.Directory.GetFiles(midiFolderPath, "*.mid").Concat(System.IO.Directory.GetFiles(midiFolderPath, "*.midi")).ToArray();

        var options = midiFiles.Select(file => Path.GetFileNameWithoutExtension(file).Substring(0, Mathf.Min(15, Path.GetFileNameWithoutExtension(file).Length))).ToList();

        dropdown.AddOptions(options);
    }

    void OnDropdownValueChanged(int index)
    {
        string[] files = System.IO.Directory.GetFiles(midiFolderPath, "*.mid")
            .Concat(System.IO.Directory.GetFiles(midiFolderPath, "*.midi"))
            .ToArray();

        if (index < files.Length)
        {
            GameData.midiFilePath = files[index];
            Debug.Log("Fichier MIDI sélectionné : " + midiFilePath);
        }
        var options = files.Select(file => Path.GetFileNameWithoutExtension(file).Substring(0, Mathf.Min(15, Path.GetFileNameWithoutExtension(file).Length))).ToList();

        EditButtonStart(options[index]);

    }

    void LoadMIDI()
    {
        // Déclarez midiFilePath avant de l'utiliser
        string midiFilePath = "";
        

#if UNITY_EDITOR
        // Utiliser Application.dataPath pour obtenir un chemin absolu vers le dossier "Assets/MidiFiles"
        string midiFolderPath = Path.Combine(Application.dataPath, "MidiFiles");
        
        // Ouvrir un panneau de sélection de fichier en utilisant le chemin absolu
        GameData.midiFilePath = UnityEditor.EditorUtility.OpenFilePanel("", midiFolderPath, "mid,midi");
#endif

        // Affichez le chemin du fichier sélectionné dans la console
        Debug.Log("Fichier MIDI sélectionné : " + midiFilePath);
        var options = midiFilePath.Split(new char []{'/'}).Last().Split(new char []{'.'}) ;
        EditButtonStart(options[0].Substring(0, Mathf.Min(15, Path.GetFileNameWithoutExtension(options[0]).Length)));
    }


    private void EditButtonStart(string midiFilePath)
    {
        string old = "Lancez le jeu avec : " ;
        GameObject.Find("GameStarter").GetComponentInChildren<Text>().text = old + midiFilePath;
    }
    private void ReturnGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}

