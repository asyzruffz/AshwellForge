namespace AshwellForge.Core.RtmpServer;

public interface ISessionHandle : ISessionControl, ISessionInfo
{
    void Send(IDataBuffer dataBuffer, Action<bool>? callback = null);
    void Send(IRentedBuffer rentedBuffer, Action<bool>? callback = null);
    void Send(Action<IDataBuffer> writer, Action<bool>? callback = null);

    ValueTask SendAsync(IDataBuffer dataBuffer);
    ValueTask SendAsync(IRentedBuffer rentedBuffer);
    ValueTask SendAsync(Action<IDataBuffer> writer);
}
