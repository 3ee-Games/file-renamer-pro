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
        string actual = Program.CleanFileName(fileName, Program.FilterLevel.SPACES.ToString());

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CleanFileName_SpacesParenthesesFilterLevel_RemovesSpacesAndParentheses()
    {
        string fileName = "Hello (World)";
        string expected = "helloworld";
        string actual = Program.CleanFileName(fileName, Program.FilterLevel.SPACES_PARENTHESES.ToString());

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CleanFileName_SpecialCharactersFilterLevel_RemovesSpecialCharacters()
    {
        string fileName = "Hello@World!";
        string expected = "helloworld";
        string actual = Program.CleanFileName(fileName, Program.FilterLevel.SPECIAL_CHARACTERS.ToString());

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CleanFileName_NumbersFilterLevel_RemovesNumbers()
    {
        // Arrange
        string fileName = "hello123world456";
        string expected = "helloworld";
        string actual = Program.CleanFileName(fileName, Program.FilterLevel.NUMBERS.ToString());

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void CleanFileName_GuidFilterLevel_GeneratesNewGuid()
    {

        string fileName = "d6ef5b3e-d019-46b5-89c0-6bb6a1db1560";
        string actual = Program.CleanFileName(fileName, Program.FilterLevel.GUID.ToString());

        Guid guid;
        bool isGuid = Guid.TryParse(actual, out guid);
        Assert.IsTrue(isGuid);
    }
}
