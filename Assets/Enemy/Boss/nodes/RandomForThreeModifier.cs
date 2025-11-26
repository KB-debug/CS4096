using System;
using Unity.Properties;


namespace Unity.Behavior
{
    /// <summary>
    /// Executes a random branch.
    /// </summary>
    [Serializable, GeneratePropertyBag]
    [NodeDescription(
        name: "Random",
        description: "Executes a random branch for 3 children with weights of 0.1,0.3,0.6",
        icon: "Icons/random",
        category: "Flow",
        id: "3ec329cc9c414fd88aa9113e7c422f1a")]
    internal partial class RandomProbModifier : Composite
    {
        int m_RandomIndex = 0;

        /// <inheritdoc cref="OnStart" />
        protected override Status OnStart()
        {

            int ranNum = UnityEngine.Random.Range(0, 10);

            if (ranNum <= 0)
            {
                m_RandomIndex = 0;
            }
            else if (ranNum <= 2)
            {
                m_RandomIndex = 1;
            }
            else
            {
                m_RandomIndex = 2;
            }

            if (m_RandomIndex < Children.Count)
            {
                var status = StartNode(Children[m_RandomIndex]);
                if (status == Status.Success || status == Status.Failure)
                    return status;

                return Status.Waiting;
            }

            return Status.Success;
        }

        /// <inheritdoc cref="OnUpdate" />
        protected override Status OnUpdate()
        {
            var status = Children[m_RandomIndex].CurrentStatus;
            if (status == Status.Success || status == Status.Failure)
                return status;

            return Status.Waiting;
        }
    }
}