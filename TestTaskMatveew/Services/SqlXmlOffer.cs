using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Xml;
using TestTaskMatveew.DAL.Context;
using TestTaskMatveew.Domain;
using TestTaskMatveew.Services.Interfaces;

namespace TestTaskMatveew.Services
{
    public class SqlXmlOffer : IXmlOffer
    {
        private readonly TestTaskMatveewDB _db;
        private readonly ILogger<SqlXmlOffer> _Logger;

        public SqlXmlOffer(TestTaskMatveewDB db, ILogger<SqlXmlOffer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public async Task<Offer> GetOffer(string Url, string Id)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1254");

            var offer = new Offer();

            //var offerExist = _db.Offers.Where(p => p.Id == Int32.Parse(Id));
            //if (offerExist is not null)
            //{
            //    _Logger.LogInformation($"Оффер с ID: {Id} уже добавлен в БД");
            //    return new Offer();
            //}

            using (XmlReader xmlReader = XmlReader.Create(Url, new XmlReaderSettings() { DtdProcessing = DtdProcessing.Parse }))
            {
                while (xmlReader.Read())
                {
                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "offer"))
                    {
                        if (xmlReader.HasAttributes)
                        {
                            if (!(xmlReader.GetAttribute("id") == Id))
                                continue;

                            offer.Id = int.TryParse(xmlReader.GetAttribute("id"), out var id) ? id : 0;

                            xmlReader.ReadToFollowing("url");
                            offer.Url = xmlReader.ReadElementContentAsString();

                            xmlReader.ReadToFollowing("price");
                            offer.Price = int.TryParse(xmlReader.ReadElementContentAsString(), out var price) ? id : 0;

                            xmlReader.ReadToFollowing("currencyId");
                            offer.CurrencyId = xmlReader.ReadElementContentAsString();

                            xmlReader.ReadToFollowing("categoryId");
                            offer.CategoryId = xmlReader.ReadElementContentAsString();

                            xmlReader.ReadToFollowing("picture");
                            offer.Picture = xmlReader.ReadElementContentAsString();

                            xmlReader.ReadToFollowing("delivery");
                            offer.Delivery = Convert.ToBoolean(xmlReader.ReadElementContentAsString());

                            xmlReader.ReadToFollowing("artist");
                            offer.Artist = xmlReader.ReadElementContentAsString();

                            xmlReader.ReadToFollowing("title");
                            offer.Title = xmlReader.ReadElementContentAsString();

                            xmlReader.ReadToFollowing("year");
                            offer.Year = xmlReader.ReadElementContentAsString();

                            xmlReader.ReadToFollowing("media");
                            offer.Media = xmlReader.ReadElementContentAsString();

                            xmlReader.ReadToFollowing("description");
                            offer.Description = xmlReader.ReadElementContentAsString();

                            if (_db.Offers.Contains(offer))
                            {
                                _Logger.LogInformation($"Оффер с ID: {Id} уже добавлен в БД");
                                return new Offer();
                            }

                            _db.Database.OpenConnection();
                            try
                            {
                                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers ON;");
                                _db.Offers.Add(offer);
                                await _db.SaveChangesAsync();
                                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Offers OFF;");
                            }
                            finally
                            {
                                _db.Database.CloseConnection();
                            }

                            _Logger.LogInformation($"Оффер с ID: {Id} успешно добавлен в БД");
                            break;
                        }
                    }
                }
            }

            return offer;
        }


    }
}
