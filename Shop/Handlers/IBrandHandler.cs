﻿using Shop.Entities;

namespace Shop.Handlers
{
    public interface IBrandHandler
    {
        bool AddBrand(Brand brand);
        int GetId(string name);
    }
}