namespace Tracking.Application.Common.Interface
{
    public interface IAcortadorServices
    {
        Task<string> AcordarEnlace(string longUrl);
    }
}
