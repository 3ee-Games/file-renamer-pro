using NUnit.Framework;
using System;
using FileRenamerPro;

[TestFixture]
public class FileRenamerProTests
{
    [Test]
    public void CleanFileName_SpacesFilterLevel_RemovesSpaces()
    {
        string fileName = "Hello Wor l d";
        string expected = "helloworld";
        Program._filter = Program.FilterLevel.Spaces;
        string actual = Program.CleanFileName(fileName);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CleanFileName_SpacesParenthesesFilterLevel_RemovesSpacesAndParentheses()
    {
        string fileName = "Hello (World)";
        string expected = "helloworld";
        Program._filter = Program.FilterLevel.SpacesParentheses;
        string actual = Program.CleanFileName(fileName);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CleanFileName_SpecialCharactersFilterLevel_RemovesSpecialCharacters()
    {
        string fileName = "Hello@World!";
        string expected = "helloworld";
        Program._filter = Program.FilterLevel.SpecialCharacters;
        string actual = Program.CleanFileName(fileName);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CleanFileName_NumbersFilterLevel_RemovesNumbers()
    {
        string fileName = "hello123world456";
        string expected = "helloworld";
        Program._filter = Program.FilterLevel.Numbers;
        string actual = Program.CleanFileName(fileName);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CleanFileName_GuidFilterLevel_GeneratesNewGuid()
    {
        string fileName = "d6ef5b3e-d019-46b5-89c0-6bb6a1db1560";
        Program._filter = Program.FilterLevel.Guid;
        string actual = Program.CleanFileName(fileName);

        Guid guid;
        bool isGuid = Guid.TryParse(actual, out guid);
        Assert.IsTrue(isGuid);
    }
}
