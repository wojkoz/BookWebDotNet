using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace BookWebTests
{
    internal class DbSetMock
    {
        public static DbSet<T> GenerateMockSet<T>(IEnumerable<T> enumerableData) where T : class
        {
            var data = enumerableData.AsQueryable();
            var mockSet = Substitute.For<DbSet<T>, IQueryable<T>>();

            ((IQueryable<T>)mockSet).Provider.Returns(data.Provider);
            ((IQueryable<T>)mockSet).Expression.Returns(data.Expression);
            ((IQueryable<T>)mockSet).ElementType.Returns(data.ElementType);
            ((IQueryable<T>)mockSet).GetEnumerator().Returns(data.GetEnumerator());
            return mockSet;
        }
    }
}
