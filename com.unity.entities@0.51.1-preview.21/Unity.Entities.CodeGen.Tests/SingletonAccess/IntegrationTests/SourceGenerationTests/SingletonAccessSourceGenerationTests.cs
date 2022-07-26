﻿using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Unity.Entities.CodeGen.Tests.SourceGenerationTests;
using UnityEngine;

namespace Unity.Entities.CodeGen.Tests
{
    public abstract class SingletonAccessSourceGenerationTests  : IntegrationTest
    {
        protected override string ExpectedPath =>
            "Packages/com.unity.entities/Unity.Entities.CodeGen.Tests/SingletonAccess/IntegrationTests/SourceGenerationTests";

        protected void RunTest(string cSharpCode, params GeneratedType[] generatedTypes)
        {
            var (isSuccess, compilerMessages) = TestCompiler.Compile(cSharpCode, new[]
            {
                typeof(Unity.Entities.SystemBase),
                typeof(Unity.Jobs.JobHandle),
                typeof(Unity.Burst.BurstCompileAttribute),
                typeof(Unity.Mathematics.float3),
                typeof(Unity.Collections.ReadOnlyAttribute),
                typeof(Unity.Entities.CodeGen.Tests.Translation),
                typeof(Unity.Entities.Tests.EcsTestData),
                typeof(GameObject)
            }, true);

            if (!isSuccess)
                Assert.Fail($"Compilation failed with errors {string.Join(Environment.NewLine, compilerMessages.Select(msg => msg.message))}");

            RunSourceGenerationTest(generatedTypes, Path.Combine(TestCompiler.DirectoryForTestDll, TestCompiler.OutputDllName));
            TestCompiler.CleanUp();
        }
    }
}
