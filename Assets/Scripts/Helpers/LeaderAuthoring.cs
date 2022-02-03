using Unity.Entities;
using UnityEngine;

namespace Helpers
{
    [AddComponentMenu("Custom Authoring/Leader Authoring")]
    public class LeaderAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public GameObject FollowerObject;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            FollowEntity followEntity = FollowerObject.GetComponent<FollowEntity>();
            if (followEntity == null)
            {
                followEntity = FollowerObject.AddComponent<FollowEntity>();
            }

            followEntity.Target = entity;
        }
    }
}