using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskCategory("Tutorial")]
    public class CanSeeObject : Conditional
    {        

        // 모든 적끼리는 플레이어 감지 상태 공유
        public static bool isPlayerSpotted = false; 

        public SharedGameObject targetObject;
        public SharedFloat fieldOfViewAngle = 90;
        public SharedFloat viewDistance = 10;

        public override TaskStatus OnUpdate()
        {
            if (WithinSight(targetObject.Value, fieldOfViewAngle.Value, viewDistance.Value))
            {
                isPlayerSpotted = true;
                return TaskStatus.Success;
            }

            isPlayerSpotted = false;
            return TaskStatus.Failure;
        }

        private bool WithinSight(GameObject targetObject, float fieldOfViewAngle, float viewDistance)
        {
            if (targetObject == null) return false;

            Vector3 direction = targetObject.transform.position - transform.position;
            float distanceToTarget = direction.magnitude;
            float angleToTarget = Vector3.Angle(direction, transform.forward);

            if (distanceToTarget < viewDistance && angleToTarget < fieldOfViewAngle * 0.5f)
            {
                if (LineOfSight(targetObject))
                {
                    // 목표물(Player)이 감지
                    return true; 
                }
            }

            return false;
        }

        private bool LineOfSight(GameObject targetObject)
        {
            RaycastHit hit;
            LayerMask layerMask = LayerMask.GetMask("Player", "Obstacle");

            if (Physics.Raycast(transform.position, (targetObject.transform.position - transform.position).normalized, out hit, viewDistance.Value, layerMask))
            {
                return hit.transform == targetObject.transform;
            }
            return false;
        }
    }
}
