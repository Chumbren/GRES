using System;

namespace GRES.Data.Models;

public class User
{
    public string Name {get; set;} = "";
    public string Login {get; set;} = "";
    public string PasswordHash {get; set;} = "";
}
