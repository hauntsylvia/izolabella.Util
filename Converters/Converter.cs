using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Util.Converters;
public abstract class Converter
{
    public abstract object? FromString(string? Data);
}
