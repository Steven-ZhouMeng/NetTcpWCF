using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace NetTcpServer
{
    // Open two solutions, one to start the server, one to start the client. 
    // Please refer to http://www.cnblogs.com/scy251147/archive/2012/11/23/2784902.html. We use svcutil to generate the 
    // proxy, also we can add service reference directly to generate the proxy.
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(StudentService)))
            {
                ServiceMetadataBehavior smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (smb == null)
                    host.Description.Behaviors.Add(new ServiceMetadataBehavior());

                //暴露出元数据，以便能够让SvcUtil.exe自动生成配置文件
                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

                //开启服务
                host.Open();
                Console.WriteLine("Service listen begin to listen...");
                Console.WriteLine("press any key to teriminate...");
                Console.ReadKey();
                host.Abort();
                host.Close();
            }
        }
    }
}
