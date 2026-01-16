namespace KANIYONIKA.ECSPrefabHelper
{
    using Unity.Entities;
    using Unity.Burst;

    /// <summary>
    /// ECSPrefabRegistryAuthoring が生成した Prefab レジストリ（HolderTag + DynamicBuffer&lt;ECSPrefabRegistryEntry&gt;）から
    /// 目的の Prefab Entity を取得するための軽量ヘルパー。
    /// 
    /// 注意：この struct は内部に DynamicBuffer 参照を保持します。
    ///       基本的に「同一フレーム（同一 OnUpdate 内）」で使い切る前提で利用してください。
    /// </summary>
    [BurstCompile]
    public readonly partial struct ECSPrefabRegistryHelper
    {
        private readonly DynamicBuffer<ECSPrefabRegistryEntry> _buffer;

        public ECSPrefabRegistryHelper(DynamicBuffer<ECSPrefabRegistryEntry> buffer)
        {
            _buffer = buffer;
        }

        /// <summary>
        /// Buffer 内から「T（Prefab識別タグ）」を持つ Prefab Entity を探します。
        /// </summary>
        public bool TryGetByITag<T>(ComponentLookup<T> tagLookup, out Entity prefab) where T : unmanaged, IPrefabTagComponentData
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

        /// <summary>
        /// Buffer 内から「T（Prefab識別タグ）」を持つ Prefab Entity を探し、見つかったら返します。
        /// 見つからない場合は Entity.Null。
        /// </summary>
        public Entity GetByITag<T>(ComponentLookup<T> tagLookup) where T : unmanaged, IPrefabTagComponentData
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

        /// <summary>
        /// EntityManager を使って Buffer 内の Prefab が T を持つか判定して取得します。
        /// ComponentLookup を用意しづらい場面向け。
        /// </summary>
        public Entity GetByTagType<T>(ref EntityManager entityManager) where T : unmanaged, IPrefabTagComponentData
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
