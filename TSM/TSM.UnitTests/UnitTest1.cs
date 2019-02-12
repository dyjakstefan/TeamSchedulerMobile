using System;
using System.Globalization;
using FluentAssertions;
using TSM.Helpers;
using Xunit;

namespace TSM.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void ConverterShouldReturnReversedBool()
        {
            //Arrange
            var converter = new InvertBoolConverter();

            //Act
            var result = converter.Convert(true, typeof(bool), true, CultureInfo.CurrentCulture);

            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public void ConverterWrongTypeShouldNotReversed()
        {
            //Arrange
            var converter = new InvertBoolConverter();
            var number = 5;

            //Act
            var result = converter.Convert(number, typeof(bool), true, CultureInfo.CurrentCulture);

            //Assert
            result.Should().Be(number);
        }

        [Fact]
        public void ConverterBackShouldThrowException()
        {
            //Arrange
            var converter = new InvertBoolConverter();
            var number = 5;

            //Act
            Action act = () => converter.ConvertBack(number, typeof(bool), true, CultureInfo.CurrentCulture);

            //Assert
            act.Should().Throw<Exception>();
        }
    }
}
