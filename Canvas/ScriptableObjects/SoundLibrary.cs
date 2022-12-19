using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "SCO/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    public SoundClip[] soundClips;

    private void OnValidate()
    {
        foreach (SoundClip soundClip in soundClips)
        {
            if (soundClip.hasPlayTimer && soundClip.playTimer == 0)
            {
                soundClip.playTimer = soundClip.audioClip.length;
            }
        }
    }
}

public enum SoundName
{
   AtaquePerro,
   LadridoPerro,
   GemidoPerro,
   MusicaFondo,
   Croar,
   Lenguetazo
}

[System.Serializable]
public class SoundClip
{
    public SoundName soundName;
    public AudioClip audioClip;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    public bool hasPlayTimer;
    public float playTimer;
}