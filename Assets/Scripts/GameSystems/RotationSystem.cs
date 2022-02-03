using DataComponents;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace GameSystems
{
    public class RotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref Rotation rotation, in RotationSpeedData rotationSpeedData) =>
            {
                rotation.Value = math.mul(rotation.Value,
                    quaternion.RotateX(math.radians(rotationSpeedData.Value * deltaTime)));
            }).Run();
        }
    }
}