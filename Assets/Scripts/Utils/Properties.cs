using UnityEngine;

namespace Utils
{
    public class Properties : MonoBehaviour
    {
        public int initialDropSpawnCount = 100;
        public float maxDistanceToSelectDrop = 0.6f;
        public float massForEachDrop = 0.1f;
        public float frictionValue = 0.25f;
        public float bouncinessValue = 0.1f;
        public float minVelocityInY = 5.0f;
        public float maxVelocityInY = 5.0f;
        public void ResetValues()
        {
            initialDropSpawnCount = 100;
            maxDistanceToSelectDrop = 0.6f;
            massForEachDrop = 0.1f;
            frictionValue = 0.25f;
            bouncinessValue = 0.1f;
            minVelocityInY = 5.0f;
            maxVelocityInY = 0.7f;
        }
    }
}
