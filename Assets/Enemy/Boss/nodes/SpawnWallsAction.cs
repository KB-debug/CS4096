using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Spawn Walls", story: "Spawn [Wall] to prevent Jump Attack Waves", category: "Action", id: "209b79a92dd84f118ada86df9db8ec95")]
public partial class SpawnWallsAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> WallPrefab;
    [SerializeReference] public BlackboardVariable<List<GameObject>> WallsPos;
    [SerializeReference] public BlackboardVariable<int> Phase;


    protected override Status OnStart()
    {

        GameObject wallPrefab = WallPrefab.Value;

        foreach (GameObject wallpos in WallsPos.Value)
        {

            GameObject Wall = UnityEngine.Object.Instantiate(wallPrefab, wallpos.transform.position,wallpos.transform.rotation);
            ExpandAndDestroy logic = Wall.GetComponent<ExpandAndDestroy>();
            logic.phase = Phase.Value;
        }
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

