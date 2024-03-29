﻿using Unity.Entities;
using TinyPhysics;
using TinyPhysics.Systems;
using Unity.Transforms;
using Unity.Mathematics;

[UpdateAfter(typeof(PointerSwipeSystem))]
public class MoveBySwipeSystem : SystemBase
{
    enum playerPos { center,right,left};

    playerPos currentPos = playerPos.center;

    float3 centerPos = new float3(0, 1, -2);
    float3 rightPos = new float3(2, 1, -2);
    float3 leftPos = new float3(-2, 1, -2);

    protected override void OnUpdate()
    {
        Entities.ForEach((ref Swipeable swipeable, ref MoveBySwipe moveBySwipe) =>
        {

            switch (currentPos)
            {
                case playerPos.center:
                    if (swipeable.SwipeDirection == SwipeDirection.Right)
                    {
                        EntityManager.SetComponentData(moveBySwipe.entity, new Translation { Value = rightPos});
                        currentPos = playerPos.right;
                    }
                    else if (swipeable.SwipeDirection == SwipeDirection.Left)
                    {
                        EntityManager.SetComponentData(moveBySwipe.entity, new Translation { Value = leftPos });
                        currentPos = playerPos.left;
                    }
                    break;
                case playerPos.right:
                    if (swipeable.SwipeDirection == SwipeDirection.Left)
                    {
                        EntityManager.SetComponentData(moveBySwipe.entity, new Translation { Value = centerPos });
                        currentPos = playerPos.center;
                    }
                    break;
                case playerPos.left:
                    if (swipeable.SwipeDirection == SwipeDirection.Right)
                    {
                        EntityManager.SetComponentData(moveBySwipe.entity, new Translation { Value = centerPos });
                        currentPos = playerPos.center;
                    }
                    break;                
            }

            // Consume swipe
            swipeable.SwipeDirection = SwipeDirection.None;
        }).WithoutBurst().Run();
    }    
}
