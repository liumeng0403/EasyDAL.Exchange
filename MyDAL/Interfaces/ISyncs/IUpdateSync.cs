﻿using System.Data;

namespace MyDAL.Interfaces.ISyncs
{
    internal interface IUpdate<M>
    where M : class
    {
        int Update();
    }
}
