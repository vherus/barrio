using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Scriptable Objects/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public float movementForce { get; private set; } = 5f;

    [field: SerializeField]
    public float jumpForce { get; private set; } = 5f;

    [field: Range(min: 0, max: 5), SerializeField]
    public int maxHealth { get; private set; } = 5;
}
