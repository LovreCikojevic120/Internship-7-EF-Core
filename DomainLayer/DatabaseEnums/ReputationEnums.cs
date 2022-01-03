namespace DomainLayer.DatabaseEnums
{
    public enum ReputationPoints
    {
        CanComment = 1,
        CanReply = 3,
        CanUpvote = 5,
        CanDownvoteComment = 15,
        CanDownvoteResource = 20,
        CanEditOwnEntity = 100,
        CanEditAnyEntity = 250,
        CanDeleteComment = 500,
        IsTrusted = 1000,
        IsOrganizer = 100000
    }
}
