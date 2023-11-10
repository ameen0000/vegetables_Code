
using LoginAndVegitable.Models;
using LoginAndVegitable.services.contract;

namespace Vegetable_NamesList.services.implementation
{
    public class VegeServices : IVegeServices
    {
        private readonly VegetableListContext _context;

        public VegeServices(VegetableListContext context)
        {
            _context = context;
        }

        public List<VegNamesForlist> GetVegs()
        {
            try
            {
                List<VegNamesForlist> veglist = _context.VegNamesForlists.ToList();
                var qatar = new List<VegNamesForlist>();
                foreach (var item in veglist)
                {
                    var response = new VegNamesForlist();
                    response.Id = item.Id;
                    response.VegetableName = item.VegetableName;


                }
                return veglist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
