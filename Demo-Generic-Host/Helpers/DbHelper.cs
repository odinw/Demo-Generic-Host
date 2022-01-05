using Microsoft.Extensions.Configuration;

namespace Demo_Generic_Host.Helpers
{
    public class DbHelper : IDbHelper
    {
        readonly IConfiguration _configuration;

        public DbHelper(IConfiguration configuration) => _configuration = configuration;

        public string GetDbInfo()
        {
            return $"Environment : {_configuration["Environment"]}";
        }
    }
}
