using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace Helpers
{
    public class FollowEntity : MonoBehaviour
    {
        public Entity Target;
        public float3 Offset;

        private EntityManager _entityManager;

        private void Awake()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        private void LateUpdate()
        {
            if (Target == null) return;

            Translation position = _entityManager.GetComponentData<Translation>(Target);
            Vector3 newPosition = position.Value + Offset;
            newPosition.y = 12;

            transform.position = newPosition;
        }
    }
}