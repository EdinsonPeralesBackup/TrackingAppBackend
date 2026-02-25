using System.Data;

namespace Tracking.Application.Common.Interface
{
    public interface IDataBase
    {
        IDbConnection GetConnection();
    }
}
