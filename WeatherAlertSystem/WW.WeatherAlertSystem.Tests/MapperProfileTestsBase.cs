using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using WW.WeatherFeedClient.Common;

namespace WW.WeatherFeedClient.Tests
{
    public abstract class MapperProfileTestsBase<TProfile> 
        where TProfile : Profile
    {
        private Profile _profile;

        [SetUp]
        public void ConfigureMapperWithConfiguration()
        {
            _profile = CreateProfile();
            Mapper.Reset();
            Mapper.Initialize(m =>
            {
                GetAdditionalProfiles().ForEach(m.AddProfile);
                m.AddProfile(_profile);
            });
        }

        protected virtual Profile CreateProfile()
        {
            return Activator.CreateInstance<TProfile>();
        }

        protected virtual IEnumerable<Profile> GetAdditionalProfiles()
        {
            return Enumerable.Empty<Profile>();
        }

        [Test]
        public void Should_have_valid_configuration()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}