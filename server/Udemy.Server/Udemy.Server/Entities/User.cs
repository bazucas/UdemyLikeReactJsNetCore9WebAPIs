﻿namespace Udemy.Server.Entities;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    public ICollection<UserVideoProgress> UserVideoProgresses { get; set; }
}
