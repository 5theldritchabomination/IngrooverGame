using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class AddFiles : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Button playButton;
    public Button returnButton;
    public Button addFileButton;

    public string midiFolderPath = "MidiFiles";
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

        if (!Directory.Exists(midiFolderPath)) Directory.CreateDirectory(midiFolderPath);

        string[] midiFiles = Directory.GetFiles(midiFolderPath, "*.mid")
            .Concat(Directory.GetFiles(midiFolderPath, "*.midi")).ToArray();

        var options = midiFiles.Select(file => Path.GetFileNameWithoutExtension(file)
            .Substring(0, Mathf.Min(15, Path.GetFileNameWithoutExtension(file).Length))).ToList();

        dropdown.AddOptions(options);
    }

    void OnDropdownValueChanged(int index)
    {
        string[] files = Directory.GetFiles(midiFolderPath, "*.mid")
            .Concat(Directory.GetFiles(midiFolderPath, "*.midi"))
            .ToArray();

        if (index < files.Length)
        {
            GameData.midiFilePath = files[index];
            Debug.Log("Fichier MIDI sélectionné : " + GameData.midiFilePath);
        }

        var options = files.Select(file => Path.GetFileNameWithoutExtension(file)
            .Substring(0, Mathf.Min(15, Path.GetFileNameWithoutExtension(file).Length))).ToList();

        EditButtonStart(options[index]);
    }

    void LoadMIDI()
    {
        string midiFilePath = "";
        midiFilePath = OpenFileDialog("Choisir un fichier MIDI", "Fichiers MIDI\0*.mid;*.midi\0Tous les fichiers\0*.*\0");
        if (string.IsNullOrEmpty(midiFilePath))
        {
            Debug.LogWarning("Aucun fichier MIDI sélectionné.");
            return;
        }

        GameData.midiFilePath = midiFilePath;
        Debug.Log("Fichier MIDI sélectionné : " + midiFilePath);

        var options = Path.GetFileNameWithoutExtension(midiFilePath);
        EditButtonStart(options.Substring(0, Mathf.Min(15, options.Length)));
    }

    private void EditButtonStart(string midiFilePath)
    {
        string old = "Lancez le jeu avec : ";
        GameObject.Find("GameStarter").GetComponentInChildren<Text>().text = old + midiFilePath;
    }

    private void ReturnGame() => SceneManager.LoadScene("MainMenu");
    private void PlayGame() => SceneManager.LoadScene("Game");

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OpenFileName
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public string filter = null;
        public string customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public string file = null;
        public int maxFile = 0;
        public string fileTitle = null;
        public int maxFileTitle = 0;
        public string initialDir = null;
        public string title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public string defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public string templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }

    [DllImport("comdlg32.dll", CharSet = CharSet.Auto)]
    private static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

    private string OpenFileDialog(string title, string filter)
    {
        OpenFileName ofn = new OpenFileName();
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = filter;
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new string(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = Path.Combine(Application.dataPath, "MidiFiles");
        ofn.title = title;
        ofn.defExt = "mid";

        if (GetOpenFileName(ofn))
        {
            return ofn.file;
        }
        return null;
    }
}
