namespace KANIYONIKA.ECSPrefabHelper
{
    using Unity.Entities;
    using Unity.Burst;


    [BurstCompile]
    public readonly partial struct PrefabHelper
    {
        private readonly DynamicBuffer<PrefabBuffer> _buffer;

        public PrefabHelper(DynamicBuffer<PrefabBuffer> buffer)
        {
            _buffer = buffer;
        }

        public bool TryGetByITag<T>(ComponentLookup<T> tagLookup, out Entity prefab) where T : unmanaged, IComponentData, ITagComponentData
        {
            for (int i = 0; i < _buffer.Length; i++)
            {
                Entity prefabEntity = _buffer[i].Prefab;
                if (tagLookup.HasComponent(prefabEntity))
                {
                    prefab = prefabEntity;
                    return true;
                }
            }

            prefab = Entity.Null;
            return false;
        }

        public Entity GetByITag<T>(ComponentLookup<T> tagLookup) where T : unmanaged, IComponentData, ITagComponentData
        {
            for (int i = 0; i < _buffer.Length; i++)
            {
                Entity prefabEntity = _buffer[i].Prefab;
                if (tagLookup.HasComponent(prefabEntity))
                {
                    return prefabEntity;
                }
            }

            return Entity.Null;
        }

        public Entity GetByTagType<T>(ref EntityManager entityManager) where T : unmanaged, ITagComponentData
        {
            for (int i = 0; i < _buffer.Length; i++)
            {
                Entity prefabEntity = _buffer[i].Prefab;

                if (entityManager.HasComponent<T>(prefabEntity))
                {
                    return prefabEntity;
                }
            }

            return Entity.Null;
        }
    }
}