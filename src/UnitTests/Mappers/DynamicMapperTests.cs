﻿using System.Collections.Generic;
using System.Dynamic;
using Should;
using Should.Core.Assertions;
using Xunit;

namespace AutoMapper.UnitTests.Mappers.Dynamic
{
    class Destination
    {
        public string Foo { get; set; }
        public string Bar { get; set; }
    }

    public class DynamicDictionary : DynamicObject
    {
        private readonly Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return dictionary.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            dictionary[binder.Name] = value;
            return true;
        }
    }

    public class When_mapping_to_dynamic
    {
        dynamic _destination;

        [Fact]
        public void Should_map_source_properties()
        {
            _destination = Mapper.Map<DynamicDictionary>(new Destination {Foo = "Foo", Bar = "Bar"});
            Assert.Equal("Foo", _destination.Foo);
            Assert.Equal("Bar", _destination.Bar);
        }
    }

    public class When_mapping_from_dynamic
    {
        Destination _destination;

        [Fact]
        public void Should_map_destination_properties()
        {
            dynamic source = new DynamicDictionary();
            source.Foo = "Foo";
            source.Bar = "Bar";
            _destination = Mapper.Map<Destination>(source);
            _destination.Foo.ShouldEqual("Foo");
            _destination.Bar.ShouldEqual("Bar");
        }
    }

    public class When_mapping_from_dynamic_with_missing_property
    {
        Destination _destination;

        [Fact]
        public void Should_map_existing_properties()
        {
            dynamic source = new DynamicDictionary();
            source.Foo = "Foo";
            _destination = Mapper.Map<Destination>(source);
            _destination.Foo.ShouldEqual("Foo");
            _destination.Bar.ShouldBeNull();
        }
    }

    public class When_mapping_from_dynamic_to_dynamic
    {
        dynamic _destination;

        [Fact]
        public void Should_map()
        {
            dynamic source = new DynamicDictionary();
            source.Foo = "Foo";
            source.Bar = "Bar";
            _destination = Mapper.Map<DynamicDictionary>(source);
            Assert.Equal("Foo", _destination.Foo);
            Assert.Equal("Bar", _destination.Bar);
        }
    }
}