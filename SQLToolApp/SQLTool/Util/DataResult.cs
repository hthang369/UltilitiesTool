﻿using System;

namespace SQLTool.Services
{
    public class DataResult<TData> : ErrorHelper
    {
        public TData Target { get; set; }

        public bool HasErrors
        {
            get
            {
                return (base.Errors.Count > 0);
            }
        }
    }
}
