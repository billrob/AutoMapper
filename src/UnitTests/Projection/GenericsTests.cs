﻿namespace AutoMapper.UnitTests.Projection
{
    using System.Linq;
    using QueryableExtensions;
    using Should;
    using Xunit;

    public class GenericsTests : AutoMapperSpecBase
    {
        private Dest<string>[] _dests;

        public class Source<T>
        {
            public T Value { get; set; }
        }

        public class Dest<T>
        {
            public T Value { get; set; }
        }

        protected override MapperConfiguration Configuration => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap(typeof (Source<>), typeof (Dest<>));
        });

        protected override void Because_of()
        {
            var sources = new[]
            {
                new Source<string>
                {
                    Value = "5"
                }
            }.AsQueryable();

            _dests = sources.ProjectTo<Dest<string>>(Configuration).ToArray();
        }

        [Fact]
        public void Should_convert_even_though_mapper_not_explicitly_called_before_hand()
        {
            _dests[0].Value.ShouldEqual("5");
        }
    }
}