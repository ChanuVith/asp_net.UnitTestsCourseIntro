using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using System.Net.NetworkInformation;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _pingService;
        private readonly IDNS _dNS;

        public NetworkServiceTests()
        {
            //_dNS = new IDNS();
            /*Above line gives the error, "Cannot create an instance of the
            abstract type or interface 'IDNS'. This is where we need mocking 
            for continue the unit testing and the 'FakeItEasy' is the best
            mocking framework to do this job.
            */

            _dNS = A.Fake<IDNS>();
            _pingService = new NetworkService(_dNS);
        }

        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            //Arrange - variables, classes, mocks
            // var pingService = new NetworkService();
            A.CallTo(() => _dNS.SendDNS()).Returns(true);
            /* what this does is, return true for below line
             'var dnsSuccess = _dNS.SendDNS();' So that testing 
            does not have to go deepdown through those abstractions.
            Instead, just faking the expected value which is 'true'
            so that testing continues without breaking. */

            //Act
            // var result = pingService.SendPing();
            var result = _pingService.SendPing();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent!");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void NetworkService_PingTimeOut_ReturnInt(int a, int b, int expected)
        {
            //Arrange
            // var pingService = new NetworkService();

            //Act
            // var result =  pingService.PingTimeout(a, b);
            var result = _pingService.PingTimeout(a, b);

            //Assert
            result.Should().Be(expected);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-1000, 0);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDate()
        {
            //Act
            var result = _pingService.LastPingDate();

            //Assert
            result.Should().BeAfter(1.January(2010));
            result.Should().BeBefore(1.January(2030));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnsObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            //Act
            var result = _pingService.GetPingOptions();

            //Assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkService_MostRecentPings_ReturnsCollection()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            //Act
            var result = _pingService.MostRecentPings();

            //Assert
            //result.Should().BeOfType<IEnumerable<PingOptions>>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }

    }
}
