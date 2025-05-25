namespace KANIYONIKA.ECSPrefabHelper
{
    using UnityEngine;
    using Unity.Entities;

    public class PrefabTagAuthoring : MonoBehaviour
    {
        public GameObject[] PrefabEntries;

        class Baker : Unity.Entities.Baker<PrefabTagAuthoring>
        {
            public override void Bake(PrefabTagAuthoring authoring)
            {
                var e = GetEntity(TransformUsageFlags.None);
                AddComponent(e, new PrefabTag());
                AddComponent(e, new PrefabData() { });

                var buffer = AddBuffer<PrefabBuffer>(e);

                for (int i = 0; i < authoring.PrefabEntries.Length; i++)
                {
                    var prefabEntity = GetEntity(authoring.PrefabEntries[i], TransformUsageFlags.Renderable | TransformUsageFlags.Dynamic);
                    buffer.Add(new PrefabBuffer
                    {
                        Prefab = prefabEntity
                    });
                }
            }
        }
    }
}