using Microsoft.AspNetCore.Identity;

namespace Identity.Models;

public class User : IdentityUser
{
    public string Name {get;set;} = "";
    public string Email {get;set;} = "";
}