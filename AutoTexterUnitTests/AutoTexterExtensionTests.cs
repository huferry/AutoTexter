using AutoTexter;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoTexterUnitTests
{
    [TestClass]
    public class AutoTexterExtensionTests
    {
        [TestMethod]
        public void AutoText_WithTexts_ReturnsWitReplacedVariables()
        {
            // Arrange
            var text = new[]
            {
                "Hello, {name}!",
                "These are my pets:",
                "- {number} {animal}"
            };

            var expectation = new[]
            {
                "Hello, World!",
                "These are my pets:",
                "- 2 dogs",
                "- 4 cats",
                "- 1 fish"
            };

            var values = new[]
            {
                new AutoTextValues("name", "World"),
                new AutoTextValues("number|animal", "2|dogs", "4|cats", "1|fish")
            };

            // Act
            var actual = text.AutoText(values);

            // Assert
            actual.ShouldBeEquivalentTo(expectation);
        }
    }
}