using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Shoot", story: "[Agent] shoot at [Target]", category: "Action", id: "2878d61fa5487224a7845fd9ddff6c42")]
public partial class ShootAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> AgentShootPoint;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<GameObject> BulletPrefab;
    [SerializeReference] public BlackboardVariable<float> BulletSpeed;
    [SerializeReference] public BlackboardVariable<float> Damage;
    protected override Status OnStart()
    {
        GameObject shootPoint = AgentShootPoint.Value;
        GameObject bulletPrefab = BulletPrefab.Value;
        GameObject target = Target.Value;

        if (shootPoint == null || bulletPrefab == null)
        {
            Debug.Log("ShootAction: Missing ShootPoint or BulletPrefab.");
            return Status.Failure;
        }

        GameObject bullet = UnityEngine.Object.Instantiate(bulletPrefab,shootPoint.transform.position,shootPoint.transform.rotation);
        BulletLogicBoss logic = bullet.GetComponent<BulletLogicBoss>();
        logic.speed = BulletSpeed.Value;
        logic.damage = Damage.Value;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

