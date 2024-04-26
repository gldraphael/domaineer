using FluentAssertions;

namespace Domaineer.Core.Tests.DomainParserTests;

public class IsValid_should
{
    [Theory]
    [InlineData("galdin.dev")]
    [InlineData("galdin42.dev")]
    public void Return_true_if_domain_is_valid(string domain)
    {
        DomainParser.IsValid(domain)
            .Should().BeTrue();
    }

    [Theory]
    // > 255 characters
    [InlineData("galdin-liomsd2zfhro0klep5tz1axgr3ccxqcv64wxqci84vyzng3ozjtuobg5cbwrb70kve701ulopzpda4bjuperl2pzfqvjpronhxjckwe45v6ddaftliks2thygtiznhteewxpocuyzsi0fuo89td38qb8tstkwfucstw2hstzipinm4l7chr0ccntrqavjvdxyjedfoexzepteehpwteejfqrrcbldggpufaucdbsy2khjkwhg33ciptqi0yqvazt.dev")]
    public void Return_false_if_domain_is_too_long(string domain)
    {
        DomainParser.IsValid(domain)
            .Should().BeFalse();
    }

    [Theory]
    [InlineData(".galdin.dev")] // start with a .
    [InlineData("galdin.dev.")] // end with a .
    // [InlineData("gal@din.dev")]
    public void Return_false_if_domain_contains_invalid_characters(string domain)
    {
        DomainParser.IsValid(domain)
            .Should().BeFalse();
    }
}
