using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

[UpdateAfter(typeof(LevelChunkMoveSystem))]
public class LevelChunkSpawnSystem : ComponentSystem
{
    public static LevelChunkSpawnSystem Instance;

    Random random;
    bool init = false;

    protected override void OnStartRunning()
    {
        Instance = this;
    }

    protected override void OnCreate()
    {
        RequireSingletonForUpdate<Spawn>();

        random = new Random(314159);                
    }

    protected override void OnUpdate()
    {
        var spawnEntity = GetSingletonEntity<Spawn>();


        int initPos = -10;

        if (!init)
        {
            for (int i = 0; i < 5; i++)
            {
                var spawnBuffer = EntityManager.GetBuffer<Spawn>(spawnEntity);
                var spawnedEntity = EntityManager.Instantiate(spawnBuffer[0].entity);
                EntityManager.SetComponentData(spawnedEntity, new Translation { Value = new float3(0, 0, initPos) });
                initPos += 10;
            }

            init = true;
            return;
        }
        
    }

    public void SpawnLevelChunk()
    {
        var spawnEntity = GetSingletonEntity<Spawn>();
        var spawnBuffer = EntityManager.GetBuffer<Spawn>(spawnEntity);
        var spawnedEntity = EntityManager.Instantiate(spawnBuffer[random.NextInt(spawnBuffer.Length)].entity);
        
        EntityManager.SetComponentData(spawnedEntity, new Translation { Value = new float3(0, 0, 30) });        
    }
}
