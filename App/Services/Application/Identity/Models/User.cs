namespace Identity.Models;

public class User : IdentityBaseEntity
{
    public string Name {get;set;} = "";
    public string Email {get;set;} = "";
}