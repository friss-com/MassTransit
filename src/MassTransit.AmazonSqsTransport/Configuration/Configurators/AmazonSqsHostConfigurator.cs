namespace MassTransit.AmazonSqsTransport.Configuration.Configurators
{
    using System;
    using Amazon;
    using Amazon.Runtime;
    using Amazon.SimpleNotificationService;
    using Amazon.SQS;


    public class AmazonSqsHostConfigurator :
        IAmazonSqsHostConfigurator
    {
        readonly ConfigurationHostSettings _settings;

        string _accessKey;
        string _secretKey;

        public AmazonSqsHostConfigurator(Uri address)
        {
            var hostAddress = new AmazonSqsHostAddress(address);

            var regionEndpoint = RegionEndpoint.GetBySystemName(hostAddress.Host);

            _settings = new ConfigurationHostSettings()
            {
                Scope = hostAddress.Scope,
                Region = regionEndpoint,
                AmazonSqsConfig = new AmazonSQSConfig {RegionEndpoint = regionEndpoint},
                AmazonSnsConfig = new AmazonSimpleNotificationServiceConfig {RegionEndpoint = regionEndpoint}
            };

            if (!string.IsNullOrEmpty(address.UserInfo))
            {
                string[] parts = address.UserInfo.Split(':');
                _accessKey = parts[0];

                if (parts.Length >= 2)
                {
                    _secretKey = parts[1];
                    SetBasicCredentials();
                }
            }
        }

        public AmazonSqsHostSettings Settings => _settings;

        public void AccessKey(string accessKey)
        {
            _accessKey = accessKey;
            SetBasicCredentials();
        }

        public void SecretKey(string secretKey)
        {
            _secretKey = secretKey;
            SetBasicCredentials();
        }

        public void Credentials(AWSCredentials credentials)
        {
            _settings.Credentials = credentials;
        }

        public void Config(AmazonSQSConfig config)
        {
            _settings.AmazonSqsConfig = config;
        }

        public void Config(AmazonSimpleNotificationServiceConfig config)
        {
            _settings.AmazonSnsConfig = config;
        }

        public bool CopyHeadersToMessageAttributes
        {
            set => _settings.CopyHeadersToMessageAttributes = value;
        }

        void SetBasicCredentials()
        {
            if (string.IsNullOrEmpty(_accessKey) || string.IsNullOrEmpty(_secretKey))
                return;

            _settings.Credentials = new BasicAWSCredentials(_accessKey, _secretKey);
        }
    }
}
