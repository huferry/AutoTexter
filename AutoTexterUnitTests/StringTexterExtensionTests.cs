using AutoTexter;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoTexterUnitTests
{
    [TestClass]
    public class StringTexterExtensionTests
    {
        [TestMethod]
        public void Text_WithNoVariable_ReturnsSameText()
        {
            // Arrange
            var template = "no variable";

            // Act
            var actual = template.Text("myVar", "myValue");

            // Assert
            actual.Should().Be(template);
        }

        [TestMethod]
        public void Text_WithMatchingVariable_ShouldReplaceVariable()
        {
            // Arrange
            var template = "My name is {name}.";

            // Act
            var actual = template.Text("name", "John");

            // Assert
            actual.Should().Be("My name is John.");
        }

        [TestMethod]
        public void Text_WithMatchingVariables_ShouldReplaceVariables()
        {
            // Arrange
            var template = "My name is {name} and I am {age}.";

            // Act
            var actual = template.Text("name|age", "John|30");

            // Assert
            actual.Should().Be("My name is John and I am 30.");
        }

        [TestMethod]
        public void Text_WithIncompleteVariableValue_ShouldReplaceEmpty()
        {
            // Arrange
            var template = "My name is {name} and I am {age}.";

            // Act
            var actual = template.Text("name|age", "John");

            // Assert
            actual.Should().Be("My name is John and I am .");
        }

        [TestMethod]
        public void Text_WithIncompleteVariable_ShouldNotReplace()
        {
            // Arrange
            var template = "My name is {name} and I am {age}.";

            // Act
            var actual = template.Text("name", "John");

            // Assert
            actual.Should().Be(template);
        }

        [TestMethod]
        public void Text_WithMultipleValues_ShouldReturnsTwoLines()
        {
            // Arrange
            var template = "My name is {name}.";

            // Act
            var actual = template.Text("name", "John", "Ringo");

            // Assert
            actual.Should().Be("My name is John.\nMy name is Ringo.");
        }

        [TestMethod]
        public void Text_WithMultipleVariablesAndMultipleValues_ReturnsMultipleLines()
        {
            // Arrange
            var template = "The {animal} has {legs} legs.";

            // Act
            var actual = template.Text("legs|animal", "4|cat", "2|chicken");

            // Assert
            actual.Should().Be("The cat has 4 legs.\nThe chicken has 2 legs.");
        }

        [TestMethod]
        public void Texts_WithMultipleVariablesAndMultipleValues_ReturnsMultipleLines()
        {
            // Arrange
            var template = "The {animal} has {legs} legs.";

            var expectation = new[]
            {
                "The cat has 4 legs.",
                "The chicken has 2 legs."
            };

            // Act
            var actual = template.Texts("legs|animal", "4|cat", "2|chicken");

            // Assert
            actual.ShouldBeEquivalentTo(expectation);
        }

        [TestMethod]
        public void IsMatchVariables_WithMatchingVariables_ReturnsTrue()
        {
            // Arrange
            var template = "The {animal} has {legs} legs.";

            // Act
            var actual = template.IsMatchVariables("animal|legs");

            // Assert
            actual.Should().BeTrue();
        }
    }
}