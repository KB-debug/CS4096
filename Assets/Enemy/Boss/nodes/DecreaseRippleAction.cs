using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Decrease Ripple", story: "Slowing down Ripples", category: "Action", id: "f1190882053cc845e511afe17354bf89")]
public partial class DecreaseRippleAction : Action
{

    [SerializeReference] public BlackboardVariable<float> Density;
    [SerializeReference] public BlackboardVariable<float> Amplitude;
    [SerializeReference] public BlackboardVariable<float> Frequancy;


    private float initialDensity;
    private float initialAmplitude;
    private float initialFrequency;
    protected override Status OnStart()
    {
        initialDensity = Density.Value;
        initialAmplitude = Amplitude.Value; 
        initialFrequency = Frequancy.Value;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        float totalTime = 2f;

        

        float rateDensity = initialDensity / totalTime;
        float rateAmplitude = initialAmplitude / totalTime;
        float rateFrequency = initialFrequency / totalTime;

        Density.Value = Mathf.Max(0, Density.Value - rateDensity * Time.deltaTime);
        Amplitude.Value = Mathf.Max(0, Amplitude.Value - rateAmplitude * Time.deltaTime);
        Frequancy.Value = Mathf.Max(0, Frequancy.Value - rateFrequency * Time.deltaTime);

        while (Density.Value > 0 || Amplitude.Value > 0 || Frequancy.Value > 0)
        {
            return Status.Running;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

