using UnityEngine;


[ CreateAssetMenu(fileName = "GameplayAction", menuName = "Arcade2D/GameplayAction")]
public class GameplayAction : ScriptableObject
{
    public AnimatorOverrideController animatorOverrideController;
    public AnimationClip Clip;

    public int Damage;
    public float Speed;
    public float Y_Kb;
    public float X_Kb;

}