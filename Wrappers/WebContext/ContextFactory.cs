namespace Wrappers.WebContext
{
    public abstract class ContextFactory
    {
        public abstract ICacheWrapper CacheWrapper();
        public abstract IHttpRequest HttpRequest();
        public abstract IHttpResponse HttpResponse();
        public abstract IHttpSessionState HttpSessionState();
        public abstract ExceptionThrower ExceptionThrower();
        public abstract IConfigurationManager ConfigurationManagerWrapper();
        public abstract IHttpServerUtility HttpServerUtility();
    }

    public class IisContextFactory:ContextFactory
    {
        public override ICacheWrapper CacheWrapper()
        {
            return  new CacheWrapper();
        }

        public override IHttpRequest HttpRequest()
        {
            return new HttpRequestWrapper();
        }

        public override IHttpResponse HttpResponse()
        {
            return new HttpResponseWrapper();
        }

        public override IHttpSessionState HttpSessionState()
        {
            return new HttpSessionStateWrapper();
        }

        public override ExceptionThrower ExceptionThrower()
        {
            return new ExceptionThrower(false);
        }

        public override IConfigurationManager ConfigurationManagerWrapper()
        {
            return new ConfigurationManagerWrapper();
        }

        public override IHttpServerUtility HttpServerUtility()
        {
            return new HttpServerUtilityWrapper();
        }
    }    
}
