using BookReview.Debugging;

namespace BookReview
{
    public class BookReviewConsts
    {
        public const string LocalizationSourceName = "BookReview";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "2e9f26739d474926bfae8af171864491";
    }
}
