using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace osuTablet
{
    /// <summary>
    /// CU(CommunicationUdp) 클래스는 UDP를
    /// 이용한 소켓 통신을 합니다.
    /// </summary>
    class CommunicationUdp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">데이터를 받을 변수를 넣습니다.</param>
        public delegate void ReceiveData(ref byte[] data);

        public ReceiveData ReceiveDataCallback = null;

        private UdpClient client = null;

        private IPEndPoint endpoint = null;

        private Thread commThread = null;
        
        private Boolean isRunning = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public void Open(int port)
        {
            endpoint = new IPEndPoint(IPAddress.Any, port);
            client = new UdpClient(endpoint);
            isRunning = true;
            commThread = new Thread(GetDataThread);
            commThread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            if (client == null)
            {
                return;
            }

            isRunning = false;
            commThread.Join(3939);

            if (commThread.ThreadState == ThreadState.Running)
            {
                commThread.Abort();
            }
            client.Close();
        }

        private void GetDataThread()
        {
            while (isRunning)
            {
                byte[] data = client.Receive(ref endpoint);

                var str = Encoding.ASCII.GetString(data);

                foreach (byte i in data)
                {
                    System.Console.Write(i);
                    System.Console.Write(' ');
                }

                System.Console.WriteLine("");

                foreach (char i in str)
                {
                    System.Console.Write(i);
                    System.Console.Write(' ');
                }

                System.Console.WriteLine("");


                Point point = new Point();
                point.X = Int32.Parse(Encoding.Default.GetString(data, 0, 3));

                point.Y = Int32.Parse(Encoding.Default.GetString(data, 3, 3));

                InputAssistant.MoveCursor(point);

                if (ReceiveDataCallback != null)
                {
                    ReceiveDataCallback(ref data);
                }
            }
        }
    }
}
