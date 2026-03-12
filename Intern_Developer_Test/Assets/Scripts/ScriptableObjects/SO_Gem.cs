using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "GemData", menuName = "Scriptable Objects/Data/Gem")]
public class SO_Gem : ScriptableObject
{
    [Header("Sprite Renderer Attributes"), Space(5)]
    [Tooltip("Sprite to Render.")]
    public Sprite Sprite;
    [Tooltip("Sets the color for the sprite.")]
    public Color color;
    [Header("Audio Attributes"), Space(5)]
    [Tooltip("Audio to play when being pressed.")]
    public AudioClip OnPressedAudioClip;
    [Header("Animation Attributes"), Space(5)]
    [Tooltip("Animation to play when pressed.")]
    public Animation OnPressedAnimation;
    [Header("General Attributes"), Space(5)]
    [Tooltip("Score to generate when being ocmbined with 2 of the same gem")]
    public int Score;
}
