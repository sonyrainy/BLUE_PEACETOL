using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsInZone : Conditional
{
    public SharedGameObject player;
    public Collider zoneCollider;

    public override TaskStatus OnUpdate()
    {
        if (player.Value == null || zoneCollider == null)
        {
            return TaskStatus.Failure;
        }

        if (zoneCollider.bounds.Contains(player.Value.transform.position))
        {
            return TaskStatus.Success; 
        }
        
        return TaskStatus.Failure; 
    }

    public override void OnReset()
    {
        player = null;
        zoneCollider = null;
    }
}
