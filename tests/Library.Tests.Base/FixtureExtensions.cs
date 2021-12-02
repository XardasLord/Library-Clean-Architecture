using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace Library.Tests.Base
{
    public static class FixtureExtensions
    {
        public static int CreateInt(this IFixture fixture, int min, int max)
            => fixture.Create<int>() % (max - min + 1) + min;

        public static decimal CreateDecimal(this IFixture fixture, decimal min, decimal max) =>
            fixture.Create<decimal>() % (max - min + 1) + min;


        public static T GetEnumValue<T>(this IFixture fixture, params T[] exceptValues) where T : Enum =>
            GetEnumValue(fixture, exceptValues.ToList());

        public static T GetEnumValue<T>(this IFixture fixture, IEnumerable<T> exceptValues) where T : Enum =>
            fixture.Create<Generator<T>>()
                .Skip(fixture.Create<int>())
                .First(v => !exceptValues.Contains(v));
    }
}