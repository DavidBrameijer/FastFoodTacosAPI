﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TacoFastFoodAPI.Models;

public partial class Drink
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public float? Cost { get; set; }

    public bool? Slushie { get; set; }

    [JsonIgnore]
    public virtual ICollection<Combo> Combos { get; set; } = new List<Combo>();
}
