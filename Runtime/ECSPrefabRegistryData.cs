namespace KANIYONIKA.ECSPrefabHelper
{
    using Unity.Entities;

    /// <summary>
    /// Prefab レジストリ（Prefab一覧を保持するホルダーEntity）を識別するためのタグ。
    /// System 側でこのタグを手がかりにレジストリEntityを取得し、
    /// そこから DynamicBuffer&lt;ECSPrefabRegistryEntry&gt; を取り出して利用します。
    /// </summary>
    public struct ECSPrefabRegistryTag : IComponentData { }

    /// <summary>
    /// Prefab レジストリ（ホルダーEntity）に付与する拡張用データ枠。
    /// 現状は空ですが、将来的にレジストリのメタ情報（バージョン、識別子等）を追加したい場合に使用します。
    /// </summary>
    [System.Serializable]
    public struct ECSPrefabRegistryData : IComponentData
    {
    }

    /// <summary>
    /// Prefab レジストリに登録する Prefab の1エントリ。
    /// </summary>
    [System.Serializable]
    public struct ECSPrefabRegistryEntry : IBufferElementData
    {
        public Entity Prefab;
    }

    /// <summary>
    /// Prefab を識別するためのタグ用インターフェース。
    /// ECSPrefabRegistryEntry に登録された Prefab Entity から、目的の Prefab を検索するために利用します。
    /// （例：Prefab_Runner_Frog / Prefab_CubePart など）
    /// </summary>
    public interface IPrefabTagComponentData : IComponentData { }
}
