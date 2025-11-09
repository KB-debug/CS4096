using Unity.Behavior;
using UnityEngine;

public class GetRippleValue : MonoBehaviour
{

    public BehaviorGraphAgent bga;

    public Material mat;

    private BlackboardVariable<float> Density;
    private BlackboardVariable<float> Amplitude;
    private BlackboardVariable<float> Frequency;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bga.GetVariable("RippleDensity", out Density );
        bga.GetVariable("Amplitude", out Amplitude);
        bga.GetVariable("Frequency", out Frequency);

        float density = Density.Value;
        float amplitude = Amplitude.Value;  
        float frequency = Frequency.Value;

        mat.SetFloat("_Ripple_Density", density);
        mat.SetFloat("_Amplitude", amplitude);
        mat.SetFloat("_Frequency", frequency);

    }
}
