﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.ExtentionMethod
{
    public static class GeneratCode
    {
        public static string GuidCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
