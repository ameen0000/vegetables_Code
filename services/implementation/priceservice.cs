using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Core;
using LoginAndVegitable.Models;
using LoginAndVegitable.services.contract;
using LoginAndVegitable.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LoginAndVegitable.services.implementation
{
    public class PriceService : IPrice
    {
        private readonly VegetableListContext _context;

        public PriceService(VegetableListContext context)
        {
            _context = context;
        }

        public List<priceresponse> GetPrices()
        {
            try
            {
                List<Price> priceList = _context.Prices.Include(x => x.Vegetable).Include(x => x.VegetableNavigation).ToList();
                var responseList = new List<priceresponse>();

                foreach (var price in priceList)
                {
                    var response = new priceresponse
                    {
                        Id= price.Id,
                        VegetableId = price.VegetableId,
                        VegetableName = price.VegetableNavigation?.VegetableName ?? "",
                        Price1 = price.Price1,
                        UserId = price.UserId,
                        UserName = price.Vegetable?.UserName ?? "",
                    };

                    responseList.Add(response);
                }

                return responseList;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Or return an appropriate response
            }
        }

        public PriceRequest postprice(PriceRequest priceApi)
        {
            try
            {
                var price = new Price();
                price.Price1 = priceApi.Price1;
                price.UserId = priceApi.UserId;
                price.VegetableId = priceApi.VegetableId;
                
                _context.Prices.Add(price);
                _context.SaveChanges();
                return priceApi;
              

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
