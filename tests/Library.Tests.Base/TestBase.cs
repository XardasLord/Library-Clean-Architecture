using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.SharedKernel;
using MediatR;
using NSubstitute;

namespace Library.Tests.Base
{
	public abstract class TestBase
	{
		// Shared
		protected readonly IFixture _fixture = new Fixture();
		protected readonly CancellationToken DefaultCancellationToken = CancellationToken.None;
		protected readonly ICurrentUser CurrentUser = Substitute.For<ICurrentUser>();
		protected readonly ICurrentDateTime CurrentDateTime = Substitute.For<ICurrentDateTime>();

		// Domain

		// Application
		protected readonly IAggregateReadRepository<Book> BookAggregateReadRepository = Substitute.For<IAggregateReadRepository<Book>>();
		protected readonly IMediator Mediator = Substitute.For<IMediator>();

		protected long ExpectedUserId;
		protected DateTime ExpectedDateTime;

		protected string ExpectedBatchAccreditationNumber;

		protected TestBase()
		{
			ExpectedUserId = CreateInt();
			ExpectedDateTime = DateTime.UtcNow;
			ExpectedBatchAccreditationNumber = CreateString();

			MockCurrentUserContext(ExpectedUserId);
			MockCurrentDateTime(ExpectedDateTime);
		}

		protected void MockCurrentUserContext(long userId)
		{
			CurrentUser.UserId.Returns(userId);
		}

		protected void MockCurrentDateTime(DateTime dateTime) =>
			CurrentDateTime.UtcNow.Returns(dateTime);

		public static IEnumerable<object[]> StringNullOrWhiteSpaceData =>
			new[] { "", " ", "  ", null }.ToMemberData();

		public static IEnumerable<object[]> DecimalNegativeData =>
			new[] { -100m, -10m, -1m, -0.1m, -0.01m }.ToMemberData();

		public static IEnumerable<object[]> IntNegativeData =>
			new[] { -100, -10, -1 }.ToMemberData();

		public static IEnumerable<object[]> IntNullOrNegativeData =>
			new[] { (int?)null, -100, -10, -1 }.ToMemberData();

		public static IEnumerable<object[]> DecimalPositiveData =>
			new[] { 100m, 10m, 1m, 0.1m, 0.01m }.ToMemberData();

		public static IEnumerable<object[]> DecimalPositiveAndZeroData =>
			new[] { 100m, 10m, 1m, 0.1m, 0.01m, 0m }.ToMemberData();

		public static IEnumerable<object[]> BudgetCostInvalidData =>
			new decimal?[] { -1m, -0.1m, -10m, null, 0.001m, 0.123m }
			   .ToMemberData();

		public static IEnumerable<object[]> BoolNullableInvalidData =>
			new[] { (bool?)null, false }
			   .ToMemberData();

		public static IEnumerable<object[]> DecimalNullOrNegativeData =>
			new[] { (decimal?)null, -100m, -10m, -1m, -0.1m, -0.01m }
			   .ToMemberData();

		protected T Create<T>() =>
			_fixture.Create<T>();

		protected string CreateString() =>
			Create<string>();

		protected DateTime CreateDateTime() =>
			Create<DateTime>();

		protected int CreateInt() =>
			Create<int>();

		protected int CreateSmallInt() =>
			CreateInt(1, 10);

		protected int CreateInt(int min, int max) =>
			_fixture.CreateInt(min, max);

		protected decimal CreateDecimal() =>
			Create<decimal>();

		protected Guid CreateGuid() =>
			Create<Guid>();

		protected T CreateEnumExceptGiven<T>(IEnumerable<T> exceptValues) where T : Enum =>
			CreateEnumExceptGiven(exceptValues.ToArray());

		protected T CreateEnumExceptGiven<T>(params T[] exceptValues) where T : Enum =>
			_fixture.GetEnumValue(exceptValues);

		protected static IEnumerable<object[]> GetEnumValuesExceptGiven<T>(params T[] exceptValues) where T : Enum =>
			TestExtensions.GetEnumValues(exceptValues);

		protected static IEnumerable<object[]> GetEnumValuesExceptGiven<T>(IEnumerable<T> exceptValues) where T : Enum =>
			TestExtensions.GetEnumValues(exceptValues.ToArray());

		protected static IEnumerable<object[]> GetEnumValuesExceptGiven<T>(IEnumerable<T> exceptValues,
		                                                                   params T[] additionalExceptValues) where T : Enum =>
			TestExtensions.GetEnumValues(exceptValues.Concat(additionalExceptValues).ToArray());
		
		protected static IEnumerable<object[]> GetEnumValues<T>() where T : Enum =>
			TestExtensions.GetEnumValues<T>();
	}
}
