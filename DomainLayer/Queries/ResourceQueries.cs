using DataLayer.Entities;
using DataLayer.Entities.Models;
using DomainLayer.DatabaseEnums;
using DomainLayer.Entities;

namespace DomainLayer.Queries
{
    public class ResourceQueries
    {
        private readonly StackInternshipDbContext dataBase;
        public ResourceQueries()
        {
            dataBase = DbContextFactory.GetStackInternshipDbContext();
        }

        public List<Resource>? GetUserRecources(ResourceTag resourceTag)
        {
            var resourceCategory = Enum.GetName(resourceTag);

            var resourceList = dataBase.Resources.Where(t=>t.NameTag==resourceCategory)
                .OrderBy(t=>t.TimeOfPosting).ToList();

            if(resourceList is null)
                return null;

            return resourceList;
        }

        public List<Resource>? GetNoReplyResources(ResourceTag resourceTag)
        {
            var resourceCategory = Enum.GetName(resourceTag);

            var resourceList = dataBase.Resources.Where(t => t.NameTag == resourceCategory).
                Where(n => n.NumberOfReplys == 0)
                .OrderBy(t => t.TimeOfPosting).ToList();

            if (resourceList is null)
                return null;

            return resourceList;
        }

        public List<Resource>? GetPopularResources()
        {
            var resourceList = dataBase.Resources.OrderBy(n => n.NumberOfReplys).Take(5).ToList();

            if( resourceList is null || resourceList.Count is 0)
                return null;

            return resourceList;
        }
    }
}
