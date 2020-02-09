﻿using System;
using System.Collections.Generic;
using System.Text;
using oyasumi_lazer.Objects;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace oyasumi_lazer.Database
{
    class OyasumiDbContext : DbContext
    {
        public OyasumiDbContext()
            : base(null)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseMySql($"server=localhost;database={Config.Get<ConfigScheme>().Database};user={Config.Get<ConfigScheme>().Username};password={Config.Get<ConfigScheme>().Password}");
        }
    }
}
