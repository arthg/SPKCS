using System;
using AutoMapper;
using NUnit.Framework;

namespace WW.WeatherFeedClient.Tests
{
    public abstract class MapperProfileTestsBase<TProfile> 
        where TProfile : Profile
    {
        [SetUp]
        public void InitializeMapper()
        {
            Mapper.Reset();
            Mapper.Initialize(m => m.AddProfile(Activator.CreateInstance<TProfile>()));
        }

        [Test]
        public void Should_have_valid_configuration()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}