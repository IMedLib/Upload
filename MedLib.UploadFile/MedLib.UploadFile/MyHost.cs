using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLib.UploadFile
{
    public class MyHost
    {
        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private NancyHost host;
        public void Start()
        {
            host = new NancyHost(new Uri("http://127.0.0.1:8888"));
            try
            {
                host.Start();
                _logger.Info("Nancy started at port 8888...");
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message);
            }
        }
        public void Stop()
        {
            try
            {
                host.Stop();
                _logger.Info("Nancy stopped...");
            }
            catch (Exception exception)
            {
                _logger.Error(exception.Message);
            }
        }
    }
}
