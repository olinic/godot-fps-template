using GdUnit4;
using static GdUnit4.Assertions;

namespace FPS;
[TestSuite]
public class OptionalTest
{
    private bool flag = false;

    [Before]
    public void setup()
    {
        flag = false;
    }

    [TestCase]
    public void GivenPopulatedOptional_GetValue_ReturnsValue()
    {
        Optional<int> optional= Optional<int>.Of(1);
        AssertInt(optional.GetValue()).IsEqual(1);
    }

    [TestCase]
    public void GivenEmptyOptional_IsPresent_ReturnsFalse()
    {
        AssertBool(Optional<int>.Empty().IsPresent()).IsFalse();
    }

    [TestCase]
    public void GivenEmptyOptional_IfPresent_DoesNothing()
    {
        Optional<int> optional = Optional<int>.Empty();
        optional.IfPresent(value => this.flag = true);
        AssertBool(flag).IsFalse();
    }

    [TestCase]
    public void GivenPopulatedOptional_IfPresent_PerformsAction()
    {
        Optional<int> optional = Optional<int>.Of(1);
        optional.IfPresent(value => this.flag = true);
        AssertBool(flag).IsTrue();
    }

    [TestCase]
    public void GivenPopulatedOptional_OrElse_ReturnsFirstValue()
    {
        Optional<int> optional = Optional<int>.Of(1);
        AssertInt(optional.OrElse(2)).IsEqual(1);
    }

    [TestCase]
    public void GivenEmptyOptional_OrElse_ReturnsSecondValue()
    {
        Optional<int> optional = Optional<int>.Empty();
        AssertInt(optional.OrElse(2)).IsEqual(2);
    }
}