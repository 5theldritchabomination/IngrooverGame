using System;
using System.Linq;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.MusicTheory;
using MidiNote = Melanchall.DryWetMidi.Interaction.Note;


public class MidiReader : MonoBehaviour
{
    public static MidiReader Instance;
    public string midiFilePath; // Chemin vers ton MIDI
    private MidiFile midiFile;

    public TempoMap TempoMap => midiFile.GetTempoMap();

    void Awake()
    {
        Instance = this;
        string path = GameData.midiFilePath;
        midiFile = MidiFile.Read(path);
        var notes = midiFile.GetNotes();
    }

    public void LoadMidi(string path)
    {
        midiFile = MidiFile.Read(path);
    }

    public IEnumerable<Melanchall.DryWetMidi.Interaction.Note> PercussionNotes =>
        midiFile?.GetNotes().Where(n => n.Channel == 0);
    
    public List<byte> GetChannels()
    {
        if (midiFile == null)
            return new List<byte>();

        // On parcourt toutes les notes et on prend leur channel
        var channels = midiFile.GetNotes()
                               .Select(n => (byte)n.Channel)
                               .Distinct()
                               .OrderBy(c => c)
                               .ToList();

        return channels;
    }
}
