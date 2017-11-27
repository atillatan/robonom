using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Robonom.Common
{
    public class HttpConnectionClient
    {
        #region Singleton Implementation

        private static HttpConnectionClient _httpConnectionClient = null;

        private static object syncRoot = new Object();
        private HttpConnectionClient()
        {
            Initialize();
        }
        public static HttpConnectionClient Current
        {
            get
            {
                if (_baseAddress==null)
                {
                    throw  new Exception("You must run Configure() method before instance!");
                }

                if (_httpConnectionClient == null)
                {
                    lock (syncRoot)
                    {
                        if (_httpConnectionClient == null)
                            _httpConnectionClient = new HttpConnectionClient();
                    }
                }
                return _httpConnectionClient;
            }
        }

        #endregion Singleton Implementation

        private static HttpClient _httpClient = null;

        private static  Uri _baseAddress = null;
        private HttpClient Initialize()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
            return _httpClient;
        }


        public HttpClient Client
        {
            get
            {
                if (_httpClient == null)
                {
                    return Initialize();
                }

                return _httpClient;
            }
        }

        public static void Configure(Uri baseAddress)
        {
            _baseAddress = baseAddress;
        }
    }
}
