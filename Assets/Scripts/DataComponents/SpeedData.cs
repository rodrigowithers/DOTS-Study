using Unity.Entities;

namespace DataComponents
{
    [GenerateAuthoringComponent]
    public struct SpeedData : IComponentData
    {
        public float Value;
    }
}