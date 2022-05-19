using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float minX, maxX;

    private Transform player;
    private Vector3 nextPosition;

    void Start() {
        player = GameObject.FindWithTag(Barrio.Enums.Tag.Player.ToString()).transform;
    }

    void LateUpdate() {
        if (!player) {
            return;
        }

        nextPosition = transform.position;
        nextPosition.x = player.position.x;

        if (nextPosition.x < minX) {
            nextPosition.x = minX;
        }

        if (nextPosition.x > maxX) {
            nextPosition.x = maxX;
        }

        transform.position = nextPosition;
    }
}
