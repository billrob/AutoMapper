﻿using Xunit;
using Should;
using System;

namespace AutoMapper.UnitTests.Bug
{
    public class InternalProperties : AutoMapperSpecBase
    {
        public int SomeValue = 2354;
        private Destination _destination;

        class Source
        {
            internal int Number { get; set; }
        }
        class Destination
        {
            internal int Number { get; set; }
        }

        protected override MapperConfiguration Configuration => new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => true;
            cfg.CreateMap<Source, Destination>();
        });

        protected override void Because_of()
        {
            _destination = Mapper.Map<Source, Destination>(new Source { Number = SomeValue });
        }

        [Fact]
        public void Should_map_internal_property()
        {
            _destination.Number.ShouldEqual(SomeValue);
        }
    }
}