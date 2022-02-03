using Unity.Physics;
using DataComponents;
using Unity.Entities;
using Unity.Collections;
using Unity.Physics.Systems;

namespace GameSystems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public class PickupSystem : SystemBase
    {
        private EndFixedStepSimulationEntityCommandBufferSystem _commandBuffer;
        private BuildPhysicsWorld _buildPhysicsWorld;
        private StepPhysicsWorld _stepPhysicsWorld;
        
        protected override void OnCreate()
        {
            _commandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        }

        protected override void OnUpdate()
        {
            Dependency = new TriggerJob
            {
                SpeedEntities = GetComponentDataFromEntity<SpeedData>(),
                EntitiesToDelete = GetComponentDataFromEntity<DeleteTag>(),
                CommandBuffer = _commandBuffer.CreateCommandBuffer()
            }.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, Dependency);
            
            _commandBuffer.AddJobHandleForProducer(Dependency);
        }
        
        public struct TriggerJob : ITriggerEventsJob
        {
            public ComponentDataFromEntity<SpeedData> SpeedEntities;
            [ReadOnly] public ComponentDataFromEntity<DeleteTag> EntitiesToDelete;
            public EntityCommandBuffer CommandBuffer;
            
            public void Execute(TriggerEvent triggerEvent)
            {
                TestEntityTrigger(triggerEvent.EntityA, triggerEvent.EntityB);
                TestEntityTrigger(triggerEvent.EntityB, triggerEvent.EntityA);
            }

            private void TestEntityTrigger(Entity a, Entity b)
            {
                if (SpeedEntities.HasComponent(a))
                {
                    if(EntitiesToDelete.HasComponent(b)) return;
                    
                    CommandBuffer.AddComponent<DeleteTag>(b);
                    CommandBuffer.RemoveComponent<PhysicsCollider>(b);
                }
            }
        }
    }
}