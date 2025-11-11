using System;
using System.Linq;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.MusicTheory;
using MidiNote = Melanchall.DryWetMidi.Interaction.Note;
using MidiPlayerTK;



public class MidiReader : MonoBehaviour
{
    private MidiStreamPlayer midiStreamPlayer;

    public static MidiReader Instance;
    public string midiFilePath; 
    private MidiFile midiFile;

    public TempoMap TempoMap => midiFile.GetTempoMap();

    void Awake()
    {
        midiStreamPlayer = FindFirstObjectByType<MidiStreamPlayer>();

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

        var channels = midiFile.GetNotes()
                               .Select(n => (byte)n.Channel)
                               .Distinct()
                               .OrderBy(c => c)
                               .ToList();

        return channels;
    }
    

    public void PlayNote(Melanchall.DryWetMidi.Interaction.Note note)
    {
        if (midiStreamPlayer == null) return;

        midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent()
        {
            Command = MPTKCommand.NoteOn,
            Value = note.NoteNumber,
            Velocity = note.Velocity,
            Channel = 0
        });

        StartCoroutine(StopNoteAfterDelay(note.NoteNumber, note.Time));
    }

    private System.Collections.IEnumerator StopNoteAfterDelay(int note, float delay)
    {
        yield return new WaitForSeconds(delay);

        midiStreamPlayer.MPTK_PlayEvent(new MPTKEvent()
        {
            Command = MPTKCommand.NoteOff,
            Value = 0,
            Channel = 0
        });
    }
}
