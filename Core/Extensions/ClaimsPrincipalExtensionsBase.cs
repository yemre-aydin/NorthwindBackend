namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensionsBase
    {
        public static List<string> Claims(this ClaimsPrincipalExtensions claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }
    }
}