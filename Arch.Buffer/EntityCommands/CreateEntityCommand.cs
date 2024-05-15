using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.CommandBuffer.EntityCommands
{
    public class CreateEntityCommand : IBufferCommand
    {
        private readonly World _world;
        private readonly ComponentType[] _types;
        private readonly object[] _components;

        public CreateEntityCommand(World world, ComponentType[] types, object[] components)
        {
            _world = world;
            _types = types;
            _components = components;
        }

        public void Execute()
        {
            _world.Create(_types).SetRange(_components);
        }
    }
}