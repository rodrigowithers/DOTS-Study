using Unity.Entities;

namespace DataComponents
{
    [GenerateAuthoringComponent]
    public struct RotationSpeedData : IComponentData
    {
        public float Value;
    }
}
