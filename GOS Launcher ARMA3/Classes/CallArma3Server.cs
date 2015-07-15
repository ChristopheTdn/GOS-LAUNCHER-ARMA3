using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arma3Launcher{
    
    /**
     * Permet de récupérer les joueurs d'un serveur
     **/
    class Arma3ServerPlayer {

        public List<string> list = new List<string>();

        public void populatePlayers(string response) {
            char[] datas = response.Substring(6).ToCharArray();
            try
            {
                for (int i = 0; i < response.Length; i++)
                {
                    if (datas[i] == '\0')
                    {
                        string playerName = readPlayer(i, datas);
                        list.Add(playerName);
                        i = i + 9 + playerName.Length;
                    }
                }
            }catch (IndexOutOfRangeException){
                //This is the end ...
            }
        }

        private string readPlayer(int start,char[] datas){
            try{
                int i = start + 1;
                string playerName = "";
                while (datas[i] != '\0')
                {
                    playerName += datas[i];
                    i++;
                }
                return playerName;
            }catch (IndexOutOfRangeException){
                return null;
            }
        }
    }
    
    /**
     * Permet de récupérer les infos d'un serveur
     **/
    class Arma3ServerBean {

        private String serverName;
        private String mapName;
        private String missionName;
        private String connected;

        public void setServerName(String s){
            serverName = s.Substring(5);
        }

        public void setMapName(String s){
            mapName=s; //faire la correspondance
        }

        public void setMissionName(String s){
            missionName = s;      
        }

        public void setConnected(String s){
            if(s.Length>1){
                connected =  ((int)s.ToCharArray()[0])+"/"+ ((int)s.ToCharArray()[1]);
            }else{
                connected = "0/"+ (int)s.ToCharArray()[0];
            }
        }

        public String getServerName(){
            return serverName;
        }
        public String getMapName(){
            return mapName;
        }
        public String getMissionName(){
            return missionName;
        }
        public String getConnected(){
            return connected;
        }
    }

    /**
     * Permet d'effectuer les appels au serveur
     **/
    class CallArma3Server {

        public Arma3ServerBean call(string ip, int port){

            byte[] prefix = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            byte[] query0 = System.Text.Encoding.UTF8.GetBytes("TSource Engine Query\0");
            char[] endData = new char[] {'\0','\0'};
            
            IEnumerable<byte> tmp = prefix.Concat(query0);
            byte[] query = tmp.ToArray();

            Arma3ServerBean bean = new Arma3ServerBean();
            byte[] responseByte = request(ip, port, query);
            if (responseByte == null){
                bean.setServerName("#####Server Error !");
                bean.setMapName("-");
                bean.setMissionName("-");
                bean.setConnected("\0");
                return bean;
            }

            string response = Encoding.UTF8.GetString(responseByte).TrimEnd(endData);
            string[] datas = response.Split(new Char[] { '\0' });
            bean.setServerName(datas[0]);
            if (datas[1].Length == 0)
                bean.setMapName("No Map");
            else
                bean.setMapName(datas[1]);
            if (datas[1].Length == 0)
                bean.setMissionName("No Mission");
            else
                bean.setMissionName(datas[3]);
            if (datas[6].Length == 0)
                bean.setConnected(datas[7]);
            else
                bean.setConnected(datas[6]);

            return bean;
        }

        public Arma3ServerPlayer getPlayers(string ip, int port) {
            Arma3ServerPlayer arma3serverPlayer=new Arma3ServerPlayer();

            byte[] query0 = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF,0x55};
            byte[] query1 = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }; 

            IEnumerable<byte> tmp = query0.Concat(query1);
            byte[] query = tmp.ToArray();

            byte[] challengerNumber = request(ip, port, query);
            challengerNumber[4]=0x55;

            byte[] response = request(ip, port, challengerNumber);
            arma3serverPlayer.populatePlayers(Encoding.UTF8.GetString(response));

            return arma3serverPlayer;
        }

        private byte[] request(string ip, int port,byte[] request) {
            var multicastAddress = IPAddress.Parse(ip);
            var signal = new ManualResetEvent(false);
            var multicastPort = port;
            byte[] responseByte = null;

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)) {
                var multicastEp = new IPEndPoint(multicastAddress, multicastPort);
                EndPoint localEp = new IPEndPoint(IPAddress.Any, multicastPort);
                socket.Bind(localEp);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 0); // only LAN

                var thd = new Thread(() => {
                    try
                    {
                        byte[] tmpByte = new byte[8000];
                        int size = socket.ReceiveFrom(tmpByte, ref localEp);
                        responseByte = tmpByte.Take(size).ToArray();
                    } catch {
                        //Y'a plus de chaussette !
                    } finally {
                        signal.Set();
                    }
                });
                signal.Reset();
                thd.Start();

                socket.SendTo(request, 0, request.Length, SocketFlags.None, multicastEp);
                bool transmitOK = signal.WaitOne(2000);
                if (!transmitOK) {
                    responseByte = null;
                }
                socket.Close();
                return responseByte;
            }
        }
    }
}