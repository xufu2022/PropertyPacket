namespace PropertyTenants.Gateways.GraphQL.Types.Errors
{
    public class FeatureError
    {
        public string Message { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public class FeatureNotFoundError : FeatureError
    {
        public FeatureNotFoundError(int id)
        {
            Message = $"Feature with ID {id} was not found.";
            Code = "FEATURE_NOT_FOUND";
        }
    }

    public class FeatureGroupNotFoundError : FeatureError
    {
        public FeatureGroupNotFoundError(int id)
        {
            Message = $"Feature group with ID {id} was not found.";
            Code = "FEATURE_GROUP_NOT_FOUND";
        }
    }
}
