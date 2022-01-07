﻿using DataLayer.Entities;
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

        public List<(Resource, string)>? GetUserResources(ResourceTag resourceTag)
        {
            var helpQuery = new HelperQueries();

            var resourceCategory = Enum.GetName(resourceTag);
            DatabaseStateTracker.currentResourceTag = resourceTag;

            var resourceList = dataBase.Resources.Where(t=>t.NameTag == resourceCategory)
                .OrderBy(t=>t.TimeOfPosting).ToList();

            if(resourceList is null)
                return null;

            var list = new List<(Resource, string)>();
            foreach(var resource in resourceList)
            {
                var userName = helpQuery.GetAuthorName(resource.ResourceId, null);
                list.Add((resource, userName));
            }

            return list;
        }

        public List<(Resource, string)>? GetNoReplyResources(ResourceTag resourceTag)
        {
            var helpQuery = new HelperQueries();

            var resourceCategory = Enum.GetName(resourceTag);
            DatabaseStateTracker.currentResourceTag = resourceTag;

            var resourceList = dataBase.Resources.Where(t => t.NameTag == resourceCategory).
                Where(n => n.NumberOfReplys == 0)
                .OrderBy(t => t.TimeOfPosting).ToList();

            if (resourceList is null)
                return null;

            var list = new List<(Resource, string)>();
            foreach (var resource in resourceList)
            {
                var userName = helpQuery.GetAuthorName(resource.ResourceId, null);
                list.Add((resource, userName));
            }

            return list;
        }

        public List<(Resource, string)>? GetPopularResources()
        {
            var helpQuery = new HelperQueries();

            var resourceList = dataBase.Resources.Where(t=>t.TimeOfPosting.Date == DateTime.Today)
                .OrderBy(n => n.NumberOfReplys).Take(5).ToList();

            if( resourceList is null || resourceList.Count is 0)
                return null;

            var list = new List<(Resource, string)>();
            foreach (var resource in resourceList)
            {
                var userName = helpQuery.GetAuthorName(resource.ResourceId, null);
                list.Add((resource, userName));
            }

            return list;
        }

        public void CreateResource(string content)
        {
            var newResource = new Resource(DatabaseStateTracker.GenerateEntityId(), content, Enum.GetName(DatabaseStateTracker.currentResourceTag), DatabaseStateTracker.CurrentUser.UserId);

            dataBase.Resources.Add(newResource);
            dataBase.SaveChanges();

            AddUserResource(newResource.ResourceId);
        }

        public void DislikeResource(int resourceId)
        {
            var helpQuery = new HelperQueries();
            var reputationQuery = new ReputationQueries();

            if (helpQuery.IsVoted(resourceId) ||
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanDownvoteResource)
            {
                Console.WriteLine("Vec ste dislajkali ili nemate dovoljno bodova");
                return;
            }

            var userResource = dataBase.UserResources
                .Single(ur => ur.ResourceId == resourceId && ur.UserId == DatabaseStateTracker.CurrentUser.UserId);
            userResource.IsVoted = true;

            var resource = dataBase.Resources.Single(r => r.ResourceId == resourceId);
            resource.NumberOfDislikes++;
            dataBase.SaveChanges();

            reputationQuery.GiveDownvoteResource(resource.ResourceOwnerId);
            reputationQuery.GetDownvotePoints();
        }

        public void LikeResource(int resourceId)
        {
            var helpQuery = new HelperQueries();
            var reputationQuery = new ReputationQueries();

            if (helpQuery.IsVoted(resourceId) ||
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanUpvote)
            {
                Console.WriteLine("vec ste lajkali");
                return;
            }

            var userResource = dataBase.UserResources
                .Single(ur => ur.ResourceId == resourceId && ur.UserId == DatabaseStateTracker.CurrentUser.UserId);
            userResource.IsVoted = true;

            var resource = dataBase.Resources.Single(r => r.ResourceId == resourceId);
            resource.NumberOfLikes++;

            reputationQuery.GiveUpvote(resource.ResourceOwnerId);
            reputationQuery.GetUpvoteComment();

            dataBase.SaveChanges();
        }

        public bool CommentResource(int resourceId, string content)
        {
            var helpQuery = new HelperQueries();
            var commentQuery = new CommentQueries();

            if (helpQuery.IsCommented(resourceId) || 
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanComment ||
                content.Count() is 0)
            {
                Console.WriteLine("Vec ste komentirali ili nemate dovoljno bodova");
                return false;
            }

            commentQuery.AddComment(resourceId, null, content);

            dataBase.UserResources.Find(DatabaseStateTracker.CurrentUser.UserId, resourceId).IsCommented = true;
            dataBase.SaveChanges();

            return true;
        }

        public void DeleteResource(int resourceId)
        {
            var commentQuery = new CommentQueries();
            var comments = dataBase.Comments.Where(c=>c.ResourceId == resourceId).ToList();

            foreach(var comment in comments)
            {
                var userComment=dataBase.UserComments.Find(comment.CommentOwnerId, comment.CommentId);
                dataBase.UserComments.Remove(userComment);
            }
            dataBase.RemoveRange(comments);

            var resource = dataBase.Resources.Find(resourceId);
            var resourceUser = dataBase.UserResources.Find(resource.ResourceOwnerId, resource.ResourceId);

            dataBase.UserResources.Remove(resourceUser);
            dataBase.Resources.Remove(resource);
            dataBase.SaveChanges();
        }

        public void AddUserResource(int resourceId)
        {
            if (dataBase.UserResources.Any(ur => ur.UserId == DatabaseStateTracker.CurrentUser.UserId && ur.ResourceId == resourceId))
                return;

            UserResource userResource = new UserResource(DatabaseStateTracker.CurrentUser.UserId, resourceId);
            dataBase.Add(userResource);
            dataBase.SaveChanges();
        }

        public void EditResource(int resourceId, string newContent)
        {
            var resource = dataBase.Resources.Find(resourceId);
            if (resource is null || dataBase.UserResources.Find(resource.ResourceOwnerId, resource.ResourceId) is null)
                return;

            resource.ResourceContent = newContent;
            dataBase.SaveChanges();
        }

    }
}
