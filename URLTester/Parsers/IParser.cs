using System.Collections.Generic;
using Core.Objects;

namespace Parsers
{
    public interface IParser<T>
    {
        List<T> ParseFile<T>(string filePath, ref List<ErrorMessage> errorMessages) where T : new();
    }
}
