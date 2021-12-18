﻿namespace PhotostudioDLL.Entity;

public class Inventory
{
    public int ID { get; set; }

    public virtual List<Equipment> Equipment { get; set; }
    [Required] public virtual Service Service { get; set; }
    [Required] public string Appointment { get; set; }

    public Inventory()
    {
        Equipment = new List<Equipment>();
    }
}