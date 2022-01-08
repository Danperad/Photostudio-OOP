﻿using System.ComponentModel.DataAnnotations;

namespace PhotostudioDLL.Entities;

public class Equipment
{
    public static void Add(Equipment equipment)
    {
        ContextDB.Add(equipment);
    }

    public static List<Equipment> Get()
    {
        return ContextDB.GetEquipments();
    }

    public static void Update()
    {
        ContextDB.Save();
    }

    #region Properties

    public int ID { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }

    public virtual List<Inventory> Inventories { get; set; }

    #endregion

    #region Constructors

    public Equipment()
    {
        Inventories = new List<Inventory>();
    }

    public Equipment(string Title, string Description)
    {
        this.Title = Title;
        this.Description = Description;
        Inventories = new List<Inventory>();
    }

    #endregion
}