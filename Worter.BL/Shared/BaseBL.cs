using Microsoft.Extensions.Configuration;
using Worter.DAO.Models;

namespace Worter.BL.Shared
{
    public abstract class BaseBL
    {
        protected readonly WorterContext context;
        protected readonly IConfiguration configuration;

        public BaseBL(WorterContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
    }
}
