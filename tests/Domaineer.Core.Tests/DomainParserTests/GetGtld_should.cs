using FluentAssertions;

namespace Domaineer.Core.Tests.DomainParserTests;

public class GetGtld_should
{

    [Theory]
    [InlineData("galdin.dev", "dev")]
    // [InlineData("google.co.in", "co.in")]
    [InlineData("facebook.com", "com")]
    public void Return_gtld(string domain, string gtld)
    {
        var result = DomainParser.GetGtld(domain);

        result.IsSuccess
            .Should().BeTrue();

        result.Value
            .Should().BeEquivalentTo(gtld);
    }

    [Fact]
    public void Fail_if_invalid_domain()
    {
        var result = DomainParser.GetGtld(".blah.com.");

        result.IsSuccess
            .Should().BeFalse();
    }    
}
