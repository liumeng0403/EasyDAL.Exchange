﻿using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class JoinX 
        : Operator, IMethodObject
    {

        internal JoinX(Context dc)
            : base(dc)
        { }

    }
}
