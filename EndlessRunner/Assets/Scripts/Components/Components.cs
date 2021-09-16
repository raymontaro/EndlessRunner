using Unity.Entities;

public struct Spawn : IBufferElementData
{
    public Entity entity;
}

public struct UIElement : IBufferElementData
{
    public Entity entity;
}