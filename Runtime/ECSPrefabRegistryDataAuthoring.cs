namespace KANIYONIKA.ECSPrefabHelper
{
    using UnityEngine;
    using Unity.Entities;

    public class ECSPrefabRegistryTagAuthoring : MonoBehaviour
    {
        public GameObject[] PrefabEntries;

        class Baker : Unity.Entities.Baker<ECSPrefabRegistryTagAuthoring>
        {
            public override void Bake(ECSPrefabRegistryTagAuthoring authoring)
            {
                var e = GetEntity(TransformUsageFlags.None);
                AddComponent(e, new ECSPrefabRegistryTag());
                AddComponent(e, new ECSPrefabRegistryData() { });

                var buffer = AddBuffer<ECSPrefabRegistryEntry>(e);

                for (int i = 0; i < authoring.PrefabEntries.Length; i++)
                {
                    var prefabEntity = GetEntity(authoring.PrefabEntries[i], TransformUsageFlags.Renderable | TransformUsageFlags.Dynamic);
                    buffer.Add(new ECSPrefabRegistryEntry
                    {
                        Prefab = prefabEntity
                    });
                }
            }
        }
    }
}
