using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Interaction;
using MidiPlayerTK;

public class InvaderSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject invaderPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RythmSpawn());
        Debug.Log("K");
    }
    private IEnumerator RythmSpawn()
    {
        Note previousNote = null;
        Debug.Log(MidiReader.Instance.PercussionNotes);
        foreach (var note in MidiReader.Instance.PercussionNotes)
        {
            double waitTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time - previousNote?.Time ?? 0, MidiReader.Instance.TempoMap).TotalSeconds;
            Debug.Log(note.NoteNumber);
            yield return new WaitForSeconds((float)waitTime);
            StartCoroutine(spawnEnemy(invaderPrefab, note));
            previousNote = note;
            playSound(note);
        }
    }
    private IEnumerator spawnEnemy(GameObject enemy, Note note)
    {
        GameObject newEnnemy = Instantiate(enemy, new Vector3(note.NoteNumber / 8, 9, 0), Quaternion.identity);
        yield return null;
    }
    
    public void playSound(Note note)
    {
        MidiReader.Instance.PlayNote(note);
    }
}
