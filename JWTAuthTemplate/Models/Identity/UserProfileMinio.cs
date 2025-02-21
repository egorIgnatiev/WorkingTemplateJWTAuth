namespace JWTAuthTemplate.Models.Identity
{
    public class UserProfileMinio
    {
        public int Id { get; set; } // Primary Key
        public string UserName { get; set; }
        public string ExternalMinioKey { get; set; }
    }
}
