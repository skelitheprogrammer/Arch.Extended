namespace Arch.CommandBuffer
{
    public interface ICommandBuffer : IDisposable
    {
        void Playback(bool reset = true);
        void AddCommand(IBufferCommand command);
    }
}