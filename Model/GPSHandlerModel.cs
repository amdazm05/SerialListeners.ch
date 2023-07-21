using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace MapDisplay.model
{

    class GPSHandlerModel
    {
        private 
            SerialPort _serialHandler;
        #region Events
        public 
            delegate void OnRecieveRMCString(string _nmeaStringCheck);
        public
            event OnRecieveRMCString DataRecieved;
        #endregion

        #region Configs
        private
            int _baudRate;
        
        private
            string _PortName;
        #endregion End of Configs

        public
            GPSHandlerModel(string PortName, int baudRate =9600)
            {
                _PortName = PortName;
                _baudRate = baudRate;

                //Reading Serial Port
                _serialHandler = new SerialPort();
                _serialHandler.PortName = _PortName;
                _serialHandler.BaudRate = _baudRate;
                
            }
        
        public void Poll_Data()
        {
            while(true)
            {
                string newMessage = _serialHandler.ReadLine();
                Thread.Sleep(500);
            }
        }

        public
            void StartListening()
            {
                if(!_serialHandler.IsOpen)
                {
                    _serialHandler.Open();
                    Thread _thread = new Thread(Poll_Data);
                    _thread.Start();
                }
                else
                {
                    _serialHandler.Close();
                }
            }
    }
}
