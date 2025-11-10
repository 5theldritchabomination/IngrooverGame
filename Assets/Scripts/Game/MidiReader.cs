using System;
using System.Linq;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class MidiReader : MonoBehaviour
{
    public string midiFilePath; // Chemin absolu ou relatif à ton dossier Assets

    void Start()
    {
        string path = GameData.midiFilePath;

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("❌ Aucun fichier MIDI défini !");
            return;
        }

        Debug.Log("🎵 Lecture du fichier MIDI : " + path);

        try
        {
            var midiFile = MidiFile.Read(path);
            var notes = midiFile.GetNotes();
            Debug.Log($"✅ {notes.Count} notes chargées depuis {path}");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erreur de lecture MIDI : " + e.Message);
        }
    }

    public void LoadMidi(string path)
    {
        try
        {
            Debug.Log("Chargement du fichier MIDI : " + path);

            var midiFile = MidiFile.Read(path);

            // Récupérer les notes
            var notes = midiFile.GetNotes();
            Debug.Log($"🎵 {notes.Count()} notes trouvées dans le fichier.");

            // Récupérer le tempo
            var tempoMap = midiFile.GetTempoMap();
            var tempos = midiFile.GetTempoMap().GetTempoChanges();

            foreach (var tempo in tempos)
            {
                Debug.Log($"Tempo : {tempo.Value.BeatsPerMinute} BPM à {tempo.Time}");
            }

            // Exemple : afficher les premières notes
            foreach (var note in notes.Take(10))
            {
                var time = note.TimeAs<MetricTimeSpan>(tempoMap);
                Debug.Log($"Note {note.NoteName} à {time.Minutes:D2}:{time.Seconds:D2}:{time.Milliseconds:D3}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Erreur de lecture MIDI : " + e.Message);
        }
    }
}
