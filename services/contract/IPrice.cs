using LoginAndVegitable.Utilities;

namespace LoginAndVegitable.services.contract
{
    public interface IPrice
    {
        public List<priceresponse> GetPrices();

        public PriceRequest postprice(PriceRequest priceApi);
    }
}
