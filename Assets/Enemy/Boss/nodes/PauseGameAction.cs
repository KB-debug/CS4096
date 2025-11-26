using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Pause Game", story: "Stopping Game", category: "Action", id: "37ec14ccd220eb029e6e8d2ed75cec7c")]
public partial class PauseGameAction : Action
{

    protected override Status OnStart()
    {

        GameManager.Instance.StartDelayedAction();
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

