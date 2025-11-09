using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UI Update", story: "Starts and Runs UI for Boss", category: "Action", id: "1ab83f79e802fdd9cd57b94aed58da08")]
public partial class UiUpdateAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> HealthBar;
    protected override Status OnStart()
    {

        HealthBar.Value.SetActive(true);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

