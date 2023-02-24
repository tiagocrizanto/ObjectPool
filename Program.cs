﻿using ObjectPool;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<IObjectPool<PoolAbleItem>, ObjectPool.ObjectPool>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var pool = serviceProvider.GetRequiredService<IObjectPool<PoolAbleItem>>();

var itemA = pool.Get();

Debug.Assert(itemA is PoolAbleItem, "Should be poolAble.");
Debug.Assert(PoolAbleItem.Instances == 1, "Should have only 1");

pool.Return(itemA);
Debug.Assert(PoolAbleItem.Instances == 1, "Should have only 1");

var itemB = pool.Get();
Debug.Assert(itemA == itemB, "Should be the same object.");

var itemC = pool.Get();
Debug.Assert(PoolAbleItem.Instances == 2, "Should be 2 now");
Debug.Assert(itemB != itemC, "These should be different.");
