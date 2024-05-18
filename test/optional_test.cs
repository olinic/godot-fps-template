
using GdUnit4;
using static GdUnit4.Assertions;

[TestSuite]
public class OptionalTest
{
    [TestCase]
    public void success()
    {
        AssertBool(true).IsTrue();
    }
}