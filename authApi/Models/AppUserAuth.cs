namespace authApi.Models
{
  public class AppUserAuth
  {
    public string UserName { get; set; }
    public string BearerToken { get; set; }
    public bool IsAuthenticated { get; set; }
    public AppUserAuth() : base()
    {
      this.UserName = "Not authorized";
      this.BearerToken = string.Empty;
    }
  }
}