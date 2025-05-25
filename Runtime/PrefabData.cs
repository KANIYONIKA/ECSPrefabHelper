namespace KANIYONIKA.ECSPrefabHelper
{
    using Unity.Entities;

    public struct PrefabTag : ITagComponentData { }

    [System.Serializable]
    public struct PrefabData : IComponentData
    {
    }

    [System.Serializable]
    public struct PrefabBuffer : IBufferElementData
    {
        public Entity Prefab;
    }

    /// <summary>
    /// 共通のタグ用インターフェース。タグ（データなしのマーカーコンポーネント）であることを明示するために使用します。
    /// 制約付きジェネリックなどで「タグであるコンポーネント」だけを対象に処理したいときに便利です。
    /// </summary>
    public interface ITagComponentData : IComponentData { }
}