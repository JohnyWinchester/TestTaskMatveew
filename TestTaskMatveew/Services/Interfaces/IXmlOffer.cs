using TestTaskMatveew.Domain;

namespace TestTaskMatveew.Services.Interfaces
{
    public interface IXmlOffer
    {
        Task<Offer> GetOffer(string Url, string Id);
    }
}
