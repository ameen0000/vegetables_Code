
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Text;
using LoginAndVegitable.Models;
using LoginAndVegitable.Utilities;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace LoginAndVegitable.services.implementation
{
    public class Authservice : IAuthservice
    {

        private readonly VegetableListContext _formContext;
        private readonly IConfiguration _configuration;
        public Authservice(VegetableListContext formContext, IConfiguration configuration)
        {
            _formContext = formContext;
            _configuration = configuration;

        }





        public userreaponse Addlist(userreaponse userapi)
        {
            try
            {

                var hashedpassword = BCrypt.Net.BCrypt.HashPassword(userapi.Password);
                var newcustomer = new VegList
                {
                    Password = hashedpassword,
                    UserName = userapi.UserName,
                    
                };
                _formContext.VegLists.Add(newcustomer);
                _formContext.SaveChanges();
                userapi.Password = hashedpassword;
                return userapi;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

        public string CreateToken(userreaponse userreaponse)
        {
            List<Claim> claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name,userreaponse.UserName),
                 new Claim(ClaimTypes.NameIdentifier,userreaponse.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public List<userreaponse> get()
        {
            try
            {
                List<VegList> vegLists = _formContext.VegLists.ToList();
                var responseList = new List<userreaponse>();

                foreach (var vegList in vegLists)
                {
                    var response = new userreaponse
                    {
                        Id = vegList.Id,
                        UserName= vegList.UserName,
                        Password= vegList.Password,
                        // Add more properties based on your requirements
                    };

                    responseList.Add(response);
                }

                return responseList;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex.Message}");

                // You might want to throw the exception or handle it differently based on your needs
                throw;
            }
        }

     

        public string Verify(userreaponse login)
        {
            var storedCustomer = _formContext.VegLists.FirstOrDefault(c => c.UserName == login.UserName);


            if (storedCustomer == null)
            {
                return "Not Found";
            }

            if (!BCrypt.Net.BCrypt.Verify(login.Password, storedCustomer.Password))
            {
                return "Wrong Password";
            }
            var token = CreateToken(login);


            return token;
        }

     
    }
}
