using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.UI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UI Update", story: "Starts and Runs UI for Boss", category: "Action", id: "1ab83f79e802fdd9cd57b94aed58da08")]
public partial class UiUpdateAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> HealthBar;
    [SerializeReference] public BlackboardVariable<float> Health;

    private Slider slider;

    protected override Status OnStart()
    {
        slider = HealthBar.Value.GetComponent<Slider>();
        slider.maxValue = Health.Value;
        HealthBar.Value.SetActive(true);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        
        slider.value = Health.Value;
        Debug.Log(slider.value);
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

