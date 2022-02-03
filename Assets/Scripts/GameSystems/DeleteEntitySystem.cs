using DataComponents;
using Unity.Entities;

namespace GameSystems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PickupSystem))]
    public class DeleteEntitySystem : SystemBase
    {
        private EndFixedStepSimulationEntityCommandBufferSystem _commandBuffer;

        protected override void OnStartRunning()
        {
            _commandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _commandBuffer.CreateCommandBuffer();

            Entities
                .WithAll<DeleteTag>()
                .WithoutBurst()
                .ForEach((Entity e) =>
                {
                    commandBuffer.DestroyEntity(e);
                }).Run();
            
            _commandBuffer.AddJobHandleForProducer(Dependency);
        }
    }
}