namespace Common.Application.Validation
{
    public static class ValidationMessages
    {
        #region Custome
        public const string RequiredUserIdField = "وارد کردن شناسه کاربری اجباری است";

        #endregion
        public const string RequiredField = "وارد کردن این فیلد اجباری است";
        public const string InvalidPhoneNumber = "شماره تلفن نامعتبر است";
        public const string NotFound = "اطلاعات درخواستی یافت نشد";
        public const string MaxLengthField = "تعداد کاراکتر های وارد شده بیشتر از حد مجاز است";
        public const string MinLengthField = "تعداد کاراکتر های وارد شده کمتر از حد مجاز است";

        public static string Required(string field) => $" لطفا {field} را وارد کنید ";
        public static string MaxLength(string field, int maxLength) => $"{field} باید کمتر از {maxLength} کاراکتر باشد";
        public static string MinLength(string field, int minLength) => $"{field} باید بیشتر از {minLength} کاراکتر باشد";
    }
}