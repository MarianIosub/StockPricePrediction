using System.Collections.Generic;

namespace UnitTests
{
    public interface IGenerator<T>
    {
        public IEnumerable<T> GenerateEnum(int count);
    }
}