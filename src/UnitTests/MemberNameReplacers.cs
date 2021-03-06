﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.Internal;
using Xunit;

namespace AutoMapper.UnitTests
{
    using Should.Core.Assertions;

    public class When_using_a_member_name_replacer : SpecBase
    {
        public class Source
        {
            public int Value { get; set; }
            public int Ävíator { get; set; }
            public int SubAirlinaFlight { get; set; }
        }

        public class Destination
        {
            public int Value { get; set; }
            public int Aviator { get; set; }
            public int SubAirlineFlight { get; set; }
        }

        [Fact]
        public void Should_map_properties_with_different_names()
        {
            Mapper.Initialize(c =>
            {
                c.ReplaceMemberName("Ä", "A");
                c.ReplaceMemberName("í", "i");
                c.ReplaceMemberName("Airlina", "Airline");
            });

            var source = new Source()
            {
                Value = 5,
                Ävíator = 3,
                SubAirlinaFlight = 4
            };

            //Mapper.AddMemberConvention().AddName<ReplaceName>(_ => _.AddReplace("A", "Ä").AddReplace("i", "í").AddReplace("Airline", "Airlina")).SetMemberInfo<FieldPropertyMemberInfo>();
            Mapper.CreateMap<Source, Destination>();
            var destination = Mapper.Map<Source, Destination>(source);

            Assert.Equal(source.Value, destination.Value);
            Assert.Equal(source.Ävíator, destination.Aviator);
            Assert.Equal(source.SubAirlinaFlight, destination.SubAirlineFlight);
        }
    }

}
