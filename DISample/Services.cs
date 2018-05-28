using System;
using System.Collections.Generic;
using System.Text;

namespace DISample
{
    public interface IServiceA
    {
        string GetMessage();
        string GetInstanceId();
    }
    public interface IServiceB
    {
        string GetMessage();
        string GetInstanceId();
    }

    public class ServiceAImpl : IServiceA
    {
        public IServiceB service;
        public string instanceId;

        public ServiceAImpl(IServiceB service)
        {
            this.service = service;
            this.instanceId = Guid.NewGuid().ToString();
        }

        public string GetInstanceId()
        {
            return this.instanceId;
        }

        public string GetMessage()
        {
            return this.service.GetMessage();
        }
        
    }

    public class ServiceBImpl : IServiceB
    {
        private string instanceId;

        public ServiceBImpl()
        {
            instanceId = Guid.NewGuid().ToString();
        }

        public string GetInstanceId()
        {
            return instanceId;
        }

        public string GetMessage()
        {
            return "Hello from ServiceBImpl";
        }
    }

}
