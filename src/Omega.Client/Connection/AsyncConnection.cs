using System.Threading.Tasks;

namespace Omega.Client.Connection
{
    //public class AsyncConnection : IConnection
    //{
    //    private readonly IConnection wrappedConnection;
    //    private readonly TaskFactory taskFactory;
    //    private readonly object connLock = new object();

    //    public AsyncConnection(IConnection wrappedConnection, TaskFactory taskFactory = null)
    //    {
    //        this.wrappedConnection = wrappedConnection;
    //        //this.taskFactory = taskFactory ?? Task.Factory.
    //    }

    //    public void Dispose()
    //    {
    //        lock(connLock)
    //        {
    //            wrappedConnection.Dispose();
    //        }
    //    }

    //    public void Open()
    //    {
    //        lock(connLock)
    //        {
    //            wrappedConnection.Open();
    //        }
    //    }

    //    public void Send(Packet packet, SignType type, SignAddress address, PacketFormat format)
    //    {
    //        lock(connLock)
    //        {
    //            wrappedConnection.Send(packet, type, address, format);
    //        }
    //    }
    //}
}