using Arch.Core;
using Arch.CommandBuffer;
using Arch.CommandBuffer.EntityCommands;
using Arch.Core.Utils;
using Arch.Core.Extensions;
using static NUnit.Framework.Assert;

namespace Arch.Buffer.Tests
{
    public struct Transform
    {
        public float X;
        public float Y;
    }

    public struct Speed
    {
        public float Value;
    }

    [TestFixture]
    public class CommandBufferTests
    {
        [Test]
        public void CreateEntityTest()
        {
            World world = World.Create();
            ICommandBuffer buffer = new Arch.CommandBuffer.CommandBuffer();
            ComponentType[] types = {
                typeof(Transform),
                typeof(Speed),
            };

            float x = 20;
            float y = 20;
            float speed = 10;

            CreateEntityCommand createEntity = new(world, types, new object[]
            {
                new Transform
                {
                    X = x,
                    Y = y
                },
                new Speed
                {
                    Value = speed
                }
            });

            buffer.AddCommand(createEntity);

            buffer.Playback();

            List<Entity> entities = new();
            world.GetEntities(new QueryDescription().WithAll<Transform>().WithAll<Speed>(), entities);

            Multiple(() =>
            {
                That(entities.Count, Is.EqualTo(1));
                That(entities[0].Has<Transform>());
                That(entities[0].Has<Speed>());
            });

            ref Transform transform = ref entities[0].Get<Transform>();
            Multiple(() =>
            {
                That(entities[0].Get<Transform>().X, Is.EqualTo(x));
                That(entities[0].Get<Transform>().Y, Is.EqualTo(x));
                That(entities[0].Get<Speed>().Value, Is.EqualTo(speed));
            });

            world.Dispose();
            buffer.Dispose();
        }
    }
}
