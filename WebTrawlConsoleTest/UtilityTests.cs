using NUnit.Framework;

namespace Tests
{
	public class UtilityTests
	{
		[Test]
		public void StringifyInsertsAPlusSignBetweenASingleSpaceInAString()
		{
			// Given
			var searchString = "Martin Paul Goodrich";

			// When
			var result = WebTrawlConsole.WebUtility.ConvertToUrlSearchString(searchString);
			// Then

			Assert.AreEqual(result, "Martin+Paul+Goodrich");
		}

		[Test]
		public void StringifyInsertsAPlusSignBetweenAStringThatContainsMultipleSpaces()
		{
			// Given
			var searchString = " Martin      Paul  Goodrich";

			// When
			var result = WebTrawlConsole.WebUtility.ConvertToUrlSearchString(searchString);
			// Then

			Assert.AreEqual(result, "Martin+Paul+Goodrich");
		}
		[Test]
		public void StringifyInsertsCorrectSpacesWhenTargetContainsLeadingAndOrTrailingSpaces()
		{
			// Given
			var searchString = " Martin      Paul  Goodrich";

			// When
			var result = WebTrawlConsole.WebUtility.ConvertToUrlSearchString(searchString);
			// Then

			Assert.AreEqual(result, "Martin+Paul+Goodrich");
		}

		[Test]
		public void AStringifyInsertsCorrectSpacesWhenTargetContainsLeadingAndOrTrailingSpaces()
		{
			// Given
			var searchString = "";

			// When
			var result = WebTrawlConsole.WebUtility.ConvertToUrlSearchString(searchString);
			// Then

			Assert.AreEqual(result, "");
		}


	}
}