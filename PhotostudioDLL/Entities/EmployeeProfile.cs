﻿using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entities;

public class EmployeeProfile
{
    #region Properties

    public int ID { get; set; }
    public virtual Employee Employee { get; set; }
    [MinLength(3)] public string Login { get; set; }
    public string Password { get; set; }

    #endregion

    #region Constructors

    public EmployeeProfile()
    {
    }

    public EmployeeProfile(string Login, string Password)
    {
        this.Login = Login;
        this.Password = Password;
    }

    #endregion
}