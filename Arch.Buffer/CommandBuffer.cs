namespace Arch.CommandBuffer
{
    public sealed partial class CommandBuffer : ICommandBuffer
    {
        private const int INITIAL_CAPACITY = 64;
        
        private readonly List<IBufferCommand> _commands;

        public CommandBuffer(int initialCapacity = INITIAL_CAPACITY)
        {
            _commands = new(initialCapacity);
        }

        public void AddCommand(IBufferCommand command)
        {
            _commands.Add(command);
        }

        public void Playback(bool reset = true)
        {
            lock (this)
            {
                foreach (IBufferCommand bufferCommand in _commands)
                {
                    bufferCommand.Execute();
                }

                if (reset)
                {
                    Reset();
                }
            }
        }

        public void Reset()
        {
            _commands.Clear();
        }

        public void Dispose()
        {
            Reset();
        }
    }
}