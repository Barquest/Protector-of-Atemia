using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform player; // Reference to the player's transform
        public Vector3 offset;   // Offset position of the camera relative to the player

        void Start()
        {
            // Calculate initial offset if not set
            if (offset == Vector3.zero)
            {
                offset = transform.position - player.position;
            }
        }

        void LateUpdate()
        {
            // Set the camera position to follow the player with the offset
            transform.position = player.position + offset;
        }
    }
}
