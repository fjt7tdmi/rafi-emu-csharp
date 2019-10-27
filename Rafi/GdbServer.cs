using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Rafi
{
    internal class GdbServer : IDisposable
    {
        private readonly TcpListener tcpListener;
        private readonly Task task;

        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private bool disposedValue = false;

        public GdbServer(int port)
        {
            tcpListener = new TcpListener(new IPEndPoint(IPAddress.Loopback, port));
            tcpListener.Start();

            task = new Task(ServerThread, cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
            task.Start();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    cancellationTokenSource.Cancel();

                    tcpListener.Stop();
                    task.Dispose();

                    cancellationTokenSource.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }

        private void ServerThread()
        {
            try
            {
                while (true)
                {
                    var tcpClient = tcpListener.AcceptTcpClient();

                    using (var stream = tcpClient.GetStream())
                    {
                        GdbSession.Process(stream);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(GdbServer)} finished.");
                Console.WriteLine(e.Message);
            }
        }
    }
}
