﻿using System;
using System.Text;

namespace Earl
{
    internal class Variable
    {
        public string Name { get; set; }

        public int? MaxLength { get; set; }

        public bool IsExploded { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(Name);
            if (MaxLength != null)
            {
                builder.Append(":");
                builder.Append(MaxLength);
            }
            if (IsExploded)
            {
                builder.Append("*");
            }
            return builder.ToString();
        }
    }
}
