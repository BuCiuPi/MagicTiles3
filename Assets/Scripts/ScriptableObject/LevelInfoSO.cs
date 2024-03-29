using UnityEngine;

[CreateAssetMenu(menuName = "MagicTiles3/LevelInfoSO")]
public class LevelInfoSO : ScriptableObject
{
    public float LevelNoteSpeed;
    public AudioClip LevelAudioClip;
    public float LevelAudioBPM;
    public int OffsetBeat;
    public float OffsetTime;

    public float NotePerBeat;
    public float MaxScore;

    [SerializeField] private float _badScore;
    [SerializeField] private float _goodScore;
    [SerializeField] private float _perfectScore;

    public float GetScoreByAccuracy(NoteAccuracy noteAccuracy)
    {
        switch (noteAccuracy)
        {
            case NoteAccuracy.Bad :
            return _badScore; 
            case NoteAccuracy.Good :
            return _goodScore; 
            case NoteAccuracy.Perfect :
            return _perfectScore; 
            default:
            return 0;
        }
    }

}