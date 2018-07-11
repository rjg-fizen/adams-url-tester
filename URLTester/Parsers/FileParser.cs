using Core.Objects;
using System;
using System.Collections.Generic;

namespace Parsers
{
    public class FileParser<T>
    {
        private readonly IParser<T> parser;

        public FileParser(IParser<T> parser)
        {

            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }

            this.parser = parser;

        }

        public List<T> ParseFile<T>(string filePath, ref List<ErrorMessage> errorMessages) where T : new()
        {
            return (List<T>) this.parser.ParseFile<T>(filePath, ref errorMessages);
        }
    }
}
