using UnityEngine;
using Unity.Physics;
using Unity.Entities;
using DataComponents;
using Unity.Mathematics;

namespace GameSystems
{
    public class MovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            float2 input = new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            Entities.ForEach((ref PhysicsVelocity physicsVelocity, in SpeedData speedData) =>
            {
                float2 newVelocity = physicsVelocity.Linear.xz;

                newVelocity = input * speedData.Value;
                physicsVelocity.Linear.xz = newVelocity;
            }).Run();
        }
    }
}